
namespace BO;
/// <summary>
/// Task Entity
/// </summary>
/// <param name="Id">Unique ID of task</param>
/// <param name="Engineer">The task of the engineer</param>
/// <param name="Description">Task description</param>
/// <param name="Alias">Task Alias</param>
/// <param name="IsActive">The task's status</param>
/// <param name="Status">Task status</param>
/// <param name="Deliverables">The task Deliverables</param>
/// <param name="Remarks">Remarks about the task</param>
/// <param name="CreateAtDate">Task creation date</param>
/// <param name="StartDate">The task start date</param>
/// <param name="ScheduleDate">Planned date for completion of the task</param>
/// <param name="ForecastDate">Forecast updated date for the end of the mission</param>
/// <param name="DeadlineDate">Last date for completing the task</param>
/// <param name="ComplateDate">Actual task completion date</param>
/// <param name="CompmlexityLevel">The difficulty level of the task</param>
/// <param name="pendingTasks">The list of tasks that depend on the task</param>

public class Task
{
    public int Id { get; init; }
    public EngineerInTask? Engineer { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public bool IsActive { get; set; }
    public Status? Status { get; init; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public DateTime? CreateAtDate { get; set; }
    public DateTime? StartDate{ get; set; }
    public DateTime? ScheduleDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? ComplateDate { get; set; }
    public EngineerExperience? CompmlexityLevel { get; set; }
    public IEnumerable<TaskInList> ? PendingTasks { get; set; }
}
