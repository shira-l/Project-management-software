

namespace BlImplementation;
using BlApi;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        try
        {
            if (boTask.Id < 0 || boTask.Alias == "")
            {
                throw new Exception("Incorrect data");
            }
            DO.Task doTask = new DO.Task
            (boTask.Id, boTask.Engineer!.Id, boTask.Description, boTask.Alias, true, boTask.IsActive, boTask.Deliverables, boTask.Remarks, boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, (DO.EngineerExperience?)boTask.CompmlexityLevel);
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
            IEnumerable<BO.TaskInList> pendingTasks = getPendingTasks(id);
            if(pendingTasks.Count() == 0)
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
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        }
        return new BO.Task()
        {
            Id = id,
            Engineer = getEngineer(doTask!.EngineerId),
            Description = doTask.Description,
            Alias = doTask.Alias,
            Milestone = getMilestone(id),
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
            pendingTasks = getPendingTasks(doTask.Id)
        };
    }


    public IEnumerable<BO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll(filter)
                select new BO.Task
                {
                    Id = doTask.Id,
                    Engineer = getEngineer(doTask!.EngineerId),
                    Description = doTask.Description,
                    Alias = doTask.Alias,
                    Milestone = getMilestone(doTask.Id),
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
                    pendingTasks = getPendingTasks(doTask.Id)
                });
    }

    public void Update(BO.Task boTask)
    {
        try
        {
            if (boTask.Id < 0 || boTask.Alias == "")
            {
                throw new Exception("Incorrect data");
            }
            DO.Task doTask = new DO.Task
            (boTask.Id, boTask.Engineer!.Id, boTask.Description, boTask.Alias, true, boTask.IsActive, boTask.Deliverables, boTask.Remarks, boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, (DO.EngineerExperience?)boTask.CompmlexityLevel);
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
        BO.EngineerInTask engineerInTask = new(EngineerId, engineer?.Name);
        return engineerInTask;
    }
//לבדוק milstone!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private BO.MilestoneInTask getMilestone(int Id)
    {
        Func<DO.Dependency, bool> filterDependency = (DO.Dependency dependency) => Id == dependency.DependentTask;
        IEnumerable<DO.Task?>? dependencies = (from DO.Dependency? dependency in _dal.Dependency.ReadAll(filterDependency)
                                               select _dal.Task.Read(dependency.DependOnTask));
        if(dependencies==null)
        {
            ///צריך לחשוב מה אמורים לעשות
        }
        DO.Task? task = dependencies!.Where(task => task!.Milestone == true).FirstOrDefault();
        if(task==null)
        {
            ///צריך לחשוב מה אמורים לעשות
        }
        return new BO.MilestoneInTask() 
        {
            Id = task!.Id,
        Alias = task.Alias
        };
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
