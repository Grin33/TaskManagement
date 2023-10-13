namespace TaskManagement.Data.Interfaces
{
  /// <summary>
  /// Интерфейс некоего репозитория для работы с сущностями из БД
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IRepository<T> where T : IEntity
  {
    /// <summary>
    /// Получить всех сущностей из БД асинхронно.
    /// </summary>
    /// <returns>Список сущностей.</returns>
    Task<IEnumerable<T?>> GetAllAsync();

    /// <summary>
    /// Получить сущность из БД асинхронно.
    /// </summary>
    /// <param name="id">Id сущности.</param>
    /// <returns>Сущность.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить сущность в контекст БД.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат добавления.</returns>
    bool Add(T entity);

    /// <summary>
    /// Обновить сущность в контексте БД.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат обновления.</returns>
    bool Update(T entity);

    /// <summary>
    /// Удалить сущность из контекста БД.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат удаления.</returns>
    bool Delete(T entity);

    /// <summary>
    /// Сохранить все изменения в БД.
    /// </summary>
    /// <returns>Результат сохранения.</returns>
    bool Save();
  }
}
