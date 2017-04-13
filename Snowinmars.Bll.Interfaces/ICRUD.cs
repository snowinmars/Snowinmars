

using Snowinmars.Common;
using Snowinmars.Entities;


namespace Snowinmars.Bll.Interfaces
{
	 public  interface ICRUD<T>  
	{


 void  Create (T book);

T Get (System.Guid id);

 void  Remove (System.Guid id);

 void  Update (T book);
	}
}