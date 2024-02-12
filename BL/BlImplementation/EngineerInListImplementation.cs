namespace BlImplementation;
using BlApi;
using BO;
using System.Text.RegularExpressions;
/// <summary>
/// The interface implementation of Engineer
/// </summary>
internal class EngineerInListImplementation : IEngineerInList
{
    public IEnumerable<EngineerInList> ReadAll(IEnumerable<Engineer?> engineer)
    {
       return engineer.Select(engineer => new EngineerInList() { Id = engineer!.Id, Name = engineer.Name, Email = engineer.Email, Level = engineer.Level });
    }
}
