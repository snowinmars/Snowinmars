using System;
using System.Collections.Generic;
using Snowinmars.AuthorSlice.AuthorEntities;
using Snowinmars.BookSlice.BookEntities;
using Snowinmars.Common;

namespace Snowinmars.BookSlice.BookDao.Interfaces
{
	public interface IBookDao : ILayer<Book>
	{
		IEnumerable<KeyValuePair<Guid, Guid>> GetAllBookAuthorConnections();

		IEnumerable<Author> GetAuthorsForBook(Guid bookId);

		IEnumerable<Guid> SelectBooksUnindexedByShortcutsCommand();

		IEnumerable<Book> GetWishlist(string username, BookStatus status);
	}
}
