
namespace BO;

public class EngineerInTask
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public EngineerInTask(int Id,string? Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}
