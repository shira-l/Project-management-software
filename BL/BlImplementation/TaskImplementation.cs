

namespace BlImplementation;
using BlApi;
using BO;
/// <summary>
/// The interface implementation of Task
/// </summary>
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        try
        {
            if (boTask.Alias == "")
            {
                throw new BO.BlInvalidValueExeption("Invalid or missing data input");
            }
            if (boTask.CreateAtDate > boTask.StartDate || boTask.ScheduleDate > boTask.ForecastDate || boTask.ForecastDate < boTask.StartDate || boTask.ForecastDate > boTask.DeadlineDate || boTask.ComplateDate != null)
            {
                throw new BO.BlIncorrectDateOrderExeption("Invalid or missing data input");
            }
            DO.Task doTask = new 
            (boTask.Id, boTask.Engineer!.Id, boTask.Description, boTask.Alias, boTask.IsActive, boTask.Deliverables, boTask.Remarks, boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, (DO.EngineerExperience?)boTask.CompmlexityLevel);
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        try
        {
            IEnumerable<BO.TaskInList> PendingTasks = getPendingTasks(id);
            if(PendingTasks.Count() == 0)
            {
                throw new BO.BlCannotBeDeletedExeption($"Task with ID={id} cannot be deleted");
            }
            
            _dal.Task.Delete(id);
        }
        catch (DO.DalIsNotExistException ex)
        {
               throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", ex);
        }
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
        return new BO.Task()
        {
            Id = id,
            Engineer = getEngineer(doTask!.EngineerId),
            Description = doTask.Description,
            Alias = doTask.Alias,
            IsActive = doTask.IsActive,
            Status = getStatusOfTask(doTask),
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            CreateAtDate = doTask.CreateAtDate,
            StartDate = doTask.StartDate,
            ScheduleDate = doTask.ScheduleDate,
            ForecastDate = doTask.ForecastDate,
            DeadlineDate = doTask.DeadlineDate,
            ComplateDate = doTask.ComplateDate,
            CompmlexityLevel = (BO.EngineerExperience?)doTask.CompmlexityLevel,
            PendingTasks = getPendingTasks(doTask.Id)
        };
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task?>? tasks=(from DO.Task doTask in _dal.Task.ReadAll()
                select new BO.Task
                {
                    Id = doTask.Id,
                    Engineer = getEngineer(doTask!.EngineerId),
                    Description = doTask.Description,
                    Alias = doTask.Alias,
                    IsActive = doTask.IsActive,
                    Status = getStatusOfTask(doTask),
                    Deliverables = doTask.Deliverables,
                    Remarks = doTask.Remarks,
                    CreateAtDate = doTask.CreateAtDate,
                    StartDate = doTask.StartDate,
                    ScheduleDate = doTask.ScheduleDate,
                    ForecastDate = doTask.ForecastDate,
                    DeadlineDate = doTask.DeadlineDate,
                    ComplateDate = doTask.ComplateDate,
                    CompmlexityLevel = (BO.EngineerExperience?)doTask.CompmlexityLevel,
                    PendingTasks = getPendingTasks(doTask.Id)
                });
        return filter==null?tasks:tasks.Where(filter!);
    }

    public void Update(BO.Task boTask)
    {
        try
        {
            //מחקתי id
            if (boTask.Alias == "")
            {
                throw new BO.BlInvalidValueExeption("Invalid or missing data input");
            }
            /// לבדוק תאריכים
            if(boTask.CreateAtDate > boTask.StartDate || boTask.ScheduleDate > boTask.ForecastDate || boTask.ForecastDate < boTask.StartDate || boTask.ForecastDate > boTask.DeadlineDate)
            {
                throw new BO.BlIncorrectDateOrderExeption("Invalid or missing data input");
            }
            DO.Task doTask = new DO.Task
            (boTask.Id, boTask.Engineer!.Id, boTask.Description, boTask.Alias, boTask.IsActive, boTask.Deliverables, boTask.Remarks, boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, (DO.EngineerExperience?)boTask.CompmlexityLevel);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }
    private BO.EngineerInTask getEngineer(int EngineerId)
    {
        Func<DO.Engineer, bool> filter = (DO.Engineer engineer) => EngineerId == engineer.Id;
        DO.Engineer? engineer = _dal.Engineer.ReadAll(filter).FirstOrDefault();
        BO.EngineerInTask engineerInTask = new() {Id= EngineerId,Name= engineer?.Name };
        return engineerInTask;
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
