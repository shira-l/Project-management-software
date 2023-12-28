
namespace BO;

public class Task
{
    public int Id { get; init; }
    public EngineerInTask Engineer { get; init; }
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
    public IEnumerable<Task> ? pendingTasks { get; set; }
    public Task(int Id, EngineerInTask Engineer,string Description ,string Alias , MilestoneInTask Milestone,bool IsActive, Status Status ,string Deliverables,string Remarks,DateTime CreateAtDate,DateTime StartDate, DateTime ScheduleDate,DateTime ForecastDate,DateTime DeadlineDate,DateTime ComplateDate,EngineerExperience CompmlexityLevel,IEnumerable<Task> pendingTasks)
    {
        this.Id = Id;
        this.Engineer = Engineer;
        this.Description = Description;
        this.Alias = Alias;
        this.Milestone = Milestone;
        this.IsActive = IsActive;
        this.Status = Status;
        this.Deliverables = Deliverables;
        this.Remarks = Remarks;
        this.CreateAtDate = CreateAtDate;
        this.StartDate = StartDate;
        this.ScheduleDate = ScheduleDate;
        this.ForecastDate = ForecastDate;
        this.DeadlineDate = DeadlineDate;
        this.ComplateDate = ComplateDate;
        this.CompmlexityLevel = CompmlexityLevel;
        this.pendingTasks = pendingTasks;
    }
}
