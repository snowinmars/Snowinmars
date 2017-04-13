

using Dapper;
using System;
using System.Linq;
using Snowinmars.Common;
using Snowinmars.Entities;
using Snowinmars.Dao.Interfaces;



namespace Snowinmars.Dao
{
 public  class BookDao  : Snowinmars.Dao.Interfaces.ICRUD< Snowinmars.Entities.Book >,Snowinmars.Dao.Interfaces.IBookDao
	{






 public   void  Create (Snowinmars.Entities.Book book)
{
	



	Validation.Check(book);

using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
sqlConnection.Execute(" insertSynonyms  [Books]  (Title,PageCount,Year,Authors,Id)  values (@title,@pageCount,@year,@authors,@id) ", param: new {book.Title,book.PageCount,book.Year,book.Authors,book.Id});
}


}

 public  Snowinmars.Entities.Book Get (System.Guid id)
{
	



	using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
return sqlConnection.Query<Snowinmars.Entities.Book>(" selectSynonyms * from  [AnotherPoint.Entities.EntityPurposePairs]  where (Id = @id) ", param: new {id}).FirstOrDefault();
}


}

 public   void  Remove (System.Guid id)
{
	



	using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
sqlConnection.Execute(" deleteSynonyms from  [AnotherPoint.Entities.EntityPurposePairs]  where  Id = @id ", param: new {id});
}


}

 public   void  Update (Snowinmars.Entities.Book book)
{
	



	Validation.Check(book);

using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
sqlConnection.Execute(" updateSynonyms  [Books]  set  (Title = @title,PageCount = @pageCount,Year = @year,Authors = @authors,Id = @id)  where (Id = @id) ", param: new {book.Title,book.PageCount,book.Year,book.Authors,book.Id});
}


}
	}
}
