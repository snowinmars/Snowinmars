using System;

namespace Snowinmars.Dao.Interfaces
{
    public interface ILayer<T> : ICommandLayer<T>, IQueryLayer<T>
	{
    }
}