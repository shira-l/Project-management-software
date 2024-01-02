
namespace BlImplementation;
using BlApi;
using BO;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public BO.Milestone Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
        {
            throw new BO.BlDoesNotExistException("The task does not exist");
        }
        return new BO.Milestone()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreateAtDate = doTask.CreateAtDate,
            Status = getStatusOfTask(doTask),
            ForecastDate = doTask.ForecastDate,
            DeadlineDate = doTask.DeadlineDate,
            ComplateDate = doTask.ComplateDate,
            Remarks = doTask.Remarks,
            dependencies = getPendingTasks(id).ToList(),
            CompletionPercentage = getCompletionPercentage(getPendingTasks(id))
        };
    }

    //?/////////////////////////////////////////////
    public BO.Milestone Update(BO.Milestone milestone)
    {
        if(milestone.Alias == "" || milestone.Remarks == "" ||  milestone.Description == "")
        {
            throw new BlInvalidValueExeption("Invalid Value");
        }
        return new BO.Milestone()
        {
            Alias = milestone.Alias,
            Remarks = milestone.Remarks,
            Description = milestone.Description
        };
    }

    // לחשוב איך לחשב?
    private double getCompletionPercentage(IEnumerable<BO.TaskInList> taskInLists)
    {
        return 5.6;
    }


    private IEnumerable<BO.TaskInList> getPendingTasks(int Id)
    {
        Func<DO.Dependency, bool> filterDependency = (DO.Dependency dependency) => Id == dependency.DependOnTask;
        IEnumerable<DO.Task> pendingTask = (from DO.Dependency? dependency in _dal.Dependency.ReadAll(filterDependency)
                                            select _dal.Task.Read(dependency.DependentTask));
        return (from DO.Task task in pendingTask
                select new BO.TaskInList()
                {
                    Id = task.Id,
                    Description = task.Description,
                    Alias = task.Alias,
                    Status = getStatusOfTask(task)
                });
    }

    private BO.Status getStatusOfTask(DO.Task task)
    {
        DateTime now = DateTime.Now;
        if (task.ScheduleDate == DateTime.MinValue)
            return BO.Status.Unscheduled;
        else if (task.StartDate == DateTime.MinValue)
            return BO.Status.Scheduled;
        else if (task.DeadlineDate < now && task.ComplateDate == DateTime.MinValue)
            return BO.Status.InJeopardy;
        else return BO.Status.OnTrack;
    }
}

