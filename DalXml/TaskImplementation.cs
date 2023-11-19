
namespace Dal;

using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class TaskImplementation : ITask
{
    public int Create(Task m_task)
    {
        int id = Config.NextTaskId;
        Task copy = m_task with { Id = id };
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<DO.Task?>? lTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(XMLTASK);
        lTask?.Add(copy);
        XMLTools.SaveListToXMLSerializer<DO.Task>(lTask, XMLTASK);
        return id;
    }

    public void Delete(int id)
    {
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<DO.Task?> lTask = XMLTools.LoadListFromXMLSerializer<DO.Task>(XMLTASK);
        Task? task = lTask?.Where(_task => _task.Id == id).FirstOrDefault();
        if (task == null || !task.IsActive)
            throw new DalIsNotExistException($"Task with ID={id} is not exists");
        Task copy = task with { IsActive = false };
        lTask?.Remove(task);
        lTask?.Add(copy);
        XMLTools.SaveListToXMLSerializer<DO.Task>(lTask, XMLTASK);
    }

    public Task? Read(int id)
    {

        throw new NotImplementedException();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
