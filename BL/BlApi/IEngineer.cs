

namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer engineer);
    public BO.Engineer? Read(int id);
    public BO.Engineer? Read(Func<BO.Engineer, bool> filter);
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null);
    public void Update(BO.Engineer engineer);
    public void Delete(int id); 
}
