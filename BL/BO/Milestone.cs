
namespace BO;
/// <summary>
///
/// </summary>
/// <param name="Id">Personal unique ID of milestone</param>
/// <param name="Description">Milestone description</param>
/// <param name="Alias">Milestone Alias</param>
/// <param name="CreateAtDate">Milestone creation date</param>
/// <param name="Status">Milestone status</param>
/// <param name="ForecastDate">Milestone forecast date</param>
/// <param name="DeadlineDate">Deadline for completing the milestone</param>
/// <param name="ComplateDate">Actual milestone completion date</param>
/// <param name="Remarks">Remarks about the milestone</param>
/// <param name="dependencies">The list of tasks that depend on a milestone</param>
/// <param name="CompletionPercentage">Milestone completion percentage</param>
public class Milestone
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreateAtDate { get; init; }
    public Status Status { get; init; }
    public DateTime? ForecastDate { get; init; }
    public DateTime? DeadlineDate { get; init; }
    public DateTime? ComplateDate { get; init; }
    public string? Remarks { get; set; }
    public List<TaskInList>? dependencies { get; init; }
    public double CompletionPercentage { get; init; }

}
