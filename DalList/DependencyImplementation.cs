
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
//The interface implementation of Dependency
public class DependencyImplementation : IDependency
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
        Dependency? dependency = DataSource.Dependencys.Find(Dependency => Dependency.Id == id);
        if (dependency == null)
            throw new Exception($"dependency with ID={id} is not exists");
        DataSource.Dependencys.Remove(dependency);
    }

    public Dependency? Read(int id)
    {
        Dependency? dependency = DataSource.Dependencys.Find(Dependency => Dependency.Id == id);
        return dependency;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    public void Update(Dependency m_dependency)
    {
        Dependency? dependency = DataSource.Dependencys.Find(Dependency => Dependency.Id == m_dependency.Id);
        if (dependency == null)
            throw new Exception($"Dependency with ID={m_dependency.Id} is not exists");
        DataSource.Dependencys.Remove(dependency);
        DataSource.Dependencys.Add(m_dependency);
    }
    public void Reset()
    {
        DataSource.Dependencys.Clear(); 
    }
}
