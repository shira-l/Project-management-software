
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
        Dependency? dependency = DataSource.Dependencys.Where(_dependency => _dependency.Id == id).FirstOrDefault()??
        throw new DalIsNotExistException($"dependency with ID={id} is not exists");
        DataSource.Dependencys.Remove(dependency);
    }

    public Dependency? Read(int id)
    {
        Dependency? dependency = DataSource.Dependencys.Where(_dependency => _dependency.Id == id).FirstOrDefault();
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
        Dependency? dependency = Read(m_dependency.Id) ?? throw new DalIsNotExistException($"Dependency with ID={m_dependency.Id} is not exists");
        DataSource.Dependencys.Remove(dependency);
        DataSource.Dependencys.Add(m_dependency);
    }
    public void Reset()
    {
        DataSource.Dependencys.Clear(); 
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? dependency = DataSource.Dependencys.Where(_dependency => filter(_dependency)).FirstOrDefault();
        return dependency;
    }
}
