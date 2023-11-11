

namespace DalApi;

using DO;
using System;
using System.Collections.Generic;

public interface IEngineer : ICrud<Engineer>
{
    IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null);
}
