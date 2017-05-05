using Snowinmars.Bll.Interfaces;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll
{
	// ReSharper disable once RedundantExtendsListEntry
	// ReSharper disable once RedundantNameQualifier
	public class AuthorLogic : IAuthorLogic, Bll.Interfaces.ICRUD<Author>
	{
		private readonly IAuthorDao authorDao;

		public AuthorLogic(IAuthorDao authorDao)
		{
			this.authorDao = authorDao;
		}

		public void Create(Author item)
		{
			Validation.Check(item);

			this.authorDao.Create(item);
		}

		public Author Get(Guid id)
		{
			Validation.Check(id);

			return this.authorDao.Get(id);
		}

		public IEnumerable<Author> Get(Expression<Func<Book, bool>> filter)
		{
			return this.authorDao.Get(filter);
		}

		public void Remove(Guid id)
		{
			Validation.Check(id);

			this.authorDao.Remove(id);
		}

		public void Update(Author item)
		{
			Validation.Check(item);

			this.authorDao.Update(item);
		}
	}
}
