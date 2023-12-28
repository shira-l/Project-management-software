

namespace BlImplementation;
using BlApi;
using BO;
using DO;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        try
        {
            if(boTask.Id < 0 || boTask.Alias == "")
            {
                throw new Exception("Incorrect data");
            }
            DO.Task doTask = new DO.Task
            (boTask.Id, boTask.Engineer.Id, boTask.Description, boTask.Alias, true , boTask.IsActive, boTask.Deliverables,boTask.Remarks, boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, (DO.EngineerExperience?)boTask.CompmlexityLevel);
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
        
    }

    public EngineerInTask getEngineer(int EngineerId)
    {
        Func<DO.Engineer, bool> filter = (DO.Engineer engineer) => EngineerId == engineer.Id;
        DO.Engineer? engineer = _dal.Engineer.ReadAll(filter).FirstOrDefault();
        EngineerInTask engineerInTask = new(EngineerId, engineer.Name);
        return engineerInTask;
    }

    public MilestoneInTask getMilestone(int Id)
    {
        Func<DO.Dependency, bool> filterDependency = (DO.Dependency dependency) => Id == dependency.Id;
        DO.Dependency? Dependency = _dal.Dependency.ReadAll(filterDependency).FirstOrDefault();
        Func<DO.Task, bool> filterTask = (DO.Task task) => Dependency.DependOnTask == task.Id;
        DO.Task? Task = _dal.Task.ReadAll(filterTask).FirstOrDefault();
        MilestoneInTask milestoneInTask = new(Dependency.DependOnTask, Task.Alias);
        return milestoneInTask;
    }

    public TaskInList getpendingTasks(int Id)
    {
        throw new NotImplementedException();
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
            Engineer = getEngineer(doTask.EngineerId),
            Description = doTask.Description,
            Alias = doTask.Alias,
            Milestone = getMilestone(id),
            IsActive = doTask.IsActive,
            //איך לבחור status?
            Status = Status.InJeopardy,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            CreateAtDate = doTask.CreateAtDate,
            //לעדכן תאריך
            StartDate = DateTime.Now,
            ScheduleDate = doTask.ScheduleDate,
            ForecastDate = doTask.ForecastDate,
            DeadlineDate = doTask.DeadlineDate,
            ComplateDate = doTask.ComplateDate,
            CompmlexityLevel = (BO.EngineerExperience)doTask.CompmlexityLevel,
            pendingTasks = null
        };
    }

    public BO.Task? Read(Func<BO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        //return (from DO.Task doTask in _dal.Task.ReadAll()
        //        select new BO.Task
        //        {
        //            Id = id,
        //            EngineerId = doTask.EngineerId,
        //            Description = doTask.Description,
        //            Alias = doTask.Alias,
        //            Milestone = doTask.Milestone,
        //            IsActive = doTask.IsActive,
        //            Remarks = doTask.Remarks,
        //            CreateAtDate = doTask.CreateAtDate,
        //            ScheduleDate = doTask.ScheduleDate,
        //            ForecastDate = doTask.ForecastDate,
        //            DeadlineDate = doTask.DeadlineDate,
        //            ComplateDate = doTask.ComplateDate,
        //            CompmlexityLevel = doTask.CompmlexityLevel
        //        });
        throw new NotImplementedException();
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
            (boTask.Id, boTask.Engineer.Id, boTask.Description, boTask.Alias, true, boTask.IsActive,boTask.Deliverables, boTask.Remarks, boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, (DO.EngineerExperience?)boTask.CompmlexityLevel);
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
}
