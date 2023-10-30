
namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

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
        throw new Exception("a Dependency object cannot be deleted");
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

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
