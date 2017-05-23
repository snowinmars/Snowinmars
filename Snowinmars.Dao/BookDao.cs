using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Snowinmars.Dao
{
    public class BookDao : IBookDao, ICRUD<Book>
    {
        private readonly IAuthorDao authorDao;

        public BookDao(IAuthorDao authorDao)
        {
            this.authorDao = authorDao;

            Validation.Set(this);
        }

        public void Create(Book item)
        {
            Validation.Check(item);

            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                this.AddBook(item, sqlConnection);
                this.AddBookAuthorConnections(item.Id, item.AuthorIds, sqlConnection);
            }
        }

        public Book Get(Guid id)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                var command = new SqlCommand(LocalConst.Book.SelectCommand, sqlConnection);

                command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, LocalCommon.ConvertToDbValue(id));

                sqlConnection.Open();
                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    throw new ObjectNotFoundException();
                }

                Book book = LocalCommon.MapBook(reader);

                sqlConnection.Close();

                //////////////////////
                //// I suppose that reusing SqlCommand will improve perfomance. I read few about it. I have to know it better. Todo?

                command.CommandText = LocalConst.BookAuthor.SelectByBookCommand;

                command.Parameters.Clear();
                command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, LocalCommon.ConvertToDbValue(book.Id));

                sqlConnection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid authorId = (Guid)reader[LocalConst.BookAuthor.Column.AuthorId];

                    book.AuthorIds.Add(authorId);
                }

                sqlConnection.Close();

                return book;
            }
        }

        public IEnumerable<Book> Get(Expression<Func<Book, bool>> filter)
        {
            var books = this.GetAll().ToList();

            //foreach (var book in books)
            //{
            //    book.AuthorIds.AddRange(this.GetAuthorsForBook(book.Id).Select(a => a.Id));
            //}

            return books;
        }

        public IEnumerable<KeyValuePair<Guid, Guid>> GetAllBookAuthorConnections()
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                var command = new SqlCommand(LocalConst.BookAuthor.SelectAllCommand, sqlConnection);

                IList<KeyValuePair<Guid, Guid>> result = new List<KeyValuePair<Guid, Guid>>();

                sqlConnection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid bookId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.BookAuthor.Column.BookId]);
                    Guid authorId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.BookAuthor.Column.AuthorId]);

                    result.Add(new KeyValuePair<Guid, Guid>(bookId, authorId));
                }

                sqlConnection.Close();

                return result;
            }
        }

        public IEnumerable<Author> GetAuthorsForBook(Guid bookId)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                var command = new SqlCommand(LocalConst.BookAuthor.SelectByBookCommand, sqlConnection);

                command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, LocalCommon.ConvertToDbValue(bookId));

                sqlConnection.Open();
                var reader = command.ExecuteReader();
                var authors = this.MapAuthorFromIds(reader);
                sqlConnection.Close();

                return authors;
            }
        }

        public void Remove(Guid id)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                var command = new SqlCommand(LocalConst.Book.DeleteCommand, sqlConnection);

                command.Parameters.AddWithValue(LocalConst.Book.Column.Id, LocalCommon.ConvertToDbValue(id));

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public IEnumerable<Guid> SelectBooksUnindexedByShortcutsCommand()
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                var command = new SqlCommand(LocalConst.Book.SelectBooksUnindexedByShortcutsCommand, sqlConnection);

                List<Guid> ids = new List<Guid>();

                sqlConnection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid id = (Guid)reader[LocalConst.Book.Column.Id];
                    ids.Add(id);
                }

                sqlConnection.Close();

                return ids;
            }
        }

        public void Update(Book item)
        {
            Validation.Check(item, mustbeUnique: false);

            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                this.HandleAuthorUpdate(item, sqlConnection);

                var authorsShortcuts = LocalCommon.ConvertToDbValue(string.Join(",", item.AuthorShortcuts));

                // updating usual fields
                var command = new SqlCommand(LocalConst.Book.UpdateCommand, sqlConnection);

                var id = LocalCommon.ConvertToDbValue(item.Id);
                var year = LocalCommon.ConvertToDbValue(item.Year);
                var title = LocalCommon.ConvertToDbValue(item.Title);
                var owner = LocalCommon.ConvertToDbValue(item.Owner);
                var pageCount = LocalCommon.ConvertToDbValue(item.PageCount);
                var bookshelf = LocalCommon.ConvertToDbValue(item.Bookshelf);
                var liveLibUrl = LocalCommon.ConvertToDbValue(item.LiveLibUrl);
                var libRusEcUrl = LocalCommon.ConvertToDbValue(item.LibRusEcUrl);
                var flibustaUrl = LocalCommon.ConvertToDbValue(item.FlibustaUrl);
                var additionalInfo = LocalCommon.ConvertToDbValue(item.AdditionalInfo);
                var isSynchronized = LocalCommon.ConvertToDbValue(item.IsSynchronized);
                var mustInformAboutWarnings = LocalCommon.ConvertToDbValue(item.MustInformAboutWarnings);

                command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, id);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.Year, year);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.Title, title);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.Owner, owner);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.PageCount, pageCount);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.Bookshelf, bookshelf);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.LiveLibUrl, liveLibUrl);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.LibRusEcUrl, libRusEcUrl);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.FlibustaUrl, flibustaUrl);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.IsSynchronized, isSynchronized);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.AdditionalInfo, additionalInfo);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.AuthorsShortcuts, authorsShortcuts);
                command.Parameters.AddWithValue(LocalConst.Book.Parameter.MustInformAboutWarnings, mustInformAboutWarnings);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        private void AddBook(Book book, SqlConnection sqlConnection)
        {
            var command = new SqlCommand(LocalConst.Book.InsertCommand, sqlConnection);

            var id = LocalCommon.ConvertToDbValue(book.Id);
            var year = LocalCommon.ConvertToDbValue(book.Year);
            var title = LocalCommon.ConvertToDbValue(book.Title);
            var owner = LocalCommon.ConvertToDbValue(book.Owner);
            var pageCount = LocalCommon.ConvertToDbValue(book.PageCount);
            var bookshelf = LocalCommon.ConvertToDbValue(book.Bookshelf);
            var liveLibUrl = LocalCommon.ConvertToDbValue(book.LiveLibUrl);
            var libRusEcUrl = LocalCommon.ConvertToDbValue(book.LibRusEcUrl);
            var flibustaUrl = LocalCommon.ConvertToDbValue(book.FlibustaUrl);
            var additionalInfo = LocalCommon.ConvertToDbValue(book.AdditionalInfo);
            var isSynchronized = LocalCommon.ConvertToDbValue(book.IsSynchronized);
            var authorsShortcuts = LocalCommon.ConvertToDbValue(string.Join(",", book.AuthorShortcuts));
            var mustInformAboutWarnings = LocalCommon.ConvertToDbValue(book.MustInformAboutWarnings);

            command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, id);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.Year, year);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.Title, title);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.Owner, owner);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.PageCount, pageCount);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.Bookshelf, bookshelf);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.LiveLibUrl, liveLibUrl);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.LibRusEcUrl, libRusEcUrl);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.FlibustaUrl, flibustaUrl);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.AdditionalInfo, additionalInfo);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.IsSynchronized, isSynchronized);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.AuthorsShortcuts, authorsShortcuts);
            command.Parameters.AddWithValue(LocalConst.Book.Parameter.MustInformAboutWarnings, mustInformAboutWarnings);

            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void AddBookAuthorConnections(Guid bookId, IEnumerable<Guid> authorIds, SqlConnection sqlConnection)
        {
            var command = new SqlCommand(LocalConst.BookAuthor.InsertCommand, sqlConnection);

            command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, LocalCommon.ConvertToDbValue(bookId));

            // Here I reusing SqlParameter to improve everything :3 Idk, is it true. Read about it? Todo.
            var authorIdParameter = new SqlParameter(LocalConst.BookAuthor.Parameter.AuthorId, SqlDbType.UniqueIdentifier);
            command.Parameters.Add(authorIdParameter);

            sqlConnection.Open();

            foreach (var authorId in authorIds)
            {
                command.Parameters[1].Value = authorId;

                command.ExecuteNonQuery();
            }

            sqlConnection.Close();
        }

        private void DeleteBookAuthorConnections(Guid bookId, IEnumerable<Guid> authorIds, SqlConnection sqlConnection)
        {
            var command = new SqlCommand(LocalConst.BookAuthor.DeleteBookAuthorCommand, sqlConnection);

            command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, LocalCommon.ConvertToDbValue(bookId));

            // Here I reusing SqlParameter to improve everything :3 Idk, is it true. Read about it? Todo.
            var authorIdParameter = new SqlParameter(LocalConst.BookAuthor.Parameter.AuthorId, SqlDbType.UniqueIdentifier);
            command.Parameters.Add(authorIdParameter);

            sqlConnection.Open();

            foreach (var authorId in authorIds)
            {
                command.Parameters[1].Value = authorId;

                command.ExecuteNonQuery();
            }

            sqlConnection.Close();
        }

        private IEnumerable<Book> GetAll()
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
                var command = new SqlCommand(LocalConst.Book.SelectAllCommand, sqlConnection);

                sqlConnection.Open();
                var reader = command.ExecuteReader();
                var books = LocalCommon.MapBooks(reader);
                sqlConnection.Close();

                return books;
            }
        }

        private void HandleAuthorUpdate(Book item, SqlConnection sqlConnection)
        {
            List<Guid> oldAuthors = this.GetAuthorsForBook(item.Id).Select(a => a.Id).ToList();
            List<Guid> newAuthors = item.AuthorIds.ToList();

            this.DeleteBookAuthorConnections(item.Id, oldAuthors, sqlConnection);
            this.AddBookAuthorConnections(item.Id, newAuthors, sqlConnection);
        }

        private IEnumerable<Author> MapAuthorFromIds(IDataReader reader)
        {
            List<Author> authors = new List<Author>();

            while (reader.Read())
            {
                Guid g = (Guid)reader[LocalConst.BookAuthor.Column.AuthorId];

                authors.Add(this.authorDao.Get(g));
            }

            return authors;
        }
    }
}