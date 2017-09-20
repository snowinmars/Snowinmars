﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Common
{
	public interface IQueryLayer<T>
	{
		T Get(Guid id);
		IEnumerable<T> Get(Func<T, bool> filter);
	}
}
