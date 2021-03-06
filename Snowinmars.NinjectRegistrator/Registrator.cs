﻿using Ninject;
using Snowinmars.Bll;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Dao;
using Snowinmars.Dao.Interfaces;

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