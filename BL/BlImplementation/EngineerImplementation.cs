﻿

namespace BlImplementation;
using BlApi;
using BO;
using System.Text.RegularExpressions;
/// <summary>
/// The interface implementation of Engineer
/// </summary>
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public static bool RegexEmailCheck(string? input)
    {
        if (input == null)
            return true;
        // returns true if the input is a valid email
        return Regex.IsMatch(input, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    public int Create(BO.Engineer boEngineer)
    {
        try
        {
            if (boEngineer.Id < 0 || boEngineer.Name == "" || boEngineer.Cost < 0 || !RegexEmailCheck(boEngineer.Email))
            {
                throw new BO.BlInvalidValueExeption("Invalid or missing data input");
            }
            DO.Engineer doEngineer = new
                (boEngineer.Id, boEngineer.Cost, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level);
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }
    
    public void Delete(int id)
    {
        try
        {
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);
            Func<DO.Task, bool> filter = (DO.Task task) => id == task.EngineerId;
            IEnumerable<DO.Task?> doTasks = _dal.Task.ReadAll(filter);
            if (doTasks.Count() > 0)
            {
                throw new BO.BlCannotBeDeletedExeption($"Engineer with ID={id} performing a task");
            }
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalIsNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exists", ex);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exists");
        }
        return new BO.Engineer()
        {
            Id = id,
            Cost = doEngineer!.Cost,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience?)doEngineer.Level,
            Task = GetCurrentTask(id)
        };
    }


    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        IEnumerable<BO.Engineer?> ?engineers= (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Cost = doEngineer.Cost,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience?)doEngineer.Level,
                    Task = GetCurrentTask(doEngineer.Id)
                });
        return filter==null? engineers: engineers.Where(filter!);
    }

    public void Update(BO.Engineer boEngineer)
    {
        try
        {
            if (boEngineer.Task != null)
            {
                DO.Task? doTask = _dal.Task.Read(boEngineer.Task!.Id);
                if (doTask == null)
                {
                    throw new BO.BlDoesNotExistException($"Task with ID={boEngineer.Task!.Id} does not exists");
                }

                DO.Task? updateDoTask = doTask with { EngineerId = boEngineer.Id };
                _dal.Task.Update(updateDoTask);
            }
            if (boEngineer.Id < 0 || boEngineer.Name == "" || boEngineer.Cost < 0 || !RegexEmailCheck(boEngineer.Email))
            {
                        throw new BO.BlInvalidValueExeption("Invalid or missing data input");
            }
            
            DO.Engineer doEngineer = new
                (boEngineer.Id, boEngineer.Cost, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level);
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalIsNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does not exists", ex);
        }
    }
    private TaskInEngineer? GetCurrentTask(int id)
    {
        Func<DO.Task, bool> filter = (DO.Task task) => id == task.EngineerId;
        DO.Task? currentTask = _dal.Task.ReadAll(filter).LastOrDefault();
        if (currentTask == null)
            return new();
        TaskInEngineer curTask = new() { Id = currentTask.Id, Alias = currentTask.Alias };
        return curTask;
    }
}

