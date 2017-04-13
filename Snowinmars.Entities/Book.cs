

using Snowinmars.Common;



namespace Snowinmars.Entities
{
 public  class Book 
	{




 public  System.String Title { get;  set; }

 public  System.UInt32 PageCount { get;  set; }

 public  System.Int32 Year { get;  set; }

 public  System.Collections.Generic.IEnumerable<Snowinmars.Entities.Author> Authors { get;  set; }

 public  System.Guid Id { get;  set; }


 public  Book (System.String title,System.UInt32 pageCount) 
{
	 this. Title = title;
 this. PageCount = pageCount;
  this .Authors =  new  System.Collections.Generic.List<Snowinmars.Entities.Author>();

}

	}
}
