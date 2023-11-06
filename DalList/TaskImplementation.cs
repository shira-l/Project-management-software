

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
        Task? task= (from _task in DataSource.Tasks
                     where (_task.Id == id)
                     select _task).First();
        if (task == null)
            throw new DalDoesNotExistException($"Task with ID={id} is not exists");
        if(!task.IsActive)
                throw new DalIsinactiveException($"Task with ID={id} is  inactive");
        Task copy= task with { IsActive = false };
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(copy);
    }

    public Task? Read(int id)
    {
        Task? task = (from _task in DataSource.Tasks
                      where (_task.Id == id)
                      select _task).First();
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
        Task? task = (from _task in DataSource.Tasks
                      where (_task.Id == m_task.Id)
                      select _task).First();
        if (task == null)
            throw new DalDoesNotExistException($"Task with ID={m_task.Id} is not exists");
        if (!task.IsActive)
            throw new DalIsinactiveException($"Task with ID={m_task.Id} is  inactive");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(m_task);
    }
    public void Reset()
    {
        DataSource.Tasks.Clear();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        Task? Task = (from _task in DataSource.Tasks
                      where (filter(_task))
                                  select _task).First();
        return Task;
    }
}
