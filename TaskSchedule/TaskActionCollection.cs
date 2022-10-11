using TaskScheduler;

namespace TaskSchedule
{
    internal class TaskActionCollection
    {
        public List<TaskAction> Actions { get; set; }

        public TaskActionCollection()
        {
            this.Actions = new();
        }

        public void Register(ITaskDefinition definition)
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
    }
}
