using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IEngineerInList
{
    public IEnumerable<BO.EngineerInList> ReadAll(IEnumerable<BO.Engineer?> engineers);//Creates new engineer entity in BL

}

