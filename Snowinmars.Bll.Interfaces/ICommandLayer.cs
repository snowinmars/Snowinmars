using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Bll.Interfaces
{
	public interface ICommandLayer<T>
	{
		void Create(T item);

		void Remove(Guid id);

		void Update(T item);
	}
}
