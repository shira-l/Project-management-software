

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
//The interface implementation of Engineer
internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer m_engineer)
    {
        Engineer? engineer = Read(m_engineer.Id);
        if(engineer!=null)
            throw new DalAlreadyExistsException($"Engineer with ID={m_engineer.Id} already exists");
        DataSource.Engineers.Add(m_engineer);
        return m_engineer.Id; 
    }

    public void Delete(int id)
    {
        Engineer? engineer = DataSource.Engineers.Where(_engineer => _engineer.Id == id).FirstOrDefault()??
         throw new DalIsNotExistException($"Engineer with ID={id} is not exists");
        DataSource.Engineers.Remove(engineer);
    }

    public Engineer? Read(int id)
    {
        Engineer? engineer = DataSource.Engineers.Where(_engineer=>_engineer.Id == id).FirstOrDefault();
        return engineer;
    }
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    public void Update(Engineer m_engineer)
    {
        Engineer? engineer = Read(m_engineer.Id)??
        throw new DalIsNotExistException($"Engineer with ID={m_engineer.Id} is not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(m_engineer);
    }
    public void Reset()
    {
        DataSource.Engineers.Clear();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        Engineer? Engineer = DataSource.Engineers.Where(_engineer => filter(_engineer)).FirstOrDefault();
        return Engineer;
    }
}
