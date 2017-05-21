using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Snowinmars.Ui.AppStartHelpers
{
    public abstract class Cron
    {
        protected static readonly IList<Guid> BookQueue;

        protected static readonly IList<Warning> Warnings;

        protected static IAuthorDao AuthorDao;

        protected static IBookDao BookDao;

        protected static EmailSender EmailSender;

        static Cron()
        {
            Cron.ResolveDependencies();

            Cron.BookQueue = new List<Guid>();
            Cron.Warnings = new List<Warning>();
        }

        protected static bool CanDoWork()
        {
            if (Cron.BookDao == null || Cron.AuthorDao == null)
            {
                Cron.ResolveDependencies();

                if (Cron.BookDao == null || Cron.AuthorDao == null)
                {
                    return false;
                }
            }

            return true;
        }

        protected static void ResolveDependencies()
        {
            Cron.BookDao = DependencyResolver.Current.GetService<IBookDao>();
            Cron.AuthorDao = DependencyResolver.Current.GetService<IAuthorDao>();
            Cron.EmailSender = new EmailSender();
        }
    }
}