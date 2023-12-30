

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
        //catch (DO.DalAlreadyExistsException ex)
        //{
        //    throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        //}
        catch (Exception ex)
        {
            throw new Exception("Incorrect data", ex);
        }
    }

    public void Delete(int id)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(id);
            if (doTask == null)
            {
                //throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
            }
            IEnumerable<BO.TaskInList> pendingTasks = getPendingTasks(id);
            if(pendingTasks.Count() == 0)
            {
                //throw new BO.BlCannotBeDeletedExeption($"Task with ID={id} cannot be deleted");
            }
            
            _dal.Task.Delete(id);

         
            
        }
        //catch (BO.BlCannotBeDeletedExeption ex)
        //{
        //    //    throw new BO.BlAlreadyExistsException($"Task with ID={id} cannot be deleted", ex);
        //}
        catch (Exception ex)
        {
            throw new Exception("Incorrect data", ex);
        }
    }



    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
        {
            //throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        }
        return new BO.Task()
        {
            Id = id,
            Engineer = getEngineer(doTask!.EngineerId),
            Description = doTask.Description,
            Alias = doTask.Alias,
            Milestone = getMilestone(id),
            IsActive = doTask.IsActive,
            //איך לבחור status?
            Status = BO.Status.InJeopardy,
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
                    //איך לבחור status?
                    Status = BO.Status.InJeopardy,
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
        //catch (DO.DalAlreadyExistsException ex)
        //{
        //    throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        //}
        catch (Exception ex)
        {
            throw new Exception("Incorrect data", ex);
        }
    }
    public BO.EngineerInTask getEngineer(int EngineerId)
    {
        Func<DO.Engineer, bool> filter = (DO.Engineer engineer) => EngineerId == engineer.Id;
        DO.Engineer? engineer = _dal.Engineer.ReadAll(filter).FirstOrDefault();
        BO.EngineerInTask engineerInTask = new(EngineerId, engineer?.Name);
        return engineerInTask;
    }
//לבדוק milstone!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public BO.MilestoneInTask getMilestone(int Id)
    {
        Func<DO.Dependency, bool> filterDependency = (DO.Dependency dependency) => Id == dependency.Id;
        DO.Dependency? Dependency = _dal.Dependency.ReadAll(filterDependency).FirstOrDefault();
        Func<DO.Task, bool> filterTask = (DO.Task task) => Dependency.DependOnTask == task.Id;
        DO.Task? Task = _dal.Task.ReadAll(filterTask).FirstOrDefault();
        BO.MilestoneInTask milestoneInTask = new(Dependency.DependOnTask, Task.Alias);
        return milestoneInTask;
    }

    public IEnumerable<BO.TaskInList> getPendingTasks(int Id)
    {
        Func<DO.Dependency, bool> filterDependency = (DO.Dependency dependency) => Id == dependency.DependentTask;
        IEnumerable<DO.Task> dependencies = (from DO.Dependency? dependency in _dal.Dependency.ReadAll(filterDependency)
                                             select _dal.Task.Read(dependency.DependOnTask));
        return (from DO.Task task in dependencies
                select new BO.TaskInList()
                {
                    Id = task.Id,
                    Description = task.Description,
                    Alias = task.Alias,
                    Status = (BO.Status)(1)
                });

    }
}
