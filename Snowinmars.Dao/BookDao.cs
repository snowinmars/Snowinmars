using Dapper;
using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Linq;

namespace Snowinmars.Dao
{
	public class BookDao : IBookDao, ICRUD<Book>
	{
		public void Create(Book book)
		{
			Validation.Check(book);

			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				sqlConnection.Execute(" insertSynonyms  [Books]  (Title,PageCount,Year,Authors,Id)  values (@title,@pageCount,@year,@authors,@id) ", param: new { book.Title, book.PageCount, book.Year, book.Authors, book.Id });
			}
		}

		public Book Get(Guid id)
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				return sqlConnection.Query<Book>(" selectSynonyms * from  [AnotherPoint.Entities.EntityPurposePairs]  where (Id = @id) ", param: new { id }).FirstOrDefault();
			}
		}

		public void Remove(Guid id)
		{
			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				sqlConnection.Execute(" deleteSynonyms from  [AnotherPoint.Entities.EntityPurposePairs]  where  Id = @id ", param: new { id });
			}
		}

		public void Update(Book book)
		{
			Validation.Check(book);

			using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
			{
				sqlConnection.Execute(" updateSynonyms  [Books]  set  (Title = @title,PageCount = @pageCount,Year = @year,Authors = @authors,Id = @id)  where (Id = @id) ", param: new { book.Title, book.PageCount, book.Year, book.Authors, book.Id });
			}
		}
	}
}