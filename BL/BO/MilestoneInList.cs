
namespace BO;
/// <summary>
/// The MilestoneInList entity represents the list of tasks that are a milestone
/// </summary>
/// <param name="Id">Personal unique ID of milestone</param>
/// <param name="Description">Milestone description</param>
/// <param name="Alias">Milestone Alias</param>
/// <param name="Status">Milestone status</param>
/// <param name="CompletionPercentage">Milestone completion percentage</param>
public class MilestoneInList
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public Status Status { get; init; }
    public double CompletionPercentage { get; set; }
}
