using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data.AppData
{
  public class ApplicationDatabaseContext : DbContext
  {
    /// <summary>
    /// User в БД.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Task в БД.
    /// </summary>
    public DbSet<Models.Task> Tasks { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public DbSet<Comment> Comments { get; set; }

    /// <summary>
    /// Notifications
    /// </summary>
    public DbSet<Notification> Notifications { get; set; }

    /// <summary>
    /// Comment в БД.
    /// </summary>

    /// <summary>
    /// Базовый конструктор для настройки БД.
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) { }

    /// <summary>
    /// Построение многие ко многим для User и Task через таблицы Reviewers и Executors.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // User -> Task
      modelBuilder.Entity<User>()
        .HasMany(e => e.Reviewers)
        .WithMany(e => e.Reviewers)
        .UsingEntity<Reviewers>();

      modelBuilder.Entity<User>()
        .HasMany(e => e.Executors)
        .WithMany(e => e.Executors)
        .UsingEntity<Executors>();

      // User -> Notification
      modelBuilder.Entity<User>()
        .HasMany(e => e.Notifications)
        .WithOne(e => e.User);

      modelBuilder.Entity<Comment>()
        .HasOne(e => e.Task)
        .WithMany(e => e.Comments);

      //Task -> User
      modelBuilder.Entity<Models.Task>()
        .HasMany(e => e.Reviewers)
        .WithMany(e => e.Reviewers)
        .UsingEntity<Reviewers>();

      modelBuilder.Entity<Models.Task>()
        .HasMany(e => e.Executors)
        .WithMany(e => e.Executors)
        .UsingEntity<Executors>();

      // Task -> Comment
      modelBuilder.Entity<Models.Task>()
        .HasMany(e => e.Comments)
        .WithOne(e => e.Task);

      modelBuilder.Entity<Notification>()
        .HasOne(e => e.User)
        .WithMany(e => e.Notifications);
    }
  }
}
