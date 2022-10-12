namespace TaskSchedule.Tasks
{
    internal class TaskAction
    {
        public string Path { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }

        public TaskAction() { }
        public TaskAction(string path, string arguments, string workingDirectory)
        {
            Path = path;
            Arguments = arguments;
            WorkingDirectory = workingDirectory;
        }
    }
}
