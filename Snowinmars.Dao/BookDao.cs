using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Snowinmars.BackgroundWorkers;

namespace Snowinmars.Dao
{
	public class BookDao : IBookDao, ICRUD<Book>
	{
		private readonly IAuthorDao authorDao;

		public BookDao(IAuthorDao authorDao)
		{
			this.authorDao = authorDao;
		}

		public void Create(Book item)
		{
			Validation.Check(item);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				this.AddBook(item, sqlConnection);
				this.AddBookAuthorConnections(item.Id, item.AuthorIds, sqlConnection);

				BackgroundDaoWorker.AddBookToQueue(item.Id);
			}
		}

		public Book Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Book.SelectCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, id);

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
				command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, book.Id);

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
			IEnumerable<Book> books = this.GetAll();

			foreach (var book in books)
			{
				book.AuthorIds.AddRange(this.GetAuthors(book.Id).Select(a => a.Id));
			}

			return books;
		}

		public IEnumerable<Author> GetAuthors(Guid bookId)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.BookAuthor.SelectByBookCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId);

				sqlConnection.Open();
				var reader = command.ExecuteReader();
				var authors = this.MapAuthorFromIds(reader);
				sqlConnection.Close();

				return authors;
			}
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


		public void Remove(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Book.DeleteCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Book.Column.Id, id);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public void Update(Book item)
		{
			Validation.Check(item);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				// updating usual fields
				var command = new SqlCommand(LocalConst.Book.UpdateCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Book.Column.Id, item.Id);
				command.Parameters.AddWithValue(LocalConst.Book.Column.Title, item.Title);
				command.Parameters.AddWithValue(LocalConst.Book.Column.PageCount, item.PageCount);
				command.Parameters.AddWithValue(LocalConst.Book.Column.Year, item.Year);
				command.Parameters.AddWithValue(LocalConst.Book.Column.AuthorsShortcuts, string.Join(",", item.AuthorShortcuts));

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();

				//handle with authors

				this.HandleAuthorUpdate(item, sqlConnection);

				BackgroundDaoWorker.AddBookToQueue(item.Id);
			}
		}

		private void HandleAuthorUpdate(Book item, SqlConnection sqlConnection)
		{
			// Authors can be updated with three ways: one can add new ones, can remove old ones and can don't touch authors at all

			// I want to compare the old and the new one collections.
			List<Guid> oldAuthors = this.GetAuthors(item.Id).Select(a => a.Id).ToList();
			List<Guid> newAuthors = item.AuthorIds.ToList();

			// I remove all matching entries from both of them
			for (var i = 0; i < oldAuthors.Count; i++)
			{
				var oldAuthor = oldAuthors[i];

				if (newAuthors.Contains(oldAuthor))
				{
					newAuthors.Remove(oldAuthor);
					oldAuthors.Remove(oldAuthor);
				}
			}

			// and then if there's something in remainder in old author's collection  - I remove em.
			if (oldAuthors.Any())
			{
				this.DeleteBookAuthorConnections(item.Id, oldAuthors, sqlConnection);
			}

			// or add if there is something in new author's collection.
			if (newAuthors.Any())
			{
				this.AddBookAuthorConnections(item.Id, newAuthors, sqlConnection);
			}
		}

		private void AddBook(Book book, SqlConnection sqlConnection)
		{
			var command = new SqlCommand(LocalConst.Book.InsertCommand, sqlConnection);

			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, book.Id);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Title, book.Title);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Year, book.Year);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.PageCount, book.PageCount);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.AuthorsShortcuts, book.AuthorShortcuts);

			sqlConnection.Open();
			command.ExecuteNonQuery();
			sqlConnection.Close();
		}

		private void AddBookAuthorConnections(Guid bookId, IEnumerable<Guid> authorIds, SqlConnection sqlConnection)
		{
			var command = new SqlCommand(LocalConst.BookAuthor.InsertCommand, sqlConnection);

			command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId);

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

			command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId);

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

	
	}
}