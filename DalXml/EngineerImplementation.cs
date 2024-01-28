

namespace Dal;

using DalApi;
using DO;
using System;
using System.Collections.Generic;
/// <summary>
/// The interface implementation of Engineer
/// </summary>
internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer m_engineer)
    {
        Engineer? engineer = Read(m_engineer.Id);
        if (engineer != null)
            throw new DalAlreadyExistsException($"Engineer with ID={m_engineer.Id} already exists");
        const string XMLENGINEER = @"Engineer";
        List<Engineer>? lEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        lEngineer.Add(m_engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(lEngineer, XMLENGINEER);
        return m_engineer.Id;
    }

    public void Delete(int id)
    {
        const string XMLENGINEER = @"Engineer";
        List<Engineer>? lEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        Engineer? engineer = lEngineer.Where(_engineer => _engineer!.Id == id).FirstOrDefault() ??
         throw new DalIsNotExistException($"Engineer with ID={id} is not exists");
        lEngineer.Remove(engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(lEngineer, XMLENGINEER);
    }

    public Engineer? Read(int id)
    {
        const string XMLENGINEER = @"Engineer";
        List<Engineer>? lEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        Engineer? engineer = lEngineer.Where(_engineer => _engineer!.Id == id).FirstOrDefault();
        return engineer;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        const string XMLENGINEER = @"Engineer";
        List<Engineer>? lEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        Engineer? Engineer = lEngineer.Where(filter!).FirstOrDefault();
        return Engineer;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        const string XMLENGINEER = @"Engineer";
        List<Engineer>? lEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        if (filter == null)
            return lEngineer.Select(item => item);
        else
            return lEngineer.Where(filter!);
    }

    public void Reset()
    {
        const string XMLENGINEER = @"Engineer";
        List<Engineer>? lEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        lEngineer.Clear();
        XMLTools.SaveListToXMLSerializer<Engineer>(lEngineer!, XMLENGINEER);
    }

    public void Update(Engineer m_engineer)
    {
        const string XMLENGINEER = @"Engineer";
        List<Engineer>? lEngineer = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        Engineer? engineer = Read(m_engineer.Id) ??
        throw new DalIsNotExistException($"Engineer with ID={m_engineer.Id} is not exists");
        lEngineer.Remove(engineer);
        lEngineer.Add(m_engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(lEngineer!, XMLENGINEER);
    }
}
