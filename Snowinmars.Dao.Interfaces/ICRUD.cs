using System;

namespace Snowinmars.Dao.Interfaces
{
    public interface ICRUD<T>
    {
        void Create(T film);

        T Get(Guid id);

        void Remove(Guid id);

        void Update(T item);
    }
}