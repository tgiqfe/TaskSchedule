namespace TaskSchedule.Tasks
{
    internal class TaskTriggreCollection
    {
        public List<TaskTrigger> Triggers { get; set; }

        public TaskTriggreCollection()
        {
            Triggers = new();
        }

        public void Register(ITaskDefinition definition)
        {
            ITriggerCollection triggerCollection = definition.Triggers;
            foreach (var taskTrigger in Triggers)
            {
                ITrigger trigger;
                switch (taskTrigger.Type)
                {
                    case TaskTrigger.TriggerType.Time:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);
                        RegistTimeTrigger((ITimeTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Daily:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
                        RegistDaiyTrigger((IDailyTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Weekly:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY);
                        RegistWeeklyTrigger((IWeeklyTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Monthly:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLY);
                        RegistMonghlyTrigger((IMonthlyTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.MonthlyDOW:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLYDOW);
                        RegistMonthlyDOMTrigger((IMonthlyDOWTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Boot:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_BOOT);
                        RegistBootTrigger((IBootTrigger)trigger, taskTrigger);
                        break;
                    case TaskTrigger.TriggerType.Logon:
                        trigger = triggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);
                        RegistLogonTrigger((ILogonTrigger)trigger, taskTrigger);
                        break;
                }
            }
        }

        private void RegistTimeTrigger(ITimeTrigger trigger, TaskTrigger taskTrigger)
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

        private void RegistDaiyTrigger(IDailyTrigger trigger, TaskTrigger taskTrigger)
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

        private void RegistWeeklyTrigger(IWeeklyTrigger trigger, TaskTrigger taskTrigger)
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

        private void RegistMonghlyTrigger(IMonthlyTrigger trigger, TaskTrigger taskTrigger)
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

        private void RegistMonthlyDOMTrigger(IMonthlyDOWTrigger trigger, TaskTrigger taskTrigger)
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

        private void RegistBootTrigger(IBootTrigger trigger, TaskTrigger taskTrigger)
        {
        }

        private void RegistLogonTrigger(ILogonTrigger trigger, TaskTrigger taskTrigger)
        {
            if (!string.IsNullOrEmpty(taskTrigger.UserId))
            {
                trigger.UserId = taskTrigger.UserId;
            }
        }
    }
}
