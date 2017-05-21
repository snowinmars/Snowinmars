using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Dao.Interfaces
{
    public interface IAuthorDao : ICRUD<Author>
    {
        IEnumerable<Author> Get(Expression<Func<Book, bool>> filter);
    }
}