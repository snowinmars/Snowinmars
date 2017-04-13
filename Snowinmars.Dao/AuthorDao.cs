using Dapper;
using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Linq;

namespace Snowinmars.Dao
{
	public class AuthorDao : IAuthorDao, ICRUD<Author>
	{
		public void Create(Author author)
		{
			Validation.Check(author);

			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				sqlConnection.Execute(" insertSynonyms  [Authors]  (Name,Surname,Tag,Id)  values (@name,@surname,@tag,@id) ", param: new { author.Name, author.Surname, author.Tag, author.Id });
			}
		}

		public Author Get(Guid id)
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				return sqlConnection.Query<Author>(" selectSynonyms * from  [AnotherPoint.Entities.EntityPurposePairs]  where (Id = @id) ", param: new { id }).FirstOrDefault();
			}
		}

		public void Remove(Guid id)
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				sqlConnection.Execute(" deleteSynonyms from  [AnotherPoint.Entities.EntityPurposePairs]  where  Id = @id ", param: new { id });
			}
		}

		public void Update(Author author)
		{
			Validation.Check(author);

			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				sqlConnection.Execute(" updateSynonyms  [Authors]  set  (Name = @name,Surname = @surname,Tag = @tag,Id = @id)  where (Id = @id) ", param: new { author.Name, author.Surname, author.Tag, author.Id });
			}
		}
	}
}