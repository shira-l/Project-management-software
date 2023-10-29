
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID of task</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="Description">Task description</param>
/// <param name="Alias">Task Alias</param>
/// <param name="Milestone"></param>******!!!!!!!
/// <param name="Deliverables">The task Deliverables</param>
/// <param name="IsActive">The task's status</param>
/// <param name="Remarks">Remarks about the task</param>
/// <param name="Start">The task start date</param>
/// <param name="ForecastDate">Estimated date for completion of the task</param>
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
    DateTime? Start = null,
    DateTime? ForecastDate = null,
    DateTime? Deadline = null,
    DateTime? Complate = null,
    EngineerExperience? CompmlexityLevel=null
)
{
    ///<summary>
    ///CreateAt - Creation a task Product date
    ///</summary>
    public DateTime CreateAt => DateTime.Now; //get only
}
