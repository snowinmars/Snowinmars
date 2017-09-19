using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Dao.Interfaces
{
    public interface IBookDao : ILayer<Book>
    {
        IEnumerable<KeyValuePair<Guid, Guid>> GetAllBookAuthorConnections();

        IEnumerable<Author> GetAuthorsForBook(Guid bookId);

        IEnumerable<Guid> SelectBooksUnindexedByShortcutsCommand();

	    IEnumerable<Book> GetWishlist(string username, BookStatus status);
    }
}