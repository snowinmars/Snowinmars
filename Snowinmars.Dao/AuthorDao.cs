

using Dapper;
using System;
using System.Linq;
using Snowinmars.Common;
using Snowinmars.Entities;
using Snowinmars.Dao.Interfaces;



namespace Snowinmars.Dao
{
 public  class AuthorDao  : Snowinmars.Dao.Interfaces.ICRUD< Snowinmars.Entities.Author >,Snowinmars.Dao.Interfaces.IAuthorDao
	{






 public   void  Create (Snowinmars.Entities.Author author)
{
	



	Validation.Check(author);

using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
sqlConnection.Execute(" insertSynonyms  [Authors]  (Name,Surname,Tag,Id)  values (@name,@surname,@tag,@id) ", param: new {author.Name,author.Surname,author.Tag,author.Id});
}


}

 public  Snowinmars.Entities.Author Get (System.Guid id)
{
	



	using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
return sqlConnection.Query<Snowinmars.Entities.Author>(" selectSynonyms * from  [AnotherPoint.Entities.EntityPurposePairs]  where (Id = @id) ", param: new {id}).FirstOrDefault();
}


}

 public   void  Remove (System.Guid id)
{
	



	using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
sqlConnection.Execute(" deleteSynonyms from  [AnotherPoint.Entities.EntityPurposePairs]  where  Id = @id ", param: new {id});
}


}

 public   void  Update (Snowinmars.Entities.Author author)
{
	



	Validation.Check(author);

using (var sqlConnection = new System.Data.SqlClient.SqlConnection(Constant.ConnectionString))
{
sqlConnection.Execute(" updateSynonyms  [Authors]  set  (Name = @name,Surname = @surname,Tag = @tag,Id = @id)  where (Id = @id) ", param: new {author.Name,author.Surname,author.Tag,author.Id});
}


}
	}
}
