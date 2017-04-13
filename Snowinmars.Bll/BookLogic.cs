using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using System;

namespace Snowinmars.Bll
{
	public class BookLogic : IBookLogic, ICRUD<Book>
	{
		private readonly Dao.Interfaces.IBookDao bookLogicDestination;

		public BookLogic(Dao.Interfaces.IBookDao bookLogicDestination)
		{
			this.bookLogicDestination = bookLogicDestination;
		}

		public void Create(Book book)
		{
			Validation.Check(book);

			this.bookLogicDestination.Create(book);
		}

		public Book Get(Guid id)
		{
			return this.bookLogicDestination.Get(id);
		}

		public void Remove(Guid id)
		{
			this.bookLogicDestination.Remove(id);
		}

		public void Update(Book book)
		{
			Validation.Check(book);

			this.bookLogicDestination.Update(book);
		}
	}
}