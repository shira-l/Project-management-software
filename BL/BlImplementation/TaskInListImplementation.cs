namespace BlImplementation;
using BlApi;
using BO;
using System.Text.RegularExpressions;


internal class TaskInListImplementation : ITaskInList
{
    public IEnumerable<TaskInList> ReadAll(IEnumerable<Task?> tasks)
    {
        return tasks.Select(task => new TaskInList() {Id = task!.Id , Alias = task.Alias , Description = task.Description , Status = task.Status});
    }
}
