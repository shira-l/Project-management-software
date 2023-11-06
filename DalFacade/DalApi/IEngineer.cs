

namespace DalApi;

using DO;

public interface IEngineer
{
    int Create(Engineer item); //Creates new Engineer object in DAL
    Engineer? Read(int id); //Reads Engineer object by its ID 
    List<Engineer> ReadAll(); // Reads all Engineer objects
    void Update(Engineer item); //Updates Engineer object
    void Delete(int id); //Deletes an object by its Id
    void Reset();//Resets the data list
}
