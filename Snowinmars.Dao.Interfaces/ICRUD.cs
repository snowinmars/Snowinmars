

using Snowinmars.Common;
using Snowinmars.Entities;


namespace Snowinmars.Dao.Interfaces
{
	 public  interface ICRUD<T>  
	{


 void  Create (T book);

T Get (System.Guid id);

 void  Remove (System.Guid id);

 void  Update (T book);
	}
}