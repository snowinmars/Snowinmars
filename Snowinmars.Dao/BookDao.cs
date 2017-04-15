using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				this.AddBook(item, sqlConnection);
				this.AddBookAuthorConnections(item.Id, item.AuthorIds, sqlConnection);
			}
		}

		private void AddBookAuthorConnections(Guid bookId, IEnumerable<Guid> authorIds, SqlConnection sqlConnection)
		{
			var command = new SqlCommand(LocalConst.BookAuthor.InsertCommand, sqlConnection);

			command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId);

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

		private void AddBook(Book book, SqlConnection sqlConnection)
		{
			var command = new SqlCommand(LocalConst.Book.InsertCommand, sqlConnection);

			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, book.Id);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Title, book.Title);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.Year, book.Year);
			command.Parameters.AddWithValue(LocalConst.Book.Parameter.PageCount, book.PageCount);

			sqlConnection.Open();
			command.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public Book Get(Guid id)
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Book.SelectCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Book.Parameter.Id, id);

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				if (!reader.Read())
				{
					throw new ObjectNotFoundException();
				}

				Guid bookId = (Guid) reader[LocalConst.Book.Column.Id];
				string title = (string) reader[LocalConst.Book.Column.Title];
				int year = (int) reader[LocalConst.Book.Column.Year];
				int pageCount = (int) reader[LocalConst.Book.Column.PageCount];

				sqlConnection.Close();

				Book book = new Book(title, pageCount)
				{
					Id = bookId,
					Year = year,
				};
				
				//////////////////////

				command.CommandText = LocalConst.BookAuthor.SelectByBookCommand;

				command.Parameters.Clear();
				command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, book.Id);

				sqlConnection.Open();
				reader = command.ExecuteReader();

				while (reader.Read())
				{
					Guid authorId = (Guid) reader[LocalConst.BookAuthor.Column.AuthorId];

					book.AuthorIds.Add(authorId);
				}

				sqlConnection.Close();

				return book;
			}
		}

		public void Remove(Guid id)
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
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

			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Book.UpdateCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Book.Column.Id, item.Id);
				command.Parameters.AddWithValue(LocalConst.Book.Column.Title, item.Title);
				command.Parameters.AddWithValue(LocalConst.Book.Column.PageCount, item.PageCount);
				command.Parameters.AddWithValue(LocalConst.Book.Column.Year, item.Year);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();

				List<Guid> oldAuthors = this.GetAuthors(item.Id).Select(a => a.Id).ToList();
				List<Guid> newAuthors = item.AuthorIds.ToList();

				for (var i = 0; i < oldAuthors.Count; i++)
				{
					var oldAuthor = oldAuthors[i];

					if (newAuthors.Contains(oldAuthor))
					{
						newAuthors.Remove(oldAuthor);
						oldAuthors.Remove(oldAuthor);
					}

				}

				if (oldAuthors.Any())
				{
					this.DeleteBookAuthorConnections(item.Id, oldAuthors, sqlConnection);
				}

				if (newAuthors.Any())
				{
					this.AddBookAuthorConnections(item.Id, newAuthors, sqlConnection);
				}
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

		private IEnumerable<Book> GetAll()
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Book.SelectAllCommand, sqlConnection);

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				List<Book> books = new List<Book>();

				while (reader.Read())
				{
					Guid bookId = (Guid)reader[LocalConst.Book.Column.Id];
					string title = (string)reader[LocalConst.Book.Column.Title];
					int year = (int)reader[LocalConst.Book.Column.Year];
					int pageCount = (int)reader[LocalConst.Book.Column.PageCount];

					Book book = new Book(title, pageCount)
					{
						Id = bookId,
						Year = year,
					};

					books.Add(book);
				}

				sqlConnection.Close();

				return books;
			}
		}

		public IEnumerable<Author> GetAuthors(Guid bookId)
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.BookAuthor.SelectByBookCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, bookId);

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				List<Author> authors = new List<Author>();
				while (reader.Read())
				{
					Guid g = (Guid) reader[LocalConst.BookAuthor.Column.AuthorId];

					authors.Add(this.authorDao.Get(g));
				}

				sqlConnection.Close();

				return authors;
			}
		}
	}
}