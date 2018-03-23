using BgmCollaboration.Installation.ActionLibrary;
using BgmCollaboration.Installation.ActionLibrary.ActionBase;
using BgmCollaboration.Installation.ActionLibrary.ConfigurationMaintenance;
using BgmCollaboration.Installation.ActionLibrary.IisMaintenance;
using BgmCollaboration.Installation.ActionLibrary.InstallLogging;
using BgmCollaboration.Installation.ActionLibrary.InstallationContext;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Installation.CreditRequestToolkit.CustomActions
{
    public class Action : WebApplicationActionBase
    {
        public Action()
            : base()
        {
            _config = new Configuration();
            Logger = new FileSystemLogger(_config.CustomActionTarget);

            base.ExecutePreConfigCustomInstallActions += Action_ExecutePreConfigCustomInstallActions;
            base.ExecutePostConfigCustomInstallActions += Action_ExecutePostConfigCustomInstallActions;
        }

        public Action(Configuration config)
            : this()
        {
            this._config = config;
        }

        private readonly Configuration _config;
        public static AutoResetEvent AutoEvent = new AutoResetEvent(false);
        private InstallProgress _progress = null;
        private InstallProgress Progress
        {
            get
            {
                if (_progress == null || _progress.IsDisposed)
                    _progress = new InstallProgress();
                return _progress;
            }
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            string logFile = Path.Combine(_config.InstallTarget, "install.txt");
            Logger = new FileSystemLogger(logFile);

            base.Context.Parameters["Target"] = _config.Target;
            base.Context.Parameters["ProductVersion"] = _config.ShortVersion;
            base.Context.Parameters["AssetLocationsFileLocation"] = "";
            Thread thread = new Thread(RunThreadedInstall);

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            AutoEvent.WaitOne();
        }

        public void RunThreadedInstall()
        {
            try
            {
                //Bypass the obsolete call internal to ExecuteInstall
                //ExecuteInstall(_config.ProductName, IisSiteEnum.Localhost, AspNetVersionEnum.Asp40, AppPoolEnum.GreenStoneCustom40IntegratedImpersonating, false)
                
                Logger.LogInstallProgress("RunThreadedInstall - Begin");

                const string iisSite = "localhost";
                const AspNetVersionEnum aspNetVersion = AspNetVersionEnum.Asp40;
                const string appPool = "GreenstoneCustom40IntegratedImpersonating";
                const bool allowAnonymousAccess = false;
                const bool integratedSecurity = false;
                const string port = "";

                Logger.LogInstallProgress("RunThreadedInstall - Setup Variables");
                string xmlInstructionsPath = System.IO.Path.Combine(_config.CustomActionTarget, _config.XmlInstructionsName);
                Logger.LogInstallProgress($"RunThreadedInstall - xmlInstructionsPath: {xmlInstructionsPath}");
                ConfigurationGateway configGateway = new ConfigurationGateway(_config.ProductName, _config.Version, xmlInstructionsPath);
                ActionFacade actionFacade = new ActionFacade(Logger);

                Logger.LogInstallProgress("RunThreadedInstall - Setup Active Directory");
                SetupActiveDirectory();

                Logger.LogInstallProgress("RunThreadedInstall - Execute PreInstall Actions");
                ExecutePreInstallActions(this);

                Logger.LogInstallProgress("RunThreadedInstall - Setup IIS Site");
                IisAppPoolMaintenance appPoolManager = new IisAppPoolMaintenance();
                appPoolManager.AddAppPoolIfNotExists(appPool, aspNetVersion);
                actionFacade.CreateVirtualDirectory(_config.WebTarget, _config.ProductName, iisSite, aspNetVersion, appPool, allowAnonymousAccess, integratedSecurity, port);

                Logger.LogInstallProgress("RunThreadedInstall - Execute PostInstall Actions");
                this.ContextActions = configGateway.ProcessConfigurationInstructions();

                ExecutePostInstallActions(this);
            }
            catch (Exception ex)
            {
                // WriteEntry(<source>, <message>, <type>, <ID>)
                EventLog.WriteEntry(".NET Runtime", ex.ToString(), EventLogEntryType.Error, 1026);
                throw;
            }
            finally
            {
                AutoEvent.Set();
            }
        }

        private void Action_ExecutePreConfigCustomInstallActions(object sender, ActionEventArgs<WebApplicationInstallContext> e)
        {
            ExecutePreInstallActions((RootActionBase) sender);
        }

        public void Action_ExecutePostConfigCustomInstallActions(object sender, ActionEventArgs<WebApplicationInstallContext> e)
        {
            ExecutePostInstallActions((RootActionBase) sender);
        }

        private void ExecutePreInstallActions(RootActionBase sender)
        {
            Progress.Show();

            try
            {
                // No actions to execute
            }
            catch (Exception ex)
            {
                Logger.LogInstallProgress("ERROR");
                Logger.LogInstallProgress(ex.ToString());
                throw;
            }
            finally
            {
                if (Progress.Visible)
                {
                    Progress.Close();
                    Progress.Dispose();
                }
            }
        }

        private void ExecutePostInstallActions(RootActionBase sender)
        {
            // Update to match number of external statement in try below
            const int numberOfActions = 7;
            
            // Leave alone
            int currentAction = 1;

            Progress.Show();

            try
            {
                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Updating settings");
                UpdateSettingsAction.UpdateSettings(sender, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Updating global settings path");
                UpdateSettingsAction.UpdateGlobalSettingsPath(sender, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Updating service URL");
                UpdateSettingsAction.UpdateServiceUrl(sender, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Creating database");
                DatabaseAction.CreateAndUpdateDatabase(Logger, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Applying database security settings");
                DatabaseAction.UpdateDatabaseSecurityPermissions(Logger, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Populating database");
                DatabaseAction.PopulateDatabase(Logger, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Setting up event log source");
                SetUpEventLogSource(sender);

                UpdateProgress(100, "Done");
            }
            catch (Exception ex)
            {
                Logger.LogInstallProgress("ERROR");
                Logger.LogInstallProgress(ex.ToString());
                throw;
            }
            finally
            {
                if (Progress.Visible)
                {
                    Progress.Close();
                    Progress.Dispose();
                }
            }
        }

        private int GetProgressPercent(int numberOfActions, ref int currentAction)
        {
            return (numberOfActions / (currentAction++)) * 100;
        }

        private void UpdateProgress(int progress, string actionText)
        {
            if (!Progress.Visible)
                return;

            Progress.UpdateProgressBar(progress);
            Progress.UpdateActionText(actionText);
        }

        private void SetUpEventLogSource(RootActionBase sender)
        {
            Logger.LogInstallProgress("Setting up the Event Log Source.");

            string log = "BgmCollaboration";
            string source = sender.GetContextAction("Log Source").NewValue;

            if (EventLog.SourceExists(source))
                return;

            EventLog.CreateEventSource(source, log);
            EventLog.WriteEntry(source, $"Testing if logging is setup correctly for {source}. Source: {source}, Log: {log}");

            Logger.LogInstallProgress($"Successfully setup Event Log Source \"{source}\" in Event Log \"{log}\"");
        }
    }
}