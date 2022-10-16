using TaskSchedule;
using TaskSchedule.Tasks;


var task = new ScheduledTask();

task.General.TaskName = "bbbb";
task.General.UserId = "System";
task.Triggers.Add(new TaskTrigger()
{
    Type = TaskTrigger.TriggerType.Daily,
    StartBoundary = DateTime.Today,
    DaysInterval = 1,
    Enabled = true,
});
task.Actions.Add(new TaskAction()
{
    Path = "cmd.exe",
    Arguments = @"/c echo %date% %time% >> D:\Test\Files-Source\tasktest.txt",
});
//task.Regist();

Console.ReadLine();

var taskService = new TaskScheduler.TaskScheduler();
taskService.Connect(null);
ITaskFolder rootFolder = taskService.GetFolder("\\");
var tasks = rootFolder.GetTasks(0);

foreach (IRegisteredTask item in tasks)
{
    Console.WriteLine(item.Name);

    if (item.Name == "bbbb")
    {
        var ret = Console.ReadLine();
        if (ret == "y")
        {
            //rootFolder.DeleteTask(item.Name, 0);
        }
    }
}

tasks.OfType<IRegisteredTask>().ToList().ForEach(x =>{
    Console.WriteLine(x.Name + " ★ " + x.Definition.RegistrationInfo.Author);
});




Console.ReadLine();

