
﻿namespace BO;

/// <summary>
/// 
/// </summary>
/// <param name="Id">Personal unique ID of engineer</param>
/// <param name="Name">Private name of the engineer</param>
/// <param name="Email">The engineer's email</param>
/// <param name="Level">The level of the engineer</param>
/// <param name="Cost">Hourly cost</param>

public class Engineer
{
    public int Id { get; init; }
    public double Cost { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerExperience? Level { get; set; }
    public TaskInEngineer? Task { get; init; }
    public override string ToString()
    {
        return $"{Name} {Email} {Cost} {Level} \n current task:{Task}";
    }
}