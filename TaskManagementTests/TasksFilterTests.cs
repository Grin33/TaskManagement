using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Structs;
using Task = TaskManagement.Models.Task;

namespace TaskManagementTests
{
  public class TasksFilterTests
  {
    public IEnumerable<Task?> Tasks { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {

      Tasks = new List<Task?>
      {
        new Task()
        {
          Id = Guid.NewGuid(),
          DeadLine = DateTime.Now,
          Description = "Test",
          Name = "Test",
          Status = TaskManagement.Data.Enums.Status.Review,
          Priority = TaskManagement.Data.Enums.Priority.Medium
        },
        new Task()
        {
          Id = Guid.NewGuid(),
          DeadLine = DateTime.Now,
          Description = "34.11.94",
          Name = "GOST Hash",
          Status = TaskManagement.Data.Enums.Status.Finished,
          Priority = TaskManagement.Data.Enums.Priority.Low
        },
        new Task()
        {
          Id = Guid.NewGuid(),
          DeadLine = DateTime.Now,
          Description = "Стабильно, уверенность в завтрашнем дне, соц пакет полный...",
          Name = "Устроится всем дружно на завод",
          Status = TaskManagement.Data.Enums.Status.ExecutorRequired,
          Priority = TaskManagement.Data.Enums.Priority.High
        },
        new Task()
        {
          Id = Guid.NewGuid(),
          DeadLine = DateTime.Now,
          Description = "Не знаю чево уже писать сюда",
          Name = "Еще какая-то задача",
          Status = TaskManagement.Data.Enums.Status.InProgress,
          Priority = TaskManagement.Data.Enums.Priority.High
        },
      };
    }

    [Test]
    public void TaskFilterT_FilterByName_SuccesfulyFiltered()
    {
      var filters = new TaskFilterStruct();
      filters.FilterText = "Test";
      var FilteredTasks = filters.Filter(Tasks);

      Assert.True(FilteredTasks.Count() == 1);
    }

    [Test]
    public void TaskFilterT_FilterByPriority_SuccesfulyFiltered()
    {
      var filters = new TaskFilterStruct();
      filters.HighPriorityEnabled = false;
      var FilteredTasks = filters.Filter(Tasks);

      Assert.True(FilteredTasks.Count() == 2);
    }

    [Test]
    public void TaskFilterT_FilterByStatus_SuccesfulyFiltered()
    {
      var filters = new TaskFilterStruct();
      filters.isReviewEnabled = false;
      var FilteredTasks = filters.Filter(Tasks);

      Assert.True(FilteredTasks.Count() == 3);
    }
  }
}
