
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID of task</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="Description">Task description</param>
/// <param name="Alias">Task Alias</param>
/// <param name="Milestone"></param
/// <param name="Deliverables">The task Deliverables</param>
/// <param name="IsActive">The task's status</param>
/// <param name="Remarks">Remarks about the task</param>
/// <param name="Start">The task start date</param>
/// <param name="ScheduleDate">Planned date for completion of the task</param>
/// <param name="ForecastDate">Forecast updated date for the end of the mission</param>
/// <param name="Deadline">Last date for completing the task</param>
/// <param name="Complate">Actual task completion date</param>
/// <param name="CompmlexityLevel">The difficulty level of the task</param>

public record Task
(
    int Id,
    int EngineerId,
    string? Description = null,
    string? Alias = null,
    bool Milestone = false,
     bool IsActive = true,
    string? Deliverables = null,
    string? Remarks = null,
    DateTime? CreateAtDate = null,
    DateTime? ScheduleDate = null,
    DateTime? ForecastDate = null,
    DateTime? DeadlineDate = null,
    DateTime? ComplateDate = null,
    EngineerExperience? CompmlexityLevel = null
)
{
    public Task() : this(0,0,null,null,false,true,null,null,null,null,null,null,null,null) { } //empty ctor for stage 3
    ///<summary>
    ///CreateAt - Creation a task Product date
    ///</summary>
    public DateTime StartDate => DateTime.Now; //get only
}
