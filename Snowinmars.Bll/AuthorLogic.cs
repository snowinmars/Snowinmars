

using Snowinmars.Bll.Interfaces;
using System;
using System.Linq;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Common;
using Snowinmars.Entities;



namespace Snowinmars.Bll
{
 public  class AuthorLogic  : Snowinmars.Bll.Interfaces.ICRUD< Snowinmars.Entities.Author >,Snowinmars.Bll.Interfaces.IAuthorLogic
	{



 private  Snowinmars.Dao.Interfaces.IAuthorDao AuthorLogicDestination;



 public  AuthorLogic (Snowinmars.Dao.Interfaces.IAuthorDao authorLogicDestination) 
{
	 this. AuthorLogicDestination = authorLogicDestination;

}


 public   void  Create (Snowinmars.Entities.Author author)
{
	



	Validation.Check(author);

 this .AuthorLogicDestination.Create(author);


}

 public  Snowinmars.Entities.Author Get (System.Guid id)
{
	



	  return   this .AuthorLogicDestination.Get(id);


}

 public   void  Remove (System.Guid id)
{
	



	 this .AuthorLogicDestination.Remove(id);


}

 public   void  Update (Snowinmars.Entities.Author author)
{
	



	Validation.Check(author);

 this .AuthorLogicDestination.Update(author);


}
	}
}
