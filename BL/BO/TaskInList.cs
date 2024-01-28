
namespace BO;
/// <summary>
/// The TaskInList entity represents a list of tasks
/// </summary>
/// <param name="Id">Unique ID of task</param>
/// <param name="Description">Task description</param>
/// <param name="Alias">Task Alias</param>
/// <param name="Status">Task status</param>
public class TaskInList
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public Status Status { get; init; }
}
