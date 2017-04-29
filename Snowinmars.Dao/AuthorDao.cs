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

				var id = LocalCommon.ConvertToDbValue(item.Id);
				var givenName = LocalCommon.ConvertToDbValue(item.GivenName);
				var fullMiddleName = LocalCommon.ConvertToDbValue(item.FullMiddleName);
				var shortcut = LocalCommon.ConvertToDbValue(item.Shortcut);
				var familyName = LocalCommon.ConvertToDbValue(item.FamilyName);
				var pseudonymGivenName = LocalCommon.ConvertToDbValue(item.Pseudonym.GivenName);
				var pseudonymFullMiddleName = LocalCommon.ConvertToDbValue(item.Pseudonym.FullMiddleName);
				var pseudonymFamilyName = LocalCommon.ConvertToDbValue(item.Pseudonym.FamilyName);
				var mustInformAboutWarnings = LocalCommon.ConvertToDbValue(item.MustInformAboutWarnings);

				command.Parameters.AddWithValue(LocalConst.Author.Column.Id, id);
				command.Parameters.AddWithValue(LocalConst.Author.Column.GivenName, givenName);
				command.Parameters.AddWithValue(LocalConst.Author.Column.FullMiddleName, fullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Column.Shortcut, shortcut);
				command.Parameters.AddWithValue(LocalConst.Author.Column.FamilyName, familyName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymGivenName, pseudonymGivenName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFullMiddleName, pseudonymFullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFamilyName, pseudonymFamilyName);
				command.Parameters.AddWithValue(LocalConst.Book.Column.MustInformAboutWarnings, mustInformAboutWarnings);

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

				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Id, LocalCommon.ConvertToDbValue(id));

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				if (!reader.Read())
				{
					throw new ObjectNotFoundException();
				}

				var author = LocalCommon.MapAuthor(reader);
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
				var authors = LocalCommon.MapAuthors(reader);
				sqlConnection.Close();

				return authors;
			}
		}

		public void Remove(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.DeleteCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Author.Column.Id, LocalCommon.ConvertToDbValue(id));

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

				var id = LocalCommon.ConvertToDbValue(item.Id);
				var givenName = LocalCommon.ConvertToDbValue(item.GivenName);
				var fullMiddleName = LocalCommon.ConvertToDbValue(item.FullMiddleName);
				var shortcut = LocalCommon.ConvertToDbValue(item.Shortcut);
				var familyName = LocalCommon.ConvertToDbValue(item.FamilyName);
				var pseudonymGivenName = LocalCommon.ConvertToDbValue(item.Pseudonym?.GivenName);
				var pseudonymFullMiddleName = LocalCommon.ConvertToDbValue(item.Pseudonym?.FullMiddleName);
				var pseudonymFamilyName = LocalCommon.ConvertToDbValue(item.Pseudonym?.FamilyName);
				var mustInformAboutWarnings = LocalCommon.ConvertToDbValue(item.MustInformAboutWarnings);

				command.Parameters.AddWithValue(LocalConst.Author.Column.Id, id);
				command.Parameters.AddWithValue(LocalConst.Author.Column.GivenName, givenName);
				command.Parameters.AddWithValue(LocalConst.Author.Column.FullMiddleName, fullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Column.Shortcut, shortcut);
				command.Parameters.AddWithValue(LocalConst.Author.Column.FamilyName, familyName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymGivenName, pseudonymGivenName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFullMiddleName, pseudonymFullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFamilyName, pseudonymFamilyName);
				command.Parameters.AddWithValue(LocalConst.Book.Column.MustInformAboutWarnings, mustInformAboutWarnings);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}
	}
}