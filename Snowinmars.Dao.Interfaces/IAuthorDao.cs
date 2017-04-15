using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Snowinmars.Entities;

namespace Snowinmars.Dao.Interfaces
{
	public interface IAuthorDao : ICRUD<Author>
	{
		IEnumerable<Author> Get(Expression<Func<Book, bool>> filter);
	}
}