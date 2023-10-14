using Microsoft.EntityFrameworkCore.Infrastructure;
using TaskManagement.Data.Comparers;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagementTests
{
  public class Tests
  {
    public TaskEqualityComparer<Task?> taskEqualityComparer { get; set; }

    public TaskPriorityComparer<Task?> taskPriorityComparer { get; set; }

    public UserEqualityComparer<User?> userEqualityComparer { get; set; }

    public List<Task> testTasks { get; set; }
    
    public List<User> testUsers { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      this.taskPriorityComparer = new TaskPriorityComparer<Task?>();
      this.taskEqualityComparer = new TaskEqualityComparer<Task?>();
      this.userEqualityComparer = new UserEqualityComparer<User?>();
    }

    [SetUp]
    public void Setup() 
    {

      var id = Guid.NewGuid();
      var date = DateTime.Now;
      this.testTasks = new List<Task>();
      var task1 = new Task()
      {
        Name = "Test",
        Description = "Test",
        Id = id,
        DeadLine = date,
        Comments = new List<Comment?>(),
        Executors = new List<User?>(),
        Reviewers = new List<User>(),
        Status = TaskManagement.Data.Enums.Status.Review,
        Priority = TaskManagement.Data.Enums.Priority.Medium
      };
      this.testTasks.Add(task1);

      this.testUsers = new List<User>();
      var user1 = new User()
      {
        Email = "public@gmail.ru",
        Username = "Test",
        Executors = new List<Task?>(),
        Reviewers = new List<Task?>(),
        Information = "info",
        Notifications = new List<Notification>(),
        Password = "123123",
        Role = TaskManagement.Data.Enums.Role.Default,
        Id = Guid.NewGuid(),
        CreatedAt = DateTime.Now
      };
      this.testUsers.Add(user1);

    }

    [Test]
    public void DistinctTaskListT_NewTasks_SuccessfulyDistincted()
    {
      this.testTasks.Add(testTasks[0]);
      var distinctedTasks = this.testTasks.Distinct(this.taskEqualityComparer).ToList();
      Assert.True(distinctedTasks.Count() == 1);
    }

    [Test]
    public void PrioritySortTaskListT_NewTasks_SuccessfulySorted()
    {
      var newTask = new Task()
      {
        Name = "Test",
        Description = "Test",
        Id = Guid.NewGuid(),
        DeadLine = DateTime.Now,
        Comments = new List<Comment?>(),
        Executors = new List<User?>(),
        Reviewers = new List<User>(),
        Status = TaskManagement.Data.Enums.Status.Review,
        Priority = TaskManagement.Data.Enums.Priority.Medium
      };
      this.testTasks.Add(newTask);
      this.testTasks[1].Priority = TaskManagement.Data.Enums.Priority.High;

      this.testTasks.Sort(this.taskPriorityComparer);

      Assert.True
        (
          this.testTasks[0].Priority == TaskManagement.Data.Enums.Priority.High
          && this.testTasks[1].Priority == TaskManagement.Data.Enums.Priority.Medium
        );
    }

    [Test]
    public void DistinctUserListT_NewUser_SuccesfulyDistincted()
    {
      this.testUsers.Add(testUsers[0]);
      var distinctedUsers = this.testUsers.Distinct(this.userEqualityComparer).ToList();
      Assert.True(distinctedUsers.Count() == 1);
    }
  }
}