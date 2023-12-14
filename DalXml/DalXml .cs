using DalApi;
using System.Diagnostics;

namespace Dal;


sealed internal class DalXml : IDal
{
    public static Lazy<DalXml> Instance { get; } = new Lazy<DalXml>(true);
    private DalXml() { }
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
}

