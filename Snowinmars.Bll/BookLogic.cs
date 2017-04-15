using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll
{
	public class BookLogic : IBookLogic, ICRUD<Book>
	{
		private readonly Dao.Interfaces.IBookDao bookLogicDestination;

		public BookLogic(Dao.Interfaces.IBookDao bookLogicDestination)
		{
			this.bookLogicDestination = bookLogicDestination;
		}

		public void Create(Book item)
		{
			Validation.Check(item);

			this.bookLogicDestination.Create(item);
		}

		public Book Get(Guid id)
		{
			return this.bookLogicDestination.Get(id);
		}

		public void Remove(Guid id)
		{
			this.bookLogicDestination.Remove(id);
		}

		public void Update(Book item)
		{
			Validation.Check(item);

			this.bookLogicDestination.Update(item);
		}

		public IEnumerable<Book> Get(Expression<Func<Book, bool>> filter)
		{
			return this.bookLogicDestination.Get(filter);
		}

		public IEnumerable<Guid> GetAuthorIds(Guid bookId)
		{
			return this.bookLogicDestination.GetAuthorIds(bookId);
		}
	}
}