
namespace Dal;

using DalApi;
using DO;
using System.Xml.Linq;


internal class DependencyImplementation : IDependency
{
    public int Create(Dependency m_dependency)
    {
        int id = Config.NextTaskId;
        Dependency copy = m_dependency with { Id = id };
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies";
        XElement? lDependency = XMLTools.LoadListFromXMLElement("Dependency");
        XElement Ecopy = GetXElement(copy);
        lDependency.Add(Ecopy);
        XMLTools.SaveListToXMLElement(lDependency, XMLDEPENDENCY);
        return id;
    }

    public void Delete(int id)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies";
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
        if (dependency == null)
            return null;
        Dependency? Rdependency = getDependency(dependency);
            return Rdependency;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement? lDependency = XMLTools.LoadListFromXMLElement("Dependency");
        Dependency? dependency = lDependency.Elements().Select(d => getDependency(d)).Where(filter!).FirstOrDefault();
        return dependency;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter == null)
            return XMLTools.LoadListFromXMLElement("Dependency").Elements().Select(d => getDependency(d));
        else
            return XMLTools.LoadListFromXMLElement("Dependency").Elements().Select(d => getDependency(d)).Where(filter!);
    }

    public void Reset()
    {
        XElement? lDependency = XMLTools.LoadListFromXMLElement("Dependency");
        foreach (XElement dependency in lDependency.Elements())
            dependency.Remove();
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies";
        XMLTools.SaveListToXMLElement(lDependency, XMLDEPENDENCY);
    }

    public void Update(Dependency m_dependency)
    {
        XElement? lDependency = XMLTools.LoadListFromXMLElement("Dependency");
        XElement? dependency = lDependency.Elements().Where(dependency => dependency.ToIntNullable("Id") == m_dependency.Id).FirstOrDefault() ?? 
            throw new DalIsNotExistException($"Dependency with ID={m_dependency.Id} is not exists");
        dependency.Remove();
        lDependency.Add(m_dependency);
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies";
        XMLTools.SaveListToXMLElement(lDependency, XMLDEPENDENCY);
    }
    public static Dependency? getDependency(XElement dependency) =>
        dependency.ToIntNullable("Id") is null ? null : new Dependency()
        {
            Id = (int)dependency.Element("Id")!,
            DependentTask = (int)dependency.Element("DependentTask")!,
            DependOnTask = (int)dependency.Element("DependentTask")!
        };
  
    public static XElement GetXElement(Dependency m_dependency)
    {
        XElement Edependency = new XElement("Dependency");
        foreach (var prop in m_dependency.GetType().GetProperties())
        {
            XElement el = new XElement(prop.Name, m_dependency.GetType().GetProperty(prop.Name)?.GetValue(m_dependency));
            Edependency.Add(el);
        }

        return Edependency;

    }

}
