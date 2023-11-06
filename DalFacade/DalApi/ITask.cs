
namespace DalApi;

using DO;

public interface ITask
{
    int Create(Task item); //Creates new Task object in DAL
    Task? Read(int id); //Reads Task object by its ID 
    List<Task> ReadAll(); //Reads all Task objects
    void Update(Task item); //Updates Task object
    void Delete(int id); //Deletes an object by its Id
    void Reset();//Resets the data list
}
