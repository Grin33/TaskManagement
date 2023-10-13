using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.AppData;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Data.Repositories
{
  /// <summary>
  /// Репозиторий для работы с комментариями
  /// </summary>
  public class CommentsRepository : ICommentRepository
  {
    private readonly ApplicationDatabaseContext Context;

    public CommentsRepository(ApplicationDatabaseContext context)
    {
      this.Context = context;
    }

    public bool Add(Comment entity)
    {
      Context.Add(entity);
      return Save();
    }

    public bool Delete(Comment entity)
    {
      Context.Remove(entity);
      return Save();
    }

    public async Task<IEnumerable<Comment?>> GetAllByTaskID(Guid id)
    {
      return await Context.Comments.Where(k => k.Task.Id == id).ToListAsync();
    }

    public async Task<IEnumerable<Comment?>> GetAllByUserID(Guid id)
    {
      return await Context.Comments.Where(k => k.UserId == id).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
      return await Context.Comments.Where(k => k.Id == id).FirstOrDefaultAsync();
    }

    public bool Save()
    {
      var saved = Context.SaveChanges();
      return saved > 0;
    }

    public bool Update(Comment entity)
    {
      Context.Comments.Update(entity);
      return Save();
    }

    public async Task<IEnumerable<Comment?>> GetAllAsync()
    {
      return await Context.Comments.ToListAsync();
    }
  }
}
