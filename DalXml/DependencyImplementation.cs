using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal;

using DalApi;
using DO;


internal class DependencyImplementation : IDependency
{
    public int Create(Dependency m_dependency)
    {
        int id = Config.NextTaskId;
        Dependency copy = m_dependency with { Id = id };
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\task.xml";
        List<DO.Dependency>? lDependency = XMLTools.LoadListFromXMLSerializer<Dependency>(XMLDEPENDENCY);
        lDependency.Add(copy);
        XMLTools.SaveListToXMLSerializer<Dependency>(lDependency, XMLDEPENDENCY);
        return id;
    }

    public void Delete(int id)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\engineer.xml";
        List<DO.Dependency>? lDependency = XMLTools.LoadListFromXMLSerializer<Dependency>(XMLDEPENDENCY);
        Dependency? dependency = lDependency.Where(_dependency => _dependency.Id == id).FirstOrDefault() ??
          throw new DalIsNotExistException($"dependency with ID={id} is not exists");
        lDependency.Remove(dependency);
        XMLTools.SaveListToXMLSerializer<Dependency>(lDependency, XMLDEPENDENCY);
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
