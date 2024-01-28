using System.Xml.Linq;

namespace BO;
/// <summary>
/// The TaskInEngineer entity represents engineer tasks
/// </summary>
/// <param name="Id">Unique ID of task</param>
/// <param name="Alias">Task Alias</param>
public class TaskInEngineer
{
    public int Id { get; init; }
    public string? Alias { get; set; }
    public override string ToString()
    {
        return $"{Id} {Alias}";
    }
}

