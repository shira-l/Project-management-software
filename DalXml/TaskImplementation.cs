
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
        List<Task>? lTask = XMLTools.LoadListFromXMLSerializer<Task>(XMLTASK);
        lTask?.Add(copy);
        XMLTools.SaveListToXMLSerializer<Task>(lTask!, XMLTASK);
        return id;
    }

    public void Delete(int id)
    {
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<Task> lTask = XMLTools.LoadListFromXMLSerializer<Task>(XMLTASK);
        Task? task = lTask?.Where(_task => _task!.Id == id).FirstOrDefault();
        if (task == null || !task.IsActive)
            throw new DalIsNotExistException($"Task with ID={id} is not exists");
        Task copy = task with { IsActive = false };
        lTask?.Remove(task);
        lTask?.Add(copy);
        XMLTools.SaveListToXMLSerializer<Task>(lTask!, XMLTASK);
    }

    public Task? Read(int id)
    {
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<Task> lTask = XMLTools.LoadListFromXMLSerializer<Task>(XMLTASK);
        Task? task = lTask.Where(_task => _task!.Id == id).FirstOrDefault();
        if (task == null || !task!.IsActive)
            return null;
        return task;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<Task> lTask = XMLTools.LoadListFromXMLSerializer<Task>(XMLTASK);
        Task? Task = lTask.Where(filter!).FirstOrDefault();
        if (Task == null || !Task!.IsActive)
            return null;
        return Task;
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<Task> lTask = XMLTools.LoadListFromXMLSerializer<Task>(XMLTASK);
        if (filter == null)
            return lTask.Select(item => item);
        else
            return lTask.Where(filter!);
    }

    public void Reset()
    {
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<Task> lTask = XMLTools.LoadListFromXMLSerializer<Task>(XMLTASK);
        lTask.Clear();
        XMLTools.SaveListToXMLSerializer<Task>(lTask!, XMLTASK);
    }

    public void Update(Task m_task)
    {
        const string XMLTASK = @"..\..\..\..\..\..\xml\task.xml";
        List<Task> lTask = XMLTools.LoadListFromXMLSerializer<Task>(XMLTASK);
        Task? task = Read(m_task.Id);
        if (task == null|| !task.IsActive)
            throw new DalIsNotExistException($"Task with ID={m_task.Id} is not exists");
        lTask.Remove(task);
        lTask.Add(m_task);
        XMLTools.SaveListToXMLSerializer<Task>(lTask!, XMLTASK);
    }
}
