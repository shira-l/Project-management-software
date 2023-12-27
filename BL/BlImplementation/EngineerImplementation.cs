﻿

namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public static bool RegexEmailCheck(string input)
    {
        // returns true if the input is a valid email
        return Regex.IsMatch(input, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    public int Create(BO.Engineer boEngineer)
    {
        try
        {
            if (boEngineer.Id < 0 || boEngineer.Name == "" || boEngineer.Cost < 0 || !RegexEmailCheck(boEngineer.Email))
            {
                throw new Exception("Incorrect data");
            }
            DO.Engineer doEngineer = new DO.Engineer
                (boEngineer.Id, boEngineer.Cost, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level);
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        //catch (DO.DalAlreadyExistsException ex)
        //{
        //    throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        //}
        catch (Exception ex)
        {
            throw new Exception("Incorrect data",ex);
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
                throw new Exception("Engineer in task");
            }
            _dal.Engineer.Delete(id);
        }
        //catch (DO.DalIsNotExistException ex)
        //{
        //    throw new BO.DalIsNotExistException($"Engineer with ID={id} is not exists",ex);
        //}
        catch (Exception ex)
        {
            throw new Exception("Engineer in task",ex);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
        {
            //throw new BO.BlAlreadyExistsException($"Engineer with ID={id} is not exists");
        }
        return new BO.Engineer()
        {
            Id = id,
            Cost = doEngineer.Cost,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience?)doEngineer.Level,
            TaskInEngineer = ToBOTask(GetCurrentTask(id),id, doEngineer.Name)
        };
    }

    public BO.Engineer? Read(Func<DO.Engineer, bool> filter)
    {
        DO.Engineer? doEngineer = _dal.Engineer.ReadAll(filter).FirstOrDefault();
        return new BO.Engineer()
        {
            Id = doEngineer.Id,
            Cost = doEngineer.Cost,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience?)doEngineer.Level,
            TaskInEngineer = ToBOTask(GetCurrentTask(doEngineer.Id), doEngineer.Id, doEngineer.Name)
        };
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Cost = doEngineer.Cost,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience?)doEngineer.Level,
                    TaskInEngineer = ToBOTask(GetCurrentTask(doEngineer.Id), doEngineer.Id, doEngineer.Name)

                });
    }

    public void Update(BO.Engineer boEngineer)
    {
        try
        {
            if (boEngineer.Id < 0 || boEngineer.Name == "" || boEngineer.Cost < 0 || !RegexEmailCheck(boEngineer.Email))
            {
                throw new Exception("Incorrect data");
            }
            DO.Engineer doEngineer = new DO.Engineer
                (boEngineer.Id, boEngineer.Cost, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience?)boEngineer.Level);
            _dal.Engineer.Update(doEngineer);
        }
        //catch (DO.DalIsNotExistException ex)
        //{
        //    throw new BO.DalIsNotExistException($"Engineer with ID={boEngineer.Id} is not exists", ex);
        //}
        catch (Exception ex)
        {
            throw new Exception("Incorrect data",ex);
        }
    }

    public DO.Task? GetCurrentTask(int id)
    {
        Func<DO.Task, bool> filter = (DO.Task task) => id == task.EngineerId;
        DO.Task? currentTask = _dal.Task.ReadAll(filter).LastOrDefault();
        return currentTask;
    }
    public BO.Task? ToBOTask(DO.Task? task,int id,string? name)
    {
        if (task == null)
            return null;
        EngineerInTask EITask = new(id, name);
        MilestoneInTask MTask = new(task.Id, task.Alias);
        Status status = (Status)(1);
        BO.Task boTask = new(task.Id, EITask, task.Description, task.Alias, MTask, task.IsActive, status, task.Deliverables,task.Remarks,task.CreateAtDate,task.StartDate, task.ScheduleDate,task.ForecastDate,task.DeadlineDate,task.ComplateDate,task.CompmlexityLevel );
        //foreach (var prop in task.GetType().GetProperties())
        //{
        //    string name = prop.Name;
        //    boTask = prop.GetValue(task);
        //}
        return boTask;
    }
}

