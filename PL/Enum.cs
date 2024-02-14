using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace PL;

internal class EngineersCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

internal class TasksCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enums =
    (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

internal class Collection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!.Where(Experience=> Experience!= BO.EngineerExperience.None)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}


