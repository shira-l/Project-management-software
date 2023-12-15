

namespace BlImplementation;
using BlApi;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
        (boEngineer.Id, boEngineer.Cost, boEngineer.Name, boEngineer.Email);
        try
        {
            if (boEngineer.Id > 0 || boEngineer.Name != "" || boEngineer.Cost > 0)
            {
                int idEngineer = _dal.Engineer.Create(doEngineer);
                return idEngineer;
            }
            else
            {
                throw new Exception("Incorrect data");
            }
        }
        //catch (Exception ex)
        //{
        //    throw new BO.Exception("Incorrect data");
        //}
        //catch (DO.DalAlreadyExistsException ex)
        //{
        //    throw new BO.BlAlreadyExistsException($"Student with ID={boEngineer.Id} already exists", ex);
        //}
    }

    public void Delete(int id)
    {
        

    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        
        return new BO.Engineer() { 
          Id = id,
          Cost = doEngineer.Cost,
          Name = doEngineer.Name,
          Email = doEngineer.Email,
          //TaskInEngineer = Task.ReferenceEquals()
            
        };
    }

    public BO.Engineer? Read(Func<BO.Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Cost = doEngineer.Cost,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                });
    }

    public void Update(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
        (boEngineer.Id, boEngineer.Cost, boEngineer.Name, boEngineer.Email);
        try
        {
            if (boEngineer.Id > 0 || boEngineer.Name != "" || boEngineer.Cost > 0)
            {
                _dal.Engineer.Update(doEngineer);
            }
            else
            {
                throw new Exception("Incorrect data");
            }
        }
        //catch (Exception ex)
        //{
        //    //throw new BO.Exception("Incorrect data");
        //}
        //catch (BO.DalIsNotExistException ex)
        //{
        //    //throw new BO.DalIsNotExistException($"Engineer with ID={boEngineer.Id} is not exists", ex);
        //}
    }
}
