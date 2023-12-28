

using BO;

namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer engineer);
    public BO.Engineer? Read(int id);
    public BO.Engineer? Read(Func<DO.Engineer, bool> filter);
    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer, bool>? filter = null);
    public void Update(BO.Engineer engineer);
    public void Delete(int id);
    public TaskInEngineer? GetCurrentTask(int id);
}
