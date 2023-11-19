

namespace Dal;

using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer m_engineer)
    {
        Engineer? engineer = Read(m_engineer.Id);
        if (engineer != null)
            throw new DalAlreadyExistsException($"Engineer with ID={m_engineer.Id} already exists");
        const string XMLENGINEER = @"..\..\..\..\..\..\xml\engineer.xml";
        List<DO.Engineer?>? lEngineer = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(XMLENGINEER);
        lEngineer.Add(m_engineer);
        XMLTools.SaveListToXMLSerializer<DO.Engineer>(lEngineer, XMLENGINEER);
        return m_engineer.Id;
    }

    public void Delete(int id)
    {
        const string XMLENGINEER = @"..\..\..\..\..\..\xml\engineer.xml";
        List<DO.Engineer?>? lEngineer = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(XMLENGINEER);
        Engineer? engineer = lEngineer.Where(_engineer => _engineer.Id == id).FirstOrDefault() ??
         throw new DalIsNotExistException($"Engineer with ID={id} is not exists");
        lEngineer.Remove(engineer);
        XMLTools.SaveListToXMLSerializer<DO.Engineer>(lEngineer, XMLENGINEER);
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
