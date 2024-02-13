
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DO;
/// <summary>
/// Task Entity
/// </summary>
/// <param name="Id">Unique ID of task</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="Description">Task description</param>
/// <param name="Alias">Task Alias</param>
/// <param name="Deliverables">The task Deliverables</param>
/// <param name="IsActive">The task's status</param>
/// <param name="Remarks">Remarks about the task</param>
/// <param name="CreateAtDate">The task start date</param>
/// <param name="ScheduleDate">Planned date for completion of the task</param>
/// <param name="ForecastDate">Forecast updated date for the end of the mission</param>
/// <param name="DeadlineDate">Last date for completing the task</param>
/// <param name="ComplateDate">Actual task completion date</param>
/// <param name="CompmlexityLevel">The difficulty level of the task</param>

public record Task
(
    int Id,
    int EngineerId,
    string? Description = null,
    string? Alias = null,
    bool IsActive = true,
    string? Deliverables = null,
    string? Remarks = null,
    DateTime? StartDate = null,
    DateTime? ScheduleDate = null,
    DateTime? ForecastDate = null,
    DateTime? DeadlineDate = null,
    DateTime? ComplateDate = null,
    EngineerExperience? CompmlexityLevel = null
)
{
    public Task() : this(0,0,null,null,true,null,null,null,null,null,null,null,null) { } //empty ctor for stage 3
    ///<summary>
    ///CreateAtDate - Creation a task Product date
    ///</summary>
    public DateTime CreateAtDate => DateTime.Now; //get only
}
