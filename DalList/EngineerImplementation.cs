

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Threading.Tasks;
//The interface implementation of Engineer
internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer m_engineer)
    {
        Engineer engineer = (from _engineer in DataSource.Engineers
                             where (_engineer.Id == m_engineer.Id)
                             select _engineer).First();
        if (engineer != null)
            throw new DalAlreadyExistsException($"Engineer with ID={m_engineer.Id} already exists");
        DataSource.Engineers.Add(m_engineer);
        return m_engineer.Id; 
    }

    public void Delete(int id)
    {
        Engineer? engineer = (from _engineer in DataSource.Engineers
                              where (_engineer.Id == id)
                              select _engineer).First();
        if (engineer == null)
            throw new DalDoesNotExistException($"Engineer with ID={id} is not exists");
        DataSource.Engineers.Remove(engineer);
    }

    public Engineer? Read(int id)
    {
        Engineer? engineer = (from _engineer in DataSource.Engineers
                              where (_engineer.Id == id)
                              select _engineer).First();
        return engineer;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    public void Update(Engineer m_engineer)
    {
        Engineer? engineer = (from _engineer in DataSource.Engineers
                              where (_engineer.Id == m_engineer.Id)
                              select _engineer).First();
        if (engineer == null)
            throw new DalDoesNotExistException($"Engineer with ID={m_engineer.Id} is not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(m_engineer);
    }
    public void Reset()
    {
        DataSource.Engineers.Clear();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        Engineer? Engineer = (from _Engineer in DataSource.Engineers
                              where (filter(_Engineer))
                                  select _Engineer).First();
        return Engineer;
    }
}
