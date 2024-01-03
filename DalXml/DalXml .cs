using DalApi;
using System.Diagnostics;

namespace Dal;


sealed internal class DalXml : IDal
{
    public static DalXml Instance { get; } = new Lazy<DalXml>(() => new DalXml(), true).Value;
    private DalXml() { }
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
}

