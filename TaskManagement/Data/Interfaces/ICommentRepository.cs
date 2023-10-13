using TaskManagement.Models;

namespace TaskManagement.Data.Interfaces
{
  /// <summary>
  /// Интерфейс для работы с экземплярами типа Comment
  /// </summary>
  public interface ICommentRepository : IRepository<Comment>
  {
    Task<IEnumerable<Comment?>> GetAllByUserID(Guid id);
    Task<IEnumerable<Comment?>> GetAllByTaskID(Guid id);
  }
}
