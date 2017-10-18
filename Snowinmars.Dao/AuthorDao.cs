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
	public class AuthorDao : IAuthorDao, ILayer<Author>
	{
		public AuthorDao()
		{
			Validation.Set(this);
		}

		public void Create(Author item)
		{
			Validation.Check(item);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("Author_Insert");

				var id = LocalCommon.ConvertToDbValue(item.Id);
				var shortcut = LocalCommon.ConvertToDbValue(item.Shortcut);
				var givenName = LocalCommon.ConvertToDbValue(item.GivenName);
				var familyName = LocalCommon.ConvertToDbValue(item.FamilyName);
				var fullMiddleName = LocalCommon.ConvertToDbValue(item.FullMiddleName);
				var isSynchronized = LocalCommon.ConvertToDbValue(false);
				var pseudonymGivenName = LocalCommon.ConvertToDbValue(item.Pseudonym.GivenName);
				var pseudonymFamilyName = LocalCommon.ConvertToDbValue(item.Pseudonym.FamilyName);
				var pseudonymFullMiddleName = LocalCommon.ConvertToDbValue(item.Pseudonym.FullMiddleName);

				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.Id, SqlDbType.UniqueIdentifier,  id);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.Shortcut, SqlDbType.NVarChar, shortcut);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.GivenName, SqlDbType.NVarChar, givenName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.FamilyName, SqlDbType.NVarChar, familyName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.IsSynchronized, SqlDbType.Bit, isSynchronized);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.FullMiddleName, SqlDbType.NVarChar, fullMiddleName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.PseudonymGivenName, SqlDbType.NVarChar, pseudonymGivenName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.PseudonymFamilyName, SqlDbType.NVarChar, pseudonymFamilyName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.PseudonymFullMiddleName, SqlDbType.NVarChar, pseudonymFullMiddleName);

				var command = databaseCommand.GetSqlCommand(sqlConnection);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public Author Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("Author_Get");

				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.Id, SqlDbType.UniqueIdentifier, LocalCommon.ConvertToDbValue(id));

				var command = databaseCommand.GetSqlCommand(sqlConnection);

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				if (!reader.Read())
				{
					throw new ObjectNotFoundException();
				}

				var authorPair = LocalCommon.MapAuthor(reader);
				sqlConnection.Close();

				return authorPair.author;
			}
		}

		public IEnumerable<Author> Get(Func<Author, bool> filter)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("Author_GetAll");

				var command = databaseCommand.GetSqlCommand(sqlConnection);

				sqlConnection.Open();
				var reader = command.ExecuteReader();
				var authorPairs = LocalCommon.MapAuthors(reader);
				sqlConnection.Close();

				return authorPairs.Select(p => p.author);
			}
		}

		public void Remove(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("Author_Delete");

				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.Id, SqlDbType.UniqueIdentifier, LocalCommon.ConvertToDbValue(id));

				var command = databaseCommand.GetSqlCommand(sqlConnection);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public void Update(Author item)
		{
			Validation.Check(item, mustbeUnique: false);

			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("Author_Update");

				var id = LocalCommon.ConvertToDbValue(item.Id);
				var shortcut = LocalCommon.ConvertToDbValue(item.Shortcut);
				var givenName = LocalCommon.ConvertToDbValue(item.GivenName);
				var familyName = LocalCommon.ConvertToDbValue(item.FamilyName);
				var fullMiddleName = LocalCommon.ConvertToDbValue(item.FullMiddleName);
                var isSynchronized = LocalCommon.ConvertToDbValue(false);
				var pseudonymGivenName = LocalCommon.ConvertToDbValue(item.Pseudonym?.GivenName);
				var pseudonymFamilyName = LocalCommon.ConvertToDbValue(item.Pseudonym?.FamilyName);
				var pseudonymFullMiddleName = LocalCommon.ConvertToDbValue(item.Pseudonym?.FullMiddleName);

				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.Id, SqlDbType.UniqueIdentifier, id);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.Shortcut, SqlDbType.NVarChar, shortcut);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.GivenName, SqlDbType.NVarChar, givenName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.FamilyName, SqlDbType.NVarChar, familyName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.IsSynchronized, SqlDbType.Bit, isSynchronized);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.FullMiddleName, SqlDbType.NVarChar, fullMiddleName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.PseudonymGivenName, SqlDbType.NVarChar, pseudonymGivenName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.PseudonymFamilyName, SqlDbType.NVarChar, pseudonymFamilyName);
				databaseCommand.AddInputParameter(LocalConst.Author.Parameter.PseudonymFullMiddleName, SqlDbType.NVarChar, pseudonymFullMiddleName);

				var command = databaseCommand.GetSqlCommand(sqlConnection);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}
	}
}