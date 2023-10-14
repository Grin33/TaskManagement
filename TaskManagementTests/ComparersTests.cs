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

    [SetUp]
    public void Setup()
    {
      this.taskPriorityComparer = new TaskPriorityComparer<Task?>();
      this.taskEqualityComparer = new TaskEqualityComparer<Task?>();
      this.userEqualityComparer = new UserEqualityComparer<User?>();
    }

    [Test]
    public void Test1()
    {
      Assert.Pass();
    }
  }
}