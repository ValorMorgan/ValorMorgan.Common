using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

namespace Installation.CreditRequestToolkit.CustomActions
{
    public class Configuration
    {
        #region CONSTRUCTORS
        public Configuration()
        {
        }

        public Configuration(string webTarget, string webBinTarget, string customActionTarget, string target, string configDir)
        {
            this.WebTarget = webTarget;
            this.WebBinTarget = webBinTarget;
            this.CustomActionTarget = customActionTarget;
            this.Target = target;
            this.ConfigDir = configDir;

            if (string.IsNullOrWhiteSpace(this.WebTarget))
            {
                throw new ArgumentNullException(nameof(this.WebTarget), "WEBTARGET is missing from the CustomActionData collection.");
            }
            if (string.IsNullOrWhiteSpace(this.WebBinTarget))
            {
                throw new ArgumentNullException(nameof(this.WebBinTarget), "WEBBINTARGET is missing from the CustomActionData collection.");
            }
            if (string.IsNullOrWhiteSpace(this.CustomActionTarget))
            {
                throw new ArgumentNullException(nameof(this.CustomActionTarget), "CUSTOMACTIONTARGET is missing from the CustomActionData collection.");
            }
            if (string.IsNullOrWhiteSpace(this.Target))
            {
                throw new ArgumentNullException(nameof(this.Target), "Target is missing from the CustomActionData collection.");
            }
            if (string.IsNullOrWhiteSpace(this.ConfigDir))
            {
                throw new ArgumentNullException(nameof(this.ConfigDir), "CONFIGDIR is missing from the CustomActionData collection.");
            }

            RetrieveFromConfigDir();
            RetrieveFromGlobalSettings();
        }

        public Configuration(Microsoft.Deployment.WindowsInstaller.CustomActionData cad)
         : this(cad["WEBTARGET"], cad["WEBBINTARGET"], cad["CUSTOMACTIONTARGET"], cad["Target"], cad["CONFIGDIR"])
        {
        }
        #endregion

        #region VARIABLES
        public string ProductName = "CreditRequestToolkit";
        public string ShortVersion = "17.10.0";
        public string Version => $"20{ShortVersion}";
        public string ConfigFileName = "Web.config";
        public string XmlInstructionsName => $"{Version}.ConfigUpdate.xml";
        public string AssociationNumber;

        public string SqlServerName => "GSSQL";
        public string SqlDatabaseName => "CreditRequestToolkit";
        public string SqlMasterDBConnectionString => $"Data Source={SqlServerName};Initial Catalog=master;Trusted_Connection=True";
        public string SqlConnectionString => $"Data Source={SqlServerName};Initial Catalog={SqlDatabaseName};Trusted_Connection=True;";

        public string WebTarget;
        public string WebBinTarget;
        public string CustomActionTarget;
        public string Target;
        public string ConfigDir;

        public string DataConfiguration;
        public string GlobalSettings2File;
        public string LoggingConfigurationFile;
        public string LoggingDistributorConfigurationFile;
        public string SettingsKeyFile;

        public string InstallTarget => Path.Combine(Target, $"v{Version}\\Install");

        // NOTE: Add new database versions here
        public readonly string[] DatabaseVersions = { "2017.10.0.0" };
        public readonly string DataScriptNameSetup = "Database Setup";
        public readonly string DataScriptNameSecurity = "Security";
        public readonly string DataScriptNameViews = "Views";
        public readonly string DataScriptNameDataFill = "Data Fill";
        public readonly string DataScriptNameStoredProcedures = "Stored Procedures";

        public readonly bool AreSettingsExternal = true;
        public readonly string ExternalSettingsFileName = "Web.Settings.config";

        public readonly IDictionary<string, string> AppSettings = new Dictionary<string, string>
        {
            { "AssociationNumber", "Association Number" },
            { "ApplicationName", "Application Name" },
            { "CompanyName", "Company Name" },
            { "FileSavingDirectory", "File Saving Directory" },
            { "BdaHistoricalYearRange", "BDA Historical Year Range" },
            { "DefaultPDRating", "Default PD Rating" },
            { "OnBaseDocumentType", "OnBase Document Type" },
            { "OnBasePrimaryCifKeyword", "OnBase Primary CIF Keyword" },
            { "OnBaseTransactionIdKeyword", "OnBase Transaction ID Keyword" },
            { "SendEmails", "Send Emails" },
            { "SMTP", "SMTP" },
            { "EmailTo", "Failure Email To" },
            { "EmailFrom", "Failure Email From" },
            { "EmailSubject", "Failure Email Subject" },
            { "LogSource", "Log Source" },
            { "LogVerbose", "Log Verbose" },
            { "LogExceptionId", "Log ID" },
            { "CrmServiceUrl", "CRM Service URL" },
            { "OnBaseServiceUrl", "OnBase Service URL" },
            { "OnBaseDataSource", "OnBase Data Source" },
            { "OnBaseConnectionTimeout", "OnBase Connection Timeout" },
        };
        #endregion

        #region METHODS
        private void RetrieveFromConfigDir()
        {
            DataConfiguration = Path.Combine(ConfigDir, "DataConfiguration");
            GlobalSettings2File = Path.Combine(ConfigDir, "GlobalSettings2.config");
            LoggingConfigurationFile = Path.Combine(ConfigDir, "LoggingConfiguration.config");
            LoggingDistributorConfigurationFile = Path.Combine(ConfigDir, "LoggingDistributorConfiguration.config");
            SettingsKeyFile = Path.Combine(ConfigDir, "settings.key");
        }

        private void RetrieveFromGlobalSettings()
        {
            if (string.IsNullOrWhiteSpace(GlobalSettings2File))
                RetrieveFromConfigDir();

            XmlDocument globalXmlDocument = new XmlDocument();
            globalXmlDocument.Load(GlobalSettings2File);

            AssociationNumber = globalXmlDocument.SelectSingleNode("GlobalSettings/xmlSerializerSection/GlobalSettings/AssociationNumber")?.InnerText;
        }
        #endregion
    }
}
