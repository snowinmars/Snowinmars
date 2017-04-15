using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using Snowinmars.Entities;

namespace Snowinmars.Bll.Interfaces
{
	public interface IBookLogic : ICRUD<Book>
	{
		IEnumerable<Book> Get(Expression<Func<Book, bool>> filter);
		IEnumerable<Guid> GetAuthorIds(Guid bookId);
	}
}