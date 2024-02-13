
namespace BlApi;

public interface IBl
{
    IEngineer Engineer { get; }
    ITask Task { get; }
    IEngineerInList EngineerInList { get; }
    ITaskInList TaskInList { get; }
}
