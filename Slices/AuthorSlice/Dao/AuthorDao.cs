using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Snowinmars.AuthorSlice.AuthorDao.Interfaces;
using Snowinmars.AuthorSlice.AuthorEntities;
using Snowinmars.Common;

namespace Snowinmars.AuthorSlice.AuthorDao
{
	public class AuthorDao : IAuthorDao
	{
		public AuthorDao()
		{
		}

		public void Create(Author item)
		{
			this.Check(item, mustbeUnique: true);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.InsertCommand, sqlConnection);

				var id = item.Id.ConvertToDbValue();
				var shortcut = item.Shortcut.ConvertToDbValue();
				var givenName = item.Name.GivenName.ConvertToDbValue();
				var familyName = item.Name.FamilyName.ConvertToDbValue();
				var fullMiddleName = item.Name.FullMiddleName.ConvertToDbValue();
				var isSynchronized = false.ConvertToDbValue();
				var pseudonymGivenName = item.Pseudonym.GivenName.ConvertToDbValue();
				var pseudonymFamilyName = item.Pseudonym.FamilyName.ConvertToDbValue();
				var pseudonymFullMiddleName = item.Pseudonym.FullMiddleName.ConvertToDbValue();
				var mustInformAboutWarnings = item.MustInformAboutWarnings.ConvertToDbValue();

				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Shortcut, shortcut);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.GivenName, givenName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.FamilyName, familyName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.IsSynchronized, isSynchronized);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.FullMiddleName, fullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymGivenName, pseudonymGivenName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFamilyName, pseudonymFamilyName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFullMiddleName, pseudonymFullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.MustInformAboutWarnings, mustInformAboutWarnings);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		private void Check(Author item, bool mustbeUnique)
		{
			Validation.DaoCheck(item);
			if (mustbeUnique)
			{
				try
				{
					Get(item.Id);
				}
				catch (ObjectNotFoundException)
				{
					return;
				}

				// if there was no exception that means that there is author with that ID, so I have to take care

				throw new ValidationException($"Author with id {item.Id} already exists");
			}
		}

		public Author Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.SelectCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Id, id.ConvertToDbValue());

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

		public IEnumerable<Author> Get(Func<Author, bool> filter)
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

				command.Parameters.AddWithValue(LocalConst.Author.Column.Id, id.ConvertToDbValue());

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public void Update(Author item)
		{
			this.Check(item, mustbeUnique: false);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Author.UpdateCommand, sqlConnection);

				var id = item.Id.ConvertToDbValue();
				var shortcut = item.Shortcut.ConvertToDbValue();
				var givenName = item.Name.GivenName.ConvertToDbValue();
				var familyName = item.Name.FamilyName.ConvertToDbValue();
				var fullMiddleName = item.Name.FullMiddleName.ConvertToDbValue();
				var isSynchronized = false.ConvertToDbValue();
				var pseudonymGivenName = item.Pseudonym.GivenName.ConvertToDbValue();
				var pseudonymFamilyName = item.Pseudonym.FamilyName.ConvertToDbValue();
				var pseudonymFullMiddleName = item.Pseudonym.FullMiddleName.ConvertToDbValue();
				var mustInformAboutWarnings = item.MustInformAboutWarnings.ConvertToDbValue();

				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.Shortcut, shortcut);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.GivenName, givenName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.FamilyName, familyName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.IsSynchronized, isSynchronized);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.FullMiddleName, fullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymGivenName, pseudonymGivenName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFamilyName, pseudonymFamilyName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.PseudonymFullMiddleName, pseudonymFullMiddleName);
				command.Parameters.AddWithValue(LocalConst.Author.Parameter.MustInformAboutWarnings, mustInformAboutWarnings);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}
	}
}
