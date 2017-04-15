using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Snowinmars.Dao.Interfaces;

namespace Snowinmars.Bll
{
	// ReSharper disable once RedundantExtendsListEntry
	// ReSharper disable once RedundantNameQualifier
	public class BookLogic : IBookLogic, Bll.Interfaces.ICRUD<Book>
	{
		private readonly IBookDao bookDao;

		public BookLogic(IBookDao bookDao)
		{
			this.bookDao = bookDao;
		}

		public void Create(Book item)
		{
			Validation.Check(item);

			this.bookDao.Create(item);
		}

		public Book Get(Guid id)
		{
			Validation.Check(id);

			return this.bookDao.Get(id);
		}

		public IEnumerable<Book> Get(Expression<Func<Book, bool>> filter)
		{
			return this.bookDao.Get(filter);
		}

		public IEnumerable<Author> GetAuthors(Guid bookId)
		{
			Validation.Check(bookId);

			return this.bookDao.GetAuthors(bookId);
		}

		public void Remove(Guid id)
		{
			Validation.Check(id);

			this.bookDao.Remove(id);
		}

		public void Update(Book item)
		{
			Validation.Check(item);

			this.bookDao.Update(item);
		}
	}
}