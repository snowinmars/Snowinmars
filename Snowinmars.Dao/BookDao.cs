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
				BookDao.AddBook(item, sqlConnection);
				BookDao.AddBookAuthorConnections(item, sqlConnection);
			}
		}

		private static void AddBookAuthorConnections(Book book, SqlConnection sqlConnection)
		{
			var command = new SqlCommand(LocalConst.BookAuthor.InsertCommand, sqlConnection);

			command.Parameters.AddWithValue(LocalConst.BookAuthor.Column.BookId, book.Id);

			var authorIdParameter = new SqlParameter(LocalConst.BookAuthor.Parameter.AuthorId, SqlDbType.UniqueIdentifier);
			command.Parameters.Add(authorIdParameter);

			sqlConnection.Open();

			foreach (var authorId in book.AuthorIds)
			{
				command.Parameters[1].Value = authorId;

				command.ExecuteNonQuery();
			}

			sqlConnection.Close();
		}

		private static void AddBook(Book book, SqlConnection sqlConnection)
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
			//using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			//{
			//	sqlConnection.Execute(BookDao.DeleteCommand, param: new { id });
			//}

			throw new NotImplementedException();
		}

		public void Update(Book item)
		{
			//Validation.Check(book);

			//using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			//{
			//	sqlConnection.Execute(BookDao.UpdateCommand, param: new { book.Title, book.PageCount, book.Year, book.Authors, book.Id });
			//}

			throw new NotImplementedException();
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