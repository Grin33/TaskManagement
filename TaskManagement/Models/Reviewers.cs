namespace TaskManagement.Models
{
  /// <summary>
  /// Промежуточная таблица наблюдателей
  /// </summary>
  public class Reviewers
  {
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
  }
}
