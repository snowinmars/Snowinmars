using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll.Interfaces
{
	public interface IBookLogic : ICRUD<Book>
	{
		IEnumerable<Book> Get(Expression<Func<Book, bool>> filter);

		IEnumerable<Author> GetAuthors(Guid bookId);

		void StartInformAboutWarnings(Guid bookId);
		void StopInformAboutWarnings(Guid bookId);
		void StartInformAboutWarnings();
		void StopInformAboutWarnings();
	}
}