
namespace BO;

public class MilestoneInList
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public Status Status { get; init; }
    public double CompletionPercentage { get; set; }
}
