
using TaskSchedule.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TaskScheduler;

namespace TaskSchedule
{
    [SupportedOSPlatform("windows")]
    internal class ScheduledTask
    {
        public TaskGeneral General { get; set; }
        public List<TaskAction> Actions { get; set; }
        public List<TaskTrigger> Triggers { get; set; }
        public TaskSetting Setting { get; set; }
        public TaskRequire Require { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public void Regist()
        {
            var taskService = new TaskScheduler.TaskScheduler();
            taskService.Connect(null, null, null, null);
            ITaskFolder rootFolder = taskService.GetFolder(General.TaskPath);

            try
            {
                ITaskDefinition definition = taskService.NewTask(0);
                RegistGeneral(definition);
                RegistActions(definition);
                RegistTriggers(definition);
                RegistRequire(definition);
                RegistSetting(definition);


                rootFolder.RegisterTaskDefinition(
                    General.TaskName,
                    definition,
                    (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE,
                    UserName,
                    Password,
                    _TASK_LOGON_TYPE.TASK_LOGON_NONE,
                    null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (rootFolder != null) Marshal.ReleaseComObject(rootFolder);
                if (taskService != null) Marshal.ReleaseComObject(taskService);
            }
        }

        public void RegistGeneral(ITaskDefinition definition)
        {
            IRegistrationInfo registrationInfo = definition.RegistrationInfo;
            if (General.Author != null)
            {
                registrationInfo.Author = General.Author;
            }
            if (General.Description != null)
            {
                registrationInfo.Description = General.Description;
            }

            IPrincipal principal = definition.Principal;
            if (General.UserId != null)
            {
                principal.UserId = General.UserId;
            }
            principal.LogonType = General.GetLogonType();

            if (General.RunWithHighest != null)
            {
                principal.RunLevel = (bool)General.RunWithHighest ?
                    _TASK_RUNLEVEL.TASK_RUNLEVEL_HIGHEST :
                    _TASK_RUNLEVEL.TASK_RUNLEVEL_LUA;
            }
            if (General.Hidden != null)
            {
                ITaskSettings taskSettings = definition.Settings;
                taskSettings.Hidden = (bool)General.Hidden;
            }
        }

        public void RegistActions(ITaskDefinition definition)
        {
            IActionCollection actionCollection = definition.Actions;
            foreach (var action in Actions)
            {
                IExecAction execAction = (IExecAction)actionCollection.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
                execAction.Path = action.Path;
                execAction.Arguments = action.Arguments;
                execAction.WorkingDirectory = action.WorkingDirectory;
            }
        }

        public void RegistTriggers(ITaskDefinition definition)
        {
            ITriggerCollection triggerCollection = definition.Triggers;
            foreach (var taskTrigger in Triggers)
            {
                ITrigger trigger;
                switch (taskTrigger.Type)
                {
                    case TaskTrigger.TriggerType.Time:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);
                        RegistTrigger_time((ITimeTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Daily:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
                        RegistTrigger_daily((IDailyTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Weekly:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY);
                        RegistTrigger_weekly((IWeeklyTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Monthly:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLY);
                        RegistTrigger_monthly((IMonthlyTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.MonthlyDOW:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLYDOW);
                        RegistTrigger_monthlyDOW((IMonthlyDOWTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Boot:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_BOOT);
                        RegistTrigger_boot((IBootTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Logon:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);
                        RegistTrigger_logon((ILogonTrigger)trigger, taskTrigger);
                        break;
                }
            }
        }

        public void RegistRequire(ITaskDefinition definition)
        {
            ITaskSettings taskSettings = definition.Settings;
            if (Require.RunOnlyIfIdle != null)
            {
                taskSettings.RunOnlyIfIdle = (bool)Require.RunOnlyIfIdle;
            }
            if (Require.IdleDuration != null)
            {
                taskSettings.IdleSettings.IdleDuration = Functions.ToText(Require.IdleDuration);
            }
            if (Require.WaitTimeout != null)
            {
                taskSettings.IdleSettings.WaitTimeout = Functions.ToText(Require.WaitTimeout);
            }
            if (Require.StopOnIdleEnd != null)
            {
                taskSettings.IdleSettings.StopOnIdleEnd = (bool)Require.StopOnIdleEnd;
            }
            if (Require.RestartOnIdle != null)
            {
                taskSettings.IdleSettings.RestartOnIdle = (bool)Require.RestartOnIdle;
            }

            if (Require.DisallowStartIfOnBatteries != null)
            {
                taskSettings.DisallowStartIfOnBatteries = (bool)Require.DisallowStartIfOnBatteries;
            }
            if (Require.StopIfGoingOnBatteries != null)
            {
                taskSettings.StopIfGoingOnBatteries = (bool)Require.StopIfGoingOnBatteries;
            }
            if (Require.WakeToRun != null)
            {
                taskSettings.WakeToRun = (bool)Require.WakeToRun;
            }

            if (Require.RunOnlyIfNetworkAvailable != null)
            {
                taskSettings.RunOnlyIfNetworkAvailable = (bool)Require.RunOnlyIfNetworkAvailable;
            }
            if (Require.NetworkName != null)
            {
                taskSettings.NetworkSettings.Name = Require.NetworkName;
            }
        }

        public void RegistSetting(ITaskDefinition deffinition)
        {
            ITaskSettings settings = deffinition.Settings;
            if (Setting.AllowDemandStart != null)
            {
                settings.AllowDemandStart = (bool)Setting.AllowDemandStart;
            }
            if (Setting.StartWhenAvailable != null)
            {
                settings.StartWhenAvailable = (bool)Setting.StartWhenAvailable;
            }
            if (Setting.EnableRestartInterval == true)
            {
                settings.RestartInterval = Functions.ToText(
                    Setting.RestartInterval == null ?
                        new TimeSpan(0, 1, 0) :
                        Setting.RestartInterval);
            }
            if (Setting.RestartCount != null)
            {
                settings.RestartCount = (int)Setting.RestartCount;
            }
            if (Setting.EnableExecutionTimeLimit == true)
            {
                settings.ExecutionTimeLimit = Functions.ToText(
                    Setting.ExecutionTimeLimit == null ?
                        new TimeSpan(3, 0, 0, 0) :
                        Setting.ExecutionTimeLimit);
            }
            if (Setting.AllowHardTerminate != null)
            {
                settings.AllowHardTerminate = (bool)Setting.AllowHardTerminate;
            }
            if (Setting.EnableDeleteExpiredTaskAfter == true)
            {
                settings.DeleteExpiredTaskAfter = Functions.ToText(
                    Setting.DeleteExpiredTaskAfter == null ?
                        new TimeSpan(30, 0, 0, 0) :
                        Setting.DeleteExpiredTaskAfter);
            }
            if (Setting.MultipleInstances != null)
            {
                settings.MultipleInstances = Setting.MultipleInstances switch
                {
                    TaskSetting.TaskInstancePolicy.IgnoreNew => _TASK_INSTANCES_POLICY.TASK_INSTANCES_IGNORE_NEW,
                    TaskSetting.TaskInstancePolicy.Parallel => _TASK_INSTANCES_POLICY.TASK_INSTANCES_PARALLEL,
                    TaskSetting.TaskInstancePolicy.Queue => _TASK_INSTANCES_POLICY.TASK_INSTANCES_QUEUE,
                    TaskSetting.TaskInstancePolicy.StopExisting => _TASK_INSTANCES_POLICY.TASK_INSTANCES_STOP_EXISTING,
                };
            }
        }

        #region RegistTrigger private methods

        private void RegistTrigger_time(ITimeTrigger trigger, TaskTrigger taskTrigger)
        {
            trigger.StartBoundary = Functions.ToText(taskTrigger.StartBoundary);

            if (taskTrigger.RandomDelay != null)
            {
                trigger.RandomDelay = Functions.ToText(taskTrigger.RandomDelay);
            }
            if (taskTrigger.RepetitionInteval != null)
            {
                trigger.Repetition.Interval = Functions.ToText(taskTrigger.RepetitionInteval);
            }
            if (taskTrigger.RepetitionDuration != null)
            {
                trigger.Repetition.Duration = Functions.ToText(taskTrigger.RepetitionDuration);
            }
            if (taskTrigger.RepetitionStopAtDurationEnd != null)
            {
                trigger.Repetition.StopAtDurationEnd = (bool)taskTrigger.RepetitionStopAtDurationEnd;
            }
            if (taskTrigger.ExecutionTimeLimit != null)
            {
                trigger.ExecutionTimeLimit = Functions.ToText(taskTrigger.ExecutionTimeLimit);
            }
            if (taskTrigger.EndBoundary != null)
            {
                trigger.EndBoundary = Functions.ToText(taskTrigger.EndBoundary);
            }

            trigger.Enabled = taskTrigger.Enabled;
        }

        private void RegistTrigger_daily(IDailyTrigger trigger, TaskTrigger taskTrigger)
        {
            trigger.StartBoundary = Functions.ToText(taskTrigger.StartBoundary);

            if (taskTrigger.DaysInterval != null)
            {
                trigger.DaysInterval = (short)taskTrigger.DaysInterval;
            }
            if (taskTrigger.RandomDelay != null)
            {
                trigger.RandomDelay = Functions.ToText(taskTrigger.RandomDelay);
            }
            if (taskTrigger.RepetitionInteval != null)
            {
                trigger.Repetition.Interval = Functions.ToText(taskTrigger.RepetitionInteval);
            }
            if (taskTrigger.RepetitionDuration != null)
            {
                trigger.Repetition.Duration = Functions.ToText(taskTrigger.RepetitionDuration);
            }
            if (taskTrigger.RepetitionStopAtDurationEnd != null)
            {
                trigger.Repetition.StopAtDurationEnd = (bool)taskTrigger.RepetitionStopAtDurationEnd;
            }
            if (taskTrigger.ExecutionTimeLimit != null)
            {
                trigger.ExecutionTimeLimit = Functions.ToText(taskTrigger.ExecutionTimeLimit);
            }
            if (taskTrigger.EndBoundary != null)
            {
                trigger.EndBoundary = Functions.ToText(taskTrigger.EndBoundary);
            }

            trigger.Enabled = taskTrigger.Enabled;
        }

        private void RegistTrigger_weekly(IWeeklyTrigger trigger, TaskTrigger taskTrigger)
        {
            trigger.StartBoundary = Functions.ToText(taskTrigger.StartBoundary);

            if (taskTrigger.WeeksInterval != null)
            {
                trigger.WeeksInterval = (short)taskTrigger.WeeksInterval;
            }
            if (taskTrigger.DaysOfWeek != null)
            {
                trigger.DaysOfWeek = (short)taskTrigger.DaysOfWeek;
            }
            if (taskTrigger.RandomDelay != null)
            {
                trigger.RandomDelay = Functions.ToText(taskTrigger.RandomDelay);
            }
            if (taskTrigger.RepetitionInteval != null)
            {
                trigger.Repetition.Interval = Functions.ToText(taskTrigger.RepetitionInteval);
            }
            if (taskTrigger.RepetitionDuration != null)
            {
                trigger.Repetition.Duration = Functions.ToText(taskTrigger.RepetitionDuration);
            }
            if (taskTrigger.RepetitionStopAtDurationEnd != null)
            {
                trigger.Repetition.StopAtDurationEnd = (bool)taskTrigger.RepetitionStopAtDurationEnd;
            }
            if (taskTrigger.ExecutionTimeLimit != null)
            {
                trigger.ExecutionTimeLimit = Functions.ToText(taskTrigger.ExecutionTimeLimit);
            }
            if (taskTrigger.EndBoundary != null)
            {
                trigger.EndBoundary = Functions.ToText(taskTrigger.EndBoundary);
            }

            trigger.Enabled = taskTrigger.Enabled;
        }

        private void RegistTrigger_monthly(IMonthlyTrigger trigger, TaskTrigger taskTrigger)
        {
            trigger.StartBoundary = Functions.ToText(taskTrigger.StartBoundary);

            if (taskTrigger.MonthsOfYear != null)
            {
                trigger.MonthsOfYear = (short)taskTrigger.MonthsOfYear;
            }
            if (taskTrigger.DaysOfMonth != null)
            {
                trigger.DaysOfMonth = (short)taskTrigger.DaysOfMonth;
            }
            if (taskTrigger.RandomDelay != null)
            {
                trigger.RandomDelay = Functions.ToText(taskTrigger.RandomDelay);
            }
            if (taskTrigger.RepetitionInteval != null)
            {
                trigger.Repetition.Interval = Functions.ToText(taskTrigger.RepetitionInteval);
            }
            if (taskTrigger.RepetitionDuration != null)
            {
                trigger.Repetition.Duration = Functions.ToText(taskTrigger.RepetitionDuration);
            }
            if (taskTrigger.RepetitionStopAtDurationEnd != null)
            {
                trigger.Repetition.StopAtDurationEnd = (bool)taskTrigger.RepetitionStopAtDurationEnd;
            }
            if (taskTrigger.ExecutionTimeLimit != null)
            {
                trigger.ExecutionTimeLimit = Functions.ToText(taskTrigger.ExecutionTimeLimit);
            }
            if (taskTrigger.EndBoundary != null)
            {
                trigger.EndBoundary = Functions.ToText(taskTrigger.EndBoundary);
            }

            trigger.Enabled = taskTrigger.Enabled;
        }

        private void RegistTrigger_monthlyDOW(IMonthlyDOWTrigger trigger, TaskTrigger taskTrigger)
        {
            trigger.StartBoundary = Functions.ToText(taskTrigger.StartBoundary);

            if (taskTrigger.MonthsOfYear != null)
            {
                trigger.MonthsOfYear = (short)taskTrigger.MonthsOfYear;
            }
            if (taskTrigger.WeeksOfMonth != null)
            {
                trigger.WeeksOfMonth = (short)taskTrigger.WeeksOfMonth;
            }
            if (taskTrigger.DaysOfWeek != null)
            {
                trigger.DaysOfWeek = (short)taskTrigger.DaysOfWeek;
            }
            if (taskTrigger.RandomDelay != null)
            {
                trigger.RandomDelay = Functions.ToText(taskTrigger.RandomDelay);
            }
            if (taskTrigger.RepetitionInteval != null)
            {
                trigger.Repetition.Interval = Functions.ToText(taskTrigger.RepetitionInteval);
            }
            if (taskTrigger.RepetitionDuration != null)
            {
                trigger.Repetition.Duration = Functions.ToText(taskTrigger.RepetitionDuration);
            }
            if (taskTrigger.RepetitionStopAtDurationEnd != null)
            {
                trigger.Repetition.StopAtDurationEnd = (bool)taskTrigger.RepetitionStopAtDurationEnd;
            }
            if (taskTrigger.ExecutionTimeLimit != null)
            {
                trigger.ExecutionTimeLimit = Functions.ToText(taskTrigger.ExecutionTimeLimit);
            }
            if (taskTrigger.EndBoundary != null)
            {
                trigger.EndBoundary = Functions.ToText(taskTrigger.EndBoundary);
            }

            trigger.Enabled = taskTrigger.Enabled;
        }

        private void RegistTrigger_boot(IBootTrigger trigger, TaskTrigger taskTrigger)
        {
        }

        private void RegistTrigger_logon(ILogonTrigger trigger, TaskTrigger taskTrigger)
        {
            if (!string.IsNullOrEmpty(taskTrigger.UserId))
            {
                trigger.UserId = taskTrigger.UserId;
            }
        }

        #endregion
    }
}
