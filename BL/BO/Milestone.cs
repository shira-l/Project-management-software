
namespace BO;

public class Milestone
{
    public int Id { get; init; }
    public string? Deliverables { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreateAtDate { get; set; }
    public Status Status { get; init; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? ComplateDate { get; set; }
    public string? Remarks { get; set; }
    public List<TaskInList>? dependencies { get; set; }
    public double CompletionPercentage { get; set; }

}
