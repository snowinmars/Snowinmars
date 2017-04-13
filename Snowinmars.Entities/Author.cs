

using Snowinmars.Common;



namespace Snowinmars.Entities
{
 public  class Author 
	{




 public  System.String Name { get;  set; }

 public  System.String Surname { get;  set; }

 public  System.String Tag { get;  set; }

 public  System.Guid Id { get;  set; }


 public  Author (System.String name,System.String surname) 
{
	 this. Name = name;
 this. Surname = surname;

}

	}
}
