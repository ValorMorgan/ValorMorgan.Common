using System;
using System.Collections;
using System.Configuration.Install;
using System.Diagnostics;
using System.Windows.Forms;
using BgmCollaboration.Installation.ActionLibrary.InstallLogging;
using Microsoft.Deployment.WindowsInstaller;

namespace OnBase.TaxReturnUploader.CustomAction
{
    public class ExecuteAllAction
    {
        [CustomAction]
        public static ActionResult ExecuteAll(Session session)
        {
            try
            {
                session.Log("Begin Execute All Custom Actions");

                IInstallLogger emptyLogger = new EmptyLogger();
                InstallContext installContext = new InstallContext();
                foreach (string key in session.CustomActionData.Keys)
                    installContext.Parameters.Add(key, session.CustomActionData[key]);

                session.Log("Setting up configuration");

                Configuration config = new Configuration(session.CustomActionData);

                session.Log("Creating and executing action");

                using (Action action = new Action(config))
                {
                    action.Context = installContext;
                    action.Install(new Hashtable());
                }

                session.Log("End Execute All Custom Actions");
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error occurred during install");
                EventLog.WriteEntry("MsiInstaller", $"{ex.Message} {ex.InnerException?.Message}", EventLogEntryType.Error);
                return ActionResult.Failure;
            }
        }
    }
}