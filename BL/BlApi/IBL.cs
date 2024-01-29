
namespace BlApi;

public interface IBl
{
    IEngineer Engineer { get; }
    ITask Task { get; }
    IMilestone Milestone { get; }
    ITaskInEngineer TaskInEngineer { get; }
}
