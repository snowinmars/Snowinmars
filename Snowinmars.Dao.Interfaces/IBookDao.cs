using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Dao.Interfaces
{
    public interface IBookDao : ICRUD<Book>
    {
        IEnumerable<Book> Get(Expression<Func<Book, bool>> filter);

        IEnumerable<KeyValuePair<Guid, Guid>> GetAllBookAuthorConnections();

        IEnumerable<Author> GetAuthorsForBook(Guid bookId);

        IEnumerable<Guid> SelectBooksUnindexedByShortcutsCommand();
    }
}