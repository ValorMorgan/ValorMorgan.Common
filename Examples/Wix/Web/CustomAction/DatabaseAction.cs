using BgmCollaboration.Installation.ActionLibrary;
using BgmCollaboration.Installation.ActionLibrary.InstallLogging;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Installation.CreditRequestToolkit.CustomActions
{
    public class DatabaseAction
    {
        private static Configuration Config { get; set; }

        #region CreateAndUpdateDatabase
        [CustomAction()]
        public static ActionResult CreateAndUpdateDatabase(Session session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session), "Session object not provided or found.");

            session.Log("Begin CreateAndUpdateDatabase");

            try
            {
                Config = new Configuration(session.CustomActionData);

                FileSystemLogger logger = new FileSystemLogger("DBMaintenance.log");

                CreateAndUpdateDatabase(logger);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error occurred during install");
                return ActionResult.Failure;
            }

            session.Log("End CreateAndUpdateDatabase");
            return ActionResult.Success;
        }

        public static void CreateAndUpdateDatabase(IInstallLogger logger)
        {
            if (logger != null)
                logger.LogInstallProgress("Create and Update Database");

            string[] fileNames = { Config.DataScriptNameSetup, Config.DataScriptNameViews, Config.DataScriptNameStoredProcedures };

            IterateVersionsAndFileNamesToExecute(logger, Config.DatabaseVersions, fileNames, true);
        }

        public static void CreateAndUpdateDatabase(IInstallLogger logger, Configuration config)
        {
            Config = config;
            CreateAndUpdateDatabase(logger);
        }
        #endregion

        #region UpdateDatabaseSecurityPermissions
        [CustomAction()]
        public static ActionResult UpdateDatabaseSecurityPermissions(Session session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session), "Session object not provided or found.");

            session.Log("Begin UpdateDatabaseSecurityPermissions");

            try
            {
                Config = new Configuration(session.CustomActionData);

                FileSystemLogger logger = new FileSystemLogger("DBMaintenance.log");

                UpdateDatabaseSecurityPermissions(logger);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error occurred during install");
                return ActionResult.Failure;
            }

            session.Log("End UpdateDatabaseSecurityPermissions");
            return ActionResult.Success;
        }

        public static void UpdateDatabaseSecurityPermissions(IInstallLogger logger)
        {
            if (!DatabaseExists())
                throw new InvalidOperationException("Unable to update the security permissions on the database before it is created.");

            IterateVersionsAndFileNamesToExecute(logger, Config.DatabaseVersions, new[] { Config.DataScriptNameSecurity }, false);
        }

        public static void UpdateDatabaseSecurityPermissions(IInstallLogger logger, Configuration config)
        {
            Config = config;
            UpdateDatabaseSecurityPermissions(logger);
        }
        #endregion

        #region PerformDataBuffer
        [CustomAction()]
        public static ActionResult PopulateDatabase(Session session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session), "Session object not provided or found.");

            session.Log("Begin PerformDataBuffer");

            try
            {
                Config = new Configuration(session.CustomActionData);

                FileSystemLogger logger = new FileSystemLogger("DBMaintenance.log");

                PopulateDatabase(logger);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error occurred during install");
                return ActionResult.Failure;
            }

            session.Log("End PerformDataBuffer");
            return ActionResult.Success;
        }

        public static void PopulateDatabase(IInstallLogger logger)
        {
            if (!DatabaseExists())
                throw new InvalidOperationException("Unable to perform the data buffer on the database before it is created.");

            IterateVersionsAndFileNamesToExecute(logger, Config.DatabaseVersions, new[] { Config.DataScriptNameDataFill }, false);
        }

        public static void PopulateDatabase(IInstallLogger logger, Configuration config)
        {
            Config = config;
            PopulateDatabase(logger);
        }
        #endregion

        #region Private Methods
        private static void IterateVersionsAndFileNamesToExecute(IInstallLogger logger, IReadOnlyList<string> versions, string[] fileNames, bool doVersionCheck)
        {
            if (versions == null || !versions.Any())
                return;

            foreach (string version in versions)
            {
                // Run regardless if this is the first version + database wasn't created yet; otherwise check version
                if (doVersionCheck && (!version.Equals(versions[0]) || DatabaseExists()) && IsAtleastVersion(version, logger))
                    continue;

                if (logger != null)
                    logger.LogInstallProgress($"Installing / updating database version {version} against the following scripts: {string.Join(", ", fileNames)}");

                foreach (string fileName in fileNames)
                {
                    if (string.IsNullOrWhiteSpace(fileName))
                        continue;

                    string filePath = Path.Combine(Config.CustomActionTarget, $"SqlScripts\\{version}\\{fileName.Replace(".sql", "")}.sql");
                    if (!File.Exists(filePath))
                        continue;

                    string fileContents = File.ReadAllText(filePath);
                    ExecuteBatch(fileContents, Config.SqlMasterDBConnectionString);
                }

                if (logger != null)
                    logger.LogInstallProgress($"Successfully installed / updated database version {version}");
            }
        }

        private static void ExecuteBatch(string script, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ServerConnection serverConnection = new ServerConnection(connection);
                Server server = new Server(serverConnection);
                server.ConnectionContext.ExecuteNonQuery(script);
            }
        }

        private static bool DatabaseExists()
        {
            bool returnable = false;

            using (SqlConnection connection = new SqlConnection(Config.SqlMasterDBConnectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{Config.SqlDatabaseName}'";
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.Connection.Open();
                int databaseFinds = Convert.ToInt32(command.ExecuteScalar());
                returnable = (databaseFinds > 0);
                command.Connection.Close();
            }

            return returnable;
        }

        private static string DatabaseVersion(IInstallLogger logger)
        {
            string query = string.Format("{0}{1}{0}{2}{0}{3}",
                Environment.NewLine,
                "SELECT TOP 1 MajorVersion, MinorVersion, RevisionVersion, BuildVersion",
                "FROM dbo.DatabaseVersion",
                "ORDER BY MajorVersion DESC, MinorVersion DESC, RevisionVersion DESC, BuildVersion DESC"
            );

            DataRow row = ExecuteDataRow(query, Config.SqlConnectionString);

            // MajorVersion.MinorVersion.RevisionVersion.BuildVersion // Trimmed just in case spaces manage to slip in
            string version = $"{row["MajorVersion"].ToString().Trim()}.{row["MinorVersion"].ToString().Trim()}.{row["RevisionVersion"].ToString().Trim()}.{row["BuildVersion"].ToString().Trim()}";
            if (logger != null)
                logger.LogInstallProgress($"Database version string: | {version}");

            return version;
        }

        private static bool IsVersion(string expectedVersion, IInstallLogger logger)
        {
            string currentVersion = DatabaseVersion(logger);
            return IsVersion(currentVersion, expectedVersion, logger);
        }

        private static bool IsVersion(string currentVersion, string expectedVersion, IInstallLogger logger)
        {
            if (string.IsNullOrWhiteSpace(currentVersion) || string.IsNullOrWhiteSpace(expectedVersion))
                return false;

            if (logger != null)
                logger.LogInstallProgress($"Comparing if current version value \"{currentVersion}\" is at least (greater than or equal to) provided version value \"{expectedVersion}\"");

            string[] currentVersionSplit = currentVersion.Split('.');
            string[] expectedVersionSplit = expectedVersion.Split('.');

            // [ MajorVersion, MinorVersion, Build, Revision ]
            int[] currentVersionValue = new int[4] { int.Parse(currentVersionSplit[0]), int.Parse(currentVersionSplit[1]), int.Parse(currentVersionSplit[2]), int.Parse(currentVersionSplit[3]) };
            int[] expectedVersionValue = new int[4] { int.Parse(expectedVersionSplit[0]), int.Parse(expectedVersionSplit[1]), int.Parse(expectedVersionSplit[2]), int.Parse(expectedVersionSplit[3]) };
            
            return currentVersionValue.Sum() == expectedVersionValue.Sum();
        }

        private static bool IsAtleastVersion(string version, IInstallLogger logger)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version), "Unable to determine if the database is a specified version without a provided version to compare to.");

            string databaseVersion = DatabaseVersion(logger);
            if (string.IsNullOrWhiteSpace(databaseVersion))
                return false;

            if (logger != null)
                logger.LogInstallProgress($"Comparing if database version value \"{databaseVersion}\" is at least (greater than or equal to) provided version value \"{version}\"");

            string[] databaseVersionSplit = databaseVersion.Split('.');
            string[] expectedVersionSplit = version.Split('.');

            // [ MajorVersion, MinorVersion, Build, Revision ]
            int[] currentVersion = new int[4] { int.Parse(databaseVersionSplit[0]), int.Parse(databaseVersionSplit[1]), int.Parse(databaseVersionSplit[2]), int.Parse(databaseVersionSplit[3]) };
            int[] expectedVersion = new int[4] { int.Parse(expectedVersionSplit[0]), int.Parse(expectedVersionSplit[1]), int.Parse(expectedVersionSplit[2]), int.Parse(expectedVersionSplit[3]) };

            // (Major newer)
            // OR (Major same AND Minor newer)
            // OR (Major same AND Minor same AND Build newer)
            // OR (Major same AND Minor same AND Build same AND Revision newer)
            // OR (Major same AND Minor same AND Build same AND Revision same)
            return (currentVersion[0] > expectedVersion[0])
                || (currentVersion[0] == expectedVersion[0] && currentVersion[1] > expectedVersion[1])
                || (currentVersion[0] == expectedVersion[0] && currentVersion[1] == expectedVersion[1] && currentVersion[2] > expectedVersion[2])
                || (currentVersion[0] == expectedVersion[0] && currentVersion[1] == expectedVersion[1] && currentVersion[2] == expectedVersion[2] && currentVersion[3] > expectedVersion[3])
                || (currentVersion[0] == expectedVersion[0] && currentVersion[1] == expectedVersion[1] && currentVersion[2] == expectedVersion[2] && currentVersion[3] == expectedVersion[3]);
        }

        private static DataRow ExecuteDataRow(string query, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "Unable to execute the provided query for a data row with no connection string provided.");
            if (string.IsNullOrWhiteSpace(query))
                return null;

            DataRow row = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                row = table.Rows[0];
            }

            return row;
        }

        private static void ExecuteNonQuery(string query, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "Unable to execute the provided query with no connection string provided.");

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }
        #endregion
    }
}