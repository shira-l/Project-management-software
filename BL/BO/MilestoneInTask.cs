

namespace BO;
/// <summary>
/// The MilestoneInTask entity represents a milestone task in progress
/// </summary>
/// <param name="Id">Personal unique ID of milestone</param>
/// <param name="Alias">Milestone Alias</param>
public class MilestoneInTask
{
    public int Id { get; init; }
    public string? Alias { get; set; }
}

