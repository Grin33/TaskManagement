namespace TaskManagement.Models
{
  /// <summary>
  /// Промежуточная таблица исполнителей
  /// </summary>
  public class Executors
  {
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
  }
}
