using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Snowinmars.Dao
{
	public class AuthorDao : IAuthorDao, ICRUD<Author>
	{
		public void Create(Author item)
		{
			Validation.Check(item);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.InsertCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Id, item.Id);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.FirstName, item.FirstName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.LastName, item.LastName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Surname, item.Surname);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Shortcut, item.Shortcut);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public Author Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.SelectCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Id, id);

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				if (!reader.Read())
				{
					throw new ObjectNotFoundException();
				}

				var author = this.MapAuthor(reader);
				sqlConnection.Close();

				return author;
			}
		}

		public IEnumerable<Author> Get(Expression<Func<Book, bool>> filter)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.SelectAllCommand, sqlConnection);

				sqlConnection.Open();
				var reader = command.ExecuteReader();
				var authors = this.MapAuthors(reader);
				sqlConnection.Close();

				return authors;
			}
		}

		public void Remove(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.DeleteCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Author.Column.Id, id);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public void Update(Author item)
		{
			Validation.Check(item);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.UpdateCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Author.Column.Id, item.Id);
				command.Parameters.AddWithValue(LocalConst.Author.Column.FirstName, item.FirstName);
				command.Parameters.AddWithValue(LocalConst.Author.Column.LastName, item.LastName);
				command.Parameters.AddWithValue(LocalConst.Author.Column.Shortcut, item.Shortcut);
				command.Parameters.AddWithValue(LocalConst.Author.Column.Surname, item.Surname);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		private Author MapAuthor(IDataRecord reader)
		{
			Guid authorId = (Guid)reader[LocalConst.Author.Column.Id];
			string firstName = (string)reader[LocalConst.Author.Column.FirstName];
			string lastName = (string)reader[LocalConst.Author.Column.LastName];
			string surname = (string)reader[LocalConst.Author.Column.Surname];
			string shortcut = (string)reader[LocalConst.Author.Column.Shortcut];

			return new Author(firstName, lastName, surname)
			{
				Id = authorId,
				Shortcut = shortcut,
			};
		}

		private IEnumerable<Author> MapAuthors(IDataReader reader)
		{
			List<Author> authors = new List<Author>();

			while (reader.Read())
			{
				Author author = this.MapAuthor(reader);

				authors.Add(author);
			}

			return authors;
		}
	}
}