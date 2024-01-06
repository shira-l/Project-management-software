
﻿namespace BO;


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