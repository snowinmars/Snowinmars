using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll
{
	public class AuthorLogic : IAuthorLogic, ICRUD<Author>
	{
		private readonly Dao.Interfaces.IAuthorDao authorLogicDestination;

		public AuthorLogic(Dao.Interfaces.IAuthorDao authorLogicDestination)
		{
			this.authorLogicDestination = authorLogicDestination;
		}

		public void Create(Author item)
		{
			Validation.Check(item);

			this.authorLogicDestination.Create(item);
		}

		public Author Get(Guid id)
		{
			return this.authorLogicDestination.Get(id);
		}

		public void Remove(Guid id)
		{
			this.authorLogicDestination.Remove(id);
		}

		public void Update(Author item)
		{
			Validation.Check(item);

			this.authorLogicDestination.Update(item);
		}

		public IEnumerable<Author> Get(Expression<Func<Book, bool>> filter)
		{
			return this.authorLogicDestination.Get(filter);
		}
	}
}