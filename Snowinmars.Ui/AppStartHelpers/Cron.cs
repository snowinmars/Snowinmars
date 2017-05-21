using Quartz;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Snowinmars.Ui.AppStartHelpers
{
    public abstract class Cron<T> : IJob
    {
        protected readonly IList<T> Queue;

        protected  IAuthorDao AuthorDao;

        protected  IBookDao BookDao;

        protected  EmailSender EmailSender;

        public bool IsSmtpReady => EmailSender.IsReady;

        public void TryLogin(string entropy) => EmailSender.TryLogin(entropy);

        protected Cron()
        {
            ResolveDependencies();

            this.Queue = new List<T>();
        }

        protected bool CanDoWork()
        {
            if (!IsSmtpReady)
            {
                return false;
            }

            if (BookDao == null || AuthorDao == null)
            {
                ResolveDependencies();

                if (BookDao == null || AuthorDao == null)
                {
                    return false;
                }
            }

            return true;
        }

        protected void ResolveDependencies()
        {
            BookDao = DependencyResolver.Current.GetService<IBookDao>();
            AuthorDao = DependencyResolver.Current.GetService<IAuthorDao>();
            EmailSender = new EmailSender();
        }

        public abstract void Execute(IJobExecutionContext context);
    }
}