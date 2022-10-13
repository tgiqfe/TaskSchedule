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
task.Regist();





Console.ReadLine();

