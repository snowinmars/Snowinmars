using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Snowinmars.AuthorSlice.AuthorBll;
using Snowinmars.AuthorSlice.AuthorBll.Interfaces;
using Snowinmars.AuthorSlice.AuthorDao;
using Snowinmars.AuthorSlice.AuthorDao.Interfaces;
using Snowinmars.BookSlice.BookBll;
using Snowinmars.BookSlice.BookBll.Interfaces;
using Snowinmars.BookSlice.BookDao;
using Snowinmars.BookSlice.BookDao.Interfaces;
using Snowinmars.UserSlice.UserBll;
using Snowinmars.UserSlice.UserBll.Interfaces;
using Snowinmars.UserSlice.UserDao;
using Snowinmars.UserSlice.UserDao.Interfaces;

namespace Snowinmars.NinjectRegistrator
{
	public static class Registrator
	{
		public static void Register(IKernel kernel)
		{
			kernel.Bind<IBookLogic>().To<BookLogic>();
			kernel.Bind<IBookDao>().To<BookDao>();
			kernel.Bind<IAuthorLogic>().To<AuthorLogic>();
			kernel.Bind<IAuthorDao>().To<AuthorDao>();
			kernel.Bind<IUserLogic>().To<UserLogic>();
			kernel.Bind<IUserDao>().To<UserDao>();
		}
	}
}
