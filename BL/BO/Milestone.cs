
namespace BO;

public class Milestone
{
    public int Id { get; init; }
    public string? Deliverables { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreateAtDate { get; init; }
    public Status Status { get; init; }
    public DateTime? ForecastDate { get; init; }
    public DateTime? DeadlineDate { get; init; }
    public DateTime? ComplateDate { get; init; }
    public string? Remarks { get; set; }
    public List<TaskInList>? dependencies { get; init; }
    public double CompletionPercentage { get; init; }

}
