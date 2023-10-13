using System.Diagnostics.CodeAnalysis;
using TaskManagement.Models;

namespace TaskManagement.Data.Comparers
{
	/// <summary>
	/// Класс, определяющий методы для определния идентичности экземпляров типа User
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class UserEqualityComparer<T> : IEqualityComparer<T> where T : User?
  {
    public bool Equals(T? x, T? y)
    {
      if ((x is User) && (y is User))
      {
        if (ReferenceEquals(x, y))
          return true;

        if ((x == null)
          || (y == null))
          return false;

        return x.Id == y.Id;
      }
      throw new Exception("Wrong input types");
    }

    public int GetHashCode([DisallowNull] T obj)
    {
      return obj.GetHashCode();
    }
  }
}
