namespace TaskManagement.Data.Interfaces
{
  /// <summary>
  /// Некая сущность из БД
  /// </summary>
  public interface IEntity
  {
    /// <summary>
    /// Id сущности.
    /// </summary>
    Guid Id { get; set; }
  }
}
