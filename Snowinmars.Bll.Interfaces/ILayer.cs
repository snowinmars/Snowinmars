using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll.Interfaces
{
    public interface ILayer<T> : ICommandLayer<T>, IQueryLayer<T>
	{
    }
}