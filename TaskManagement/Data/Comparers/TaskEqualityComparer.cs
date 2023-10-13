using System.Diagnostics.CodeAnalysis;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;
namespace TaskManagement.Data.Comparers
{
	/// <summary>
	/// Класс, определяющий методы для определния идентичности экземпляров типа Task
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TaskEqualityComparer<T> : IEqualityComparer<T> where T : Task?
	{
		public bool Equals(T? x, T? y)
		{
			if((x is Task) && (y is Task))
			{
				if(ReferenceEquals(x, y)) 
					return true;
				
				if((x == null)
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
