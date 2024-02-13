using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ITaskInList
{
    public IEnumerable<BO.TaskInList> ReadAll(IEnumerable<BO.Task?> tasks);//Creates new task entity in BL
}
