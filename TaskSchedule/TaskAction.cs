using TaskScheduler;

namespace TaskSchedule
{
    internal class TaskAction
    {
        public string Path { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }

        public TaskAction() { }
        public TaskAction(string path, string arguments, string workingDirectory)
        {
            this.Path = path;
            this.Arguments = arguments;
            this.WorkingDirectory = workingDirectory;
        }
    }
}
