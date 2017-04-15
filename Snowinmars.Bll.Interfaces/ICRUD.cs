using System;

namespace Snowinmars.Bll.Interfaces
{
	public interface ICRUD<T>
	{
		void Create(T item);

		T Get(Guid id);

		void Remove(Guid id);

		void Update(T item);
	}
}