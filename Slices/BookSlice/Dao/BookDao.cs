using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Snowinmars.AuthorSlice.AuthorDao.Interfaces;
using Snowinmars.AuthorSlice.AuthorEntities;
using Snowinmars.BookSlice.BookDao.Interfaces;
using Snowinmars.BookSlice.BookEntities;
using Snowinmars.Common;
using Validation = Snowinmars.BookSlice.BookEntities.Validation;

namespace Snowinmars.BookSlice.BookDao
{
	public class BookDao : IBookDao
	{
		private readonly IAuthorDao authorDao;

		public BookDao(IAuthorDao authorDao)
		{
			this.authorDao = authorDao;
		}

		public void Create(Book item)
		{
			this.Check(item);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				this.AddBook(item, sqlConnection);
				this.AddBookAuthorConnections(item.Id, item.AuthorIds, sqlConnection);
			}
		}

		private void Check(Book book, bool mustbeUnique = true)
		{
			Validation.DaoCheck(book.Id);

			foreach (var authorId in book.AuthorIds)
			{
				var author = this.authorDao.Get(authorId);
				AuthorSlice.AuthorEntities.Validation.DaoCheck(author); // TODO this is bug
			}

			if (mustbeUnique)
			{
				try
				{
					this.Get(book.Id);
				}
				catch (ObjectNotFoundException)
				{
					return; // there's no book with this ID
				}

				// if there was no exception that means that there is book with that ID, so I have to take care

				throw new ValidationException($"Book with id {book.Id} already exists");
			}
		}

		public Book Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Book.SelectCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, id.ConvertToDbValue());

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
				command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, book.Id.ConvertToDbValue());

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

		public IEnumerable<Book> Get(Func<Book, bool> filter)
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
					Guid bookId = reader[LocalConst.BookAuthor.Column.BookId].ConvertFromDbValue<Guid>();
					Guid authorId = reader[LocalConst.BookAuthor.Column.AuthorId].ConvertFromDbValue<Guid>();

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

				command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId.ConvertToDbValue());

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

				command.Parameters.AddWithValue(LocalConst.Book.Column.Id, id.ConvertToDbValue());

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

		public IEnumerable<Book> GetWishlist(string username, BookStatus status)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Book.GetWishlist, sqlConnection);

				var un = username.ConvertToDbValue();
				var s = status.ConvertToDbValue();

				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Owner, un);
				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Status, s);

				sqlConnection.Open();
				var reader = command.ExecuteReader();
				var books = LocalCommon.MapBooks(reader);
				sqlConnection.Close();

				return books;
			}
		}

		public void Update(Book item)
		{
			this.Check(item, mustbeUnique: false);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				this.HandleAuthorUpdate(item, sqlConnection);

				var authorsShortcuts = string.Join(",", item.AuthorShortcuts).ConvertToDbValue();

				// updating usual fields
				var command = new SqlCommand(LocalConst.Book.UpdateCommand, sqlConnection);

				var id = item.Id.ConvertToDbValue();
				var year = item.Year.ConvertToDbValue();
				var title = item.Title.ConvertToDbValue();
				var owner = item.Owner.ConvertToDbValue();
				var status = item.Status.ConvertToDbValue();
				var pageCount = item.PageCount.ConvertToDbValue();
				var bookshelf = item.Bookshelf.ConvertToDbValue();
				var liveLibUrl = item.LiveLibUrl.ConvertToDbValue();
				var libRusEcUrl = item.LibRusEcUrl.ConvertToDbValue();
				var flibustaUrl = item.FlibustaUrl.ConvertToDbValue();
				var additionalInfo = item.AdditionalInfo.ConvertToDbValue();
				var isSynchronized = item.IsSynchronized.ConvertToDbValue();
				var mustInformAboutWarnings = item.MustInformAboutWarnings.ConvertToDbValue();

				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Year, year);
				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Title, title);
				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Owner, owner);
				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Status, status);
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

			var id = book.Id.ConvertToDbValue();
			var year = book.Year.ConvertToDbValue();
			var title = book.Title.ConvertToDbValue();
			var owner = book.Owner.ConvertToDbValue();
			var status = book.Status.ConvertToDbValue();
			var pageCount = book.PageCount.ConvertToDbValue();
			var bookshelf = book.Bookshelf.ConvertToDbValue();
			var liveLibUrl = book.LiveLibUrl.ConvertToDbValue();
			var libRusEcUrl = book.LibRusEcUrl.ConvertToDbValue();
			var flibustaUrl = book.FlibustaUrl.ConvertToDbValue();
			var additionalInfo = book.AdditionalInfo.ConvertToDbValue();
			var isSynchronized = book.IsSynchronized.ConvertToDbValue();
			var authorsShortcuts = string.Join(",", book.AuthorShortcuts).ConvertToDbValue();
			var mustInformAboutWarnings = book.MustInformAboutWarnings.ConvertToDbValue();

			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, id);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Year, year);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Title, title);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Owner, owner);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Status, status);
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

			command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId.ConvertToDbValue());

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

			command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId.ConvertToDbValue());

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
