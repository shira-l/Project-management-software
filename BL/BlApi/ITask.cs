
using BO;
using DalApi;
using DO;

namespace BlApi;

public interface ITask
{
   public int Create(BO.Task task); //Creates new task entity in BL
    public BO.Task? Read(int id); //Reads task entity by its ID
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null); //Reads all entity tasks
    public void Update(BO.Task task); //Updates task entity
    public void Delete(int id); //Deletes an task by its Id
}