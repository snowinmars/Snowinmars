

using Snowinmars.Bll.Interfaces;
using System;
using System.Linq;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Common;
using Snowinmars.Entities;



namespace Snowinmars.Bll
{
 public  class BookLogic  : Snowinmars.Bll.Interfaces.ICRUD< Snowinmars.Entities.Book >,Snowinmars.Bll.Interfaces.IBookLogic
	{



 private  Snowinmars.Dao.Interfaces.IBookDao BookLogicDestination;



 public  BookLogic (Snowinmars.Dao.Interfaces.IBookDao bookLogicDestination) 
{
	 this. BookLogicDestination = bookLogicDestination;

}


 public   void  Create (Snowinmars.Entities.Book book)
{
	



	Validation.Check(book);

 this .BookLogicDestination.Create(book);


}

 public  Snowinmars.Entities.Book Get (System.Guid id)
{
	



	  return   this .BookLogicDestination.Get(id);


}

 public   void  Remove (System.Guid id)
{
	



	 this .BookLogicDestination.Remove(id);


}

 public   void  Update (Snowinmars.Entities.Book book)
{
	



	Validation.Check(book);

 this .BookLogicDestination.Update(book);


}
	}
}
