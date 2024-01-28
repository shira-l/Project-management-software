

using BO;

namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer engineer);//Creates new engineer entity in BL
    public BO.Engineer? Read(int id);//Reads engineer entity by its ID 
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null);// Reads all  entity engineers
    public void Update(BO.Engineer engineer);//Updates engineer entity
    public void Delete(int id);//Deletes an engineer by its Id
}
