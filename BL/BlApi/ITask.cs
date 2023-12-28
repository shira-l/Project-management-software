
using BO;

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task task);
    public BO.Task? Read(int id);
    public BO.Task? Read(Func<BO.Task, bool> filter);
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null);
    public void Update(BO.Task task);
    public void Delete(int id);
    public EngineerInTask getEngineer(int EngineerId);
    public MilestoneInTask getMilestone(int Id);
    public TaskInList getpendingTasks(int Id);
}
