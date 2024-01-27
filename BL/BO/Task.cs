
namespace BO;
/// <summary>
/// 
/// </summary>

public class Task
{
    public int Id { get; init; }
    public EngineerInTask? Engineer { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public MilestoneInTask? Milestone { get; set; }
    public bool IsActive { get; set; }
    public Status Status { get; init; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public DateTime? CreateAtDate { get; set; }
    public DateTime? StartDate{ get; set; }
    public DateTime? ScheduleDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? ComplateDate { get; set; }
    public EngineerExperience? CompmlexityLevel { get; set; }
    public IEnumerable<TaskInList> ? pendingTasks { get; set; }
}
