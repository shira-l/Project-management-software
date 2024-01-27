
namespace BO;

/// <summary>
///
/// </summary>
/// <param name="Id">Personal unique ID of engineer</param>
/// <param name="Name">Private name of the engineer</param>
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
