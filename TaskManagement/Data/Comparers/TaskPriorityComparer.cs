using System.Collections.Generic;

namespace TaskManagement.Data.Comparers
{
  /// <summary>
  /// Класс для сравнения экзмепляров Task по приоритету
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class TaskPriorityComparer<T> : IComparer<T> where T : Models.Task?
  {
    public int Compare(T? x, T? y)
    {
      if (x.Priority > y.Priority)
        return -1;
      else if (x.Priority < y.Priority)
        return 1;
      return 0;
    }
  }
}
