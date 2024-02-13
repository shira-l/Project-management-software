
namespace BO;

/// <summary>
/// The EngineerInTask entity represents all engineers performing a task 
/// </summary>
/// <param name="Id">Personal unique ID of engineer</param>
/// <param name="Name">Private name of the engineer</param>
public class EngineerInTask
{
    public int Id { get; init; }
    public string? Name { get; set; }
}
