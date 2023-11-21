using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal;

using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;


internal class DependencyImplementation : IDependency
{
    public int Create(Dependency m_dependency)
    {
        int id = Config.NextTaskId;
        Dependency copy = m_dependency with { Id = id };
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\task.xml";
        XElement? lDependency = XMLTools.LoadListFromXMLElement("Dependency");
        lDependency.Add(copy);
        XMLTools.SaveListToXMLElement(lDependency, XMLDEPENDENCY);
        return id;
    }

    public void Delete(int id)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\engineer.xml";
        XElement? lDependency = XMLTools.LoadListFromXMLElement("Dependency");
        XElement? deleteDependency = lDependency.Elements().Where(dependency=> dependency.ToIntNullable("Id")==id).FirstOrDefault()??
          throw new DalIsNotExistException($"dependency with ID={id} is not exists");
        deleteDependency.Remove();
        XMLTools.SaveListToXMLElement(lDependency, XMLDEPENDENCY);
         
    }

    public Dependency? Read(int id)
    {
        XElement? lDependency = XMLTools.LoadListFromXMLElement("Dependency");
        XElement? dependency = lDependency.Elements().Where(dependency => dependency.ToIntNullable("Id") == id).FirstOrDefault();
        return dependency;
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
