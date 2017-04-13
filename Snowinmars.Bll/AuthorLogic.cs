using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using System;

namespace Snowinmars.Bll
{
	public class AuthorLogic : IAuthorLogic, ICRUD<Author>
	{
		private readonly Dao.Interfaces.IAuthorDao authorLogicDestination;

		public AuthorLogic(Dao.Interfaces.IAuthorDao authorLogicDestination)
		{
			this.authorLogicDestination = authorLogicDestination;
		}

		public void Create(Author author)
		{
			Validation.Check(author);

			this.authorLogicDestination.Create(author);
		}

		public Author Get(Guid id)
		{
			return this.authorLogicDestination.Get(id);
		}

		public void Remove(Guid id)
		{
			this.authorLogicDestination.Remove(id);
		}

		public void Update(Author author)
		{
			Validation.Check(author);

			this.authorLogicDestination.Update(author);
		}
	}
}