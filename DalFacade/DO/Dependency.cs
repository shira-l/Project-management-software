
namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Unique ID number</param>
/// <param name="DependentTask">ID number of pending task</param>
/// <param name="DependOnTask">ID number of a previous assignment</param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependOnTask
)
{
    public Dependency() : this(0,0,0) { } //empty ctor for stage 3
}
     
