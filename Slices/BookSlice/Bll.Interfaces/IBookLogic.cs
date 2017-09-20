using System;
using System.Collections.Generic;
using Snowinmars.AuthorSlice.AuthorEntities;
using Snowinmars.BookSlice.BookEntities;
using Snowinmars.Common;

namespace Snowinmars.BookSlice.BookBll.Interfaces
{
	public interface IBookLogic : ILayer<Book>
	{
		IEnumerable<Author> GetAuthors(Guid bookId);

		void StartInformAboutWarnings(Guid bookId);

		void StartInformAboutWarnings();

		void StopInformAboutWarnings(Guid bookId);

		void StopInformAboutWarnings();

		IEnumerable<Book> GetWishlist(string username);
	}
}
