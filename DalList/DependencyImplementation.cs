
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
//The interface implementation of Dependency
internal class DependencyImplementation : IDependency
{
    public int Create(Dependency m_dependency)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = m_dependency with { Id = id };
        DataSource.Dependencys.Add(copy);
        return id;

    }

    public void Delete(int id)
    {
        Dependency? dependency = (from _dependency in DataSource.Dependencys
                                  where (_dependency.Id == id)
                                  select _dependency).First();
        if (dependency == null)
            throw new DalDoesNotExistException($"dependency with ID={id} is not exists");
        DataSource.Dependencys.Remove(dependency);
    }

    public Dependency? Read(int id)
    {
        Dependency? dependency = (from _dependency in DataSource.Dependencys
                                  where (_dependency.Id == id)
                                  select _dependency).First();
        return dependency;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Dependencys.Select(item => item);
        else
            return DataSource.Dependencys.Where(filter);
    }

    public void Update(Dependency m_dependency)
    {
        Dependency? dependency = Read(m_dependency.Id);
        if (dependency == null)
            throw new DalDoesNotExistException($"Dependency with ID={m_dependency.Id} is not exists");
        DataSource.Dependencys.Remove(dependency);
        DataSource.Dependencys.Add(m_dependency);
    }
    public void Reset()
    {
        DataSource.Dependencys.Clear(); 
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? dependency = (from _dependency in DataSource.Dependencys
                                  where (filter(_dependency))
                                  select _dependency).First();
        return dependency;
    }
}
