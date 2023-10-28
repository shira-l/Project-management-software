

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task m_task)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = m_task with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        Task? task= DataSource.Tasks.Find(Task => Task.Id == id);
        if (task == null)
            throw new Exception($"Task with ID={id} is not exists");
        Task copy= task with { IsActive = false };
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(copy);
    }

    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.Find(Task => Task.Id == id);
        return task;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task m_task)
    {
        Task? task = DataSource.Tasks.Find(Task => Task.Id == m_task.Id);
        if (task == null)
            throw new Exception($"Task with ID={m_task.Id} is not exists");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(m_task);
    }
}
