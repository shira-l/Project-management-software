

namespace BO;

public class MilestoneInTask
{
    public int Id { get; init; }
    public string? Alias { get; set; }
    public MilestoneInTask(int Id, string ?Alias)
    {
        this.Id = Id;
        this.Alias = Alias;
    }
}

