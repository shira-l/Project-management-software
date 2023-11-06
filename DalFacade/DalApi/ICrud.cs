using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        int Create(T item); //Creates new Engineer object in DAL
        T? Read(int id); //Reads Engineer object by its ID 
        List<T> ReadAll(); // Reads all Engineer objects
        void Update(T item); //Updates Engineer object
        void Delete(int id); //Deletes an object by its Id
        void Reset();//Resets the data list
    }
}
