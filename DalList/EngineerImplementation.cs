

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Threading.Tasks;
//The interface implementation of Engineer
public class EngineerImplementation : IEngineer
{
    public int Create(Engineer m_engineer)
    {
        if (DataSource.Engineers.Find(Engineer => Engineer.Id == m_engineer.Id)!=null)
            throw new Exception($"Engineer with ID={m_engineer.Id} already exists");
        DataSource.Engineers.Add(m_engineer);
        return m_engineer.Id; 
    }

    public void Delete(int id)
    {
        Engineer? engineer = DataSource.Engineers.Find(Engineer => Engineer.Id == id);
        if (engineer == null)
            throw new Exception($"Engineer with ID={id} is not exists");
        DataSource.Engineers.Remove(engineer);
    }

    public Engineer? Read(int id)
    {
        Engineer? engineer = DataSource.Engineers.Find(Engineer => Engineer.Id == id);
        return engineer;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer m_engineer)
    {
        Engineer? engineer = DataSource.Engineers.Find(Engineer => Engineer.Id == m_engineer.Id);
        if (engineer == null)
            throw new Exception($"Engineer with ID={m_engineer.Id} is not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(m_engineer);
    }
}
