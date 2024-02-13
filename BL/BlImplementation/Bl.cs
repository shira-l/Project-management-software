namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public IEngineer Engineer =>  new EngineerImplementation();
    public IEngineerInList EngineerInList => new EngineerInListImplementation();
    public ITaskInList TaskInList => new TaskInListImplementation();
    public ITask Task => new TaskImplementation();
}

