using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Snowinmars.Entities;

namespace Snowinmars.Dao.Interfaces
{
	public interface IBookDao : ICRUD<Book>
	{
		IEnumerable<Book> Get(Expression<Func<Book, bool>> filter);
		IEnumerable<Guid> GetAuthorIds(Guid bookId);
	}
}