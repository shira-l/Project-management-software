

namespace BlImplementation;
using BlApi;


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
            (boTask.Id, boTask.Engineer.Id, boTask.Description, boTask.Alias, true , boTask.IsActive, boTask.Remarks, (DO.DateTime?)boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, boTask.CompmlexityLevel);
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

    public BO.Task? Read(int id)
    {
        //DO.Task? doTask = _dal.Task.Read(id);
        //if (doTask == null)
        //    throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        //return new BO.Task()
        //{
        //    Id = id,
        //    EngineerId = doTask.EngineerId,
        //    Description = doTask.Description,
        //    Alias = doTask.Alias,
        //    Milestone = doTask.Milestone,
        //    IsActive = doTask.IsActive,
        //    Remarks = doTask.Remarks,
        //    CreateAtDate = doTask.CreateAtDate,
        //    ScheduleDate = doTask.ScheduleDate,
        //    ForecastDate = doTask.ForecastDate,
        //    DeadlineDate = doTask.DeadlineDate,
        //    ComplateDate = doTask.ComplateDate,
        //    CompmlexityLevel = doTask.CompmlexityLevel
        //};
        throw new NotImplementedException();
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
            (boTask.Id, boTask.Engineer.Id, boTask.Description, boTask.Alias, true, boTask.IsActive, boTask.Remarks, (DO.DateTime?)boTask.CreateAtDate, boTask.ScheduleDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.ComplateDate, boTask.CompmlexityLevel);
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
