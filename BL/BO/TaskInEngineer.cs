namespace BO;

public class TaskInEngineer
{
    public int Id { get; init; }
    public string? Alias { get; set; }
    public TaskInEngineer(int Id , string? Alias)
    {
        this.Id = Id;
        this.Alias = Alias;
    }
}

