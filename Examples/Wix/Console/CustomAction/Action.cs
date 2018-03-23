using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using BgmCollaboration.Installation.ActionLibrary;
using BgmCollaboration.Installation.ActionLibrary.ActionBase;
using BgmCollaboration.Installation.ActionLibrary.ConfigurationMaintenance;
using BgmCollaboration.Installation.ActionLibrary.InstallationContext;
using BgmCollaboration.Installation.ActionLibrary.InstallLogging;
using Microsoft.Win32.TaskScheduler;

namespace OnBase.TaxReturnUploader.CustomAction
{
    public class Action : ClassLibraryActionBase
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

            string logFile = Path.Combine(_config.CustomActionTarget, "install.txt");
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

                Logger.LogInstallProgress("RunThreadedInstall - Setup Variables");
                string xmlInstructionsPath = Path.Combine(_config.CustomActionTarget, _config.XmlInstructionsName);

                Logger.LogInstallProgress($"RunThreadedInstall - xmlInstructionsPath: {xmlInstructionsPath}");
                ConfigurationGateway configGateway = new ConfigurationGateway(_config.ProductName, _config.Version, xmlInstructionsPath);

                Logger.LogInstallProgress("RunThreadedInstall - Execute PreInstall Actions");
                ExecutePreInstallActions(this);

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

        private void Action_ExecutePreConfigCustomInstallActions(object sender, ActionEventArgs<ClassLibraryInstallContext> e)
        {
            ExecutePreInstallActions((RootActionBase)sender);
        }

        public void Action_ExecutePostConfigCustomInstallActions(object sender, ActionEventArgs<ClassLibraryInstallContext> e)
        {
            ExecutePostInstallActions((RootActionBase)sender);
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
            const int numberOfActions = 4;

            // Leave alone
            int currentAction = 1;

            Progress.Show();

            try
            {
                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Updating settings");
                UpdateSettingsAction.UpdateSettings(sender, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Updating global settings path");
                UpdateSettingsAction.UpdateGlobalSettingsPath(sender, _config);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Setting up event log source");
                SetUpEventLogSource(sender);

                UpdateProgress(GetProgressPercent(numberOfActions, ref currentAction), "Setting up scheduled task");
                SetupScheduledTask(sender);

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
            return (currentAction++ / numberOfActions) * 100;
        }

        private void UpdateProgress(int progress, string actionText)
        {
            if (!Progress.Visible)
                return;

            Logger.LogInstallProgress($"Progress at {progress}% : {actionText}");
            Progress.UpdateProgressBar(progress);
            Progress.UpdateActionText(actionText);
        }

        private void SetUpEventLogSource(RootActionBase sender)
        {
            string source = sender.GetContextAction("Event Log Source").NewValue;

            if (EventLog.SourceExists(source))
                return;

            EventLog.CreateEventSource(source, _config.EventLogLog);
            EventLog.WriteEntry(source, $"Testing if logging is setup correctly for {source}. Source: {source}, Log: {_config.EventLogLog}");

            Logger.LogInstallProgress($"Successfully setup Event Log Source \"{source}\" in Event Log \"{_config.EventLogLog}\"");
        }

        private void SetupScheduledTask(RootActionBase sender)
        {
            string taskName = _config.ProductName;
            string username = sender.GetContextAction("Task Username").NewValue;
            string password = sender.GetContextAction("Task Password").NewValue;
            //short dailyInterval = short.Parse(sender.GetContextAction("Task Daily Interval").NewValue);
            double runInterval = double.Parse(sender.GetContextAction("Task Run Interval").NewValue);
            double dailyDuration = double.Parse(sender.GetContextAction("Task Daily Duration").NewValue);
            double runDuration = double.Parse(sender.GetContextAction("Task Run Duration").NewValue);

            ExecAction executable = new ExecAction(_config.ExecutableName, null, _config.BinTarget);
            TimeTrigger trigger = new TimeTrigger
            {
                StartBoundary = DateTime.Now.AddMinutes(5), // Just start in 5 minutes after install 
                ExecutionTimeLimit = (runDuration > 0) ? TimeSpan.FromSeconds(runDuration) : TimeSpan.Zero
                //DailyInterval = (short)((dailyInterval > 0) ? dailyInterval : 1),
                // TimeSpan.Zero = never/always (depending on context). i.e. No interval, no duration, no time limit, etc.
                // TimeSpan.Zero is the default value on the property regardless
            };

            trigger.Repetition.Interval = (runInterval > 0) ? TimeSpan.FromSeconds(runInterval) : TimeSpan.Zero;
            trigger.Repetition.Duration = (dailyDuration > 0) ? TimeSpan.FromHours(dailyDuration) : TimeSpan.Zero;

            using (TaskService svc = new TaskService())
            {
                svc.AddTask(taskName, trigger, executable, username, password, TaskLogonType.Password);
            }

            Logger.LogInstallProgress("Successfully setup Scheduled Task with the following parameters.");
            Logger.LogInstallProgress($"Task Name: {taskName}");
            Logger.LogInstallProgress($"Run As: {username}");
            //Logger.LogInstallProgress($"Daily Interval: {dailyInterval}");
            Logger.LogInstallProgress($"Run Interval: {runInterval}");
            Logger.LogInstallProgress($"Daily Duration: {dailyDuration}");
            Logger.LogInstallProgress($"Run Duration: {runDuration}");
        }
    }
}