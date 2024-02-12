
namespace BO
{
    /// <summary>
    /// Engineer Entity
    /// </summary>
    /// <param name="Id">Personal unique ID of engineer</param>
    /// <param name="Name">Private name of the engineer</param>
    /// <param name="Email">The engineer's email</param>
    /// <param name="Level">The level of the engineer</param>
    /// <param name="Cost">Hourly cost</param>
    /// <param name="Task">The current task of the engineer</param>

    public class EngineerInList
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public EngineerExperience? Level { get; set; }
        public override string ToString()
        {
            return $"Id:{Id} \n Name:{Name}\n Email:{Email}\n Level:{Level}";
        }
    }

}
