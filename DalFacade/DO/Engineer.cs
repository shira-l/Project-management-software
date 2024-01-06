
namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Personal unique ID of engineer</param>
/// <param name="Name">Private name of the engineer</param>
/// <param name="Email">The engineer's email</param>
/// <param name="Level">The level of the engineer</param>
/// <param name="Cost">Hourly cost</param>
public record Engineer
(
    int Id,
    double Cost,
    string? Name = null,
    string? Email = null,
    EngineerExperience? Level=null
)
{
    public Engineer() : this(0,0,null,null,null) { } //empty ctor for stage 3

}
