using System;
using System.Collections.Generic;
using Snowinmars.AuthorSlice.AuthorBll.Interfaces;
using Snowinmars.AuthorSlice.AuthorDao.Interfaces;
using Snowinmars.AuthorSlice.AuthorEntities;

namespace Snowinmars.AuthorSlice.AuthorBll
{
	public class AuthorLogic : IAuthorLogic
	{
		private readonly IAuthorDao authorDao;

		public AuthorLogic(IAuthorDao authorDao)
		{
			this.authorDao = authorDao;
		}

		public void Create(AuthorEntities.Author item)
		{
			Validation.BllCheck(item);

			this.authorDao.Create(item);
		}

		public AuthorEntities.Author Get(Guid id)
		{
			Validation.Check(id);

			return this.authorDao.Get(id);
		}

		public IEnumerable<AuthorEntities.Author> Get(Func<AuthorEntities.Author, bool> filter)
		{
			return this.authorDao.Get(filter);
		}

		public void Remove(Guid id)
		{
			Validation.Check(id);

			this.authorDao.Remove(id);
		}

		public void Update(AuthorEntities.Author item)
		{
			Validation.BllCheck(item);

			this.authorDao.Update(item);
		}
	}
}
