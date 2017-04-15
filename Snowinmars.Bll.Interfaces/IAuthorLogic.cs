using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll.Interfaces
{
	public interface IAuthorLogic : ICRUD<Author>
	{
		IEnumerable<Author> Get(Expression<Func<Book, bool>> filter);
	}
}