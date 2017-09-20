using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Common
{
	public interface ILayer<T> : ICommandLayer<T>, IQueryLayer<T>
	{
	}
}
