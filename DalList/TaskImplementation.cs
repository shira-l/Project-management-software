

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

//The interface implementation of Task
internal class TaskImplementation : ITask
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
        Task? task= DataSource.Tasks.Where(_task => _task.Id == id).FirstOrDefault();
        if (task == null|| !task.IsActive)
            throw new DalDoesNotExistException($"Task with ID={id} is not exists");
        Task copy= task with { IsActive = false };
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(copy);
    }

    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.Where(_task => _task.Id == id).FirstOrDefault();
        if (task==null||!task!.IsActive)
            return null;
        return task;
    }

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }

    public void Update(Task m_task)
    {
        Task? task = Read(m_task.Id);
        if (task == null|| !task.IsActive)
            throw new DalDoesNotExistException($"Task with ID={m_task.Id} is not exists");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(m_task);
    }
    public void Reset()
    {
        DataSource.Tasks.Clear();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        Task? Task = DataSource.Tasks.Where(_task=>filter(_task)).FirstOrDefault();
        if (Task == null || !Task!.IsActive)
            return null;
        return Task;
    }
}
