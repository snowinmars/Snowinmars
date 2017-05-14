using Quartz;
using Quartz.Impl;
using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Web.Mvc;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Snowinmars.Ui.QuartzCron), "Start")]

namespace Snowinmars.Ui
{
    public class QuartzCron : Cron, IDisposable
    {
        private static IScheduler scheduler;

        private static void Start()
        {
            try
            {
                QuartzCron.scheduler = StdSchedulerFactory.GetDefaultScheduler();

                QuartzCron.scheduler.Start();

                IJobDetail shortcutJob = JobBuilder.Create<ShortcutJob>()
                    .WithIdentity("shortcutJob", "basicGroup")
                    .Build();

                IJobDetail warningJob = JobBuilder.Create<WarningJob>()
                    .WithIdentity("warningJob", "basicGroup")
                    .Build();

                // Trigger the job to run now, and then repeat every 60 seconds
                ITrigger shortcutTrigger = TriggerBuilder.Create()
                    .WithIdentity("shortcutTrigger", "basicGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(60)
                        .RepeatForever())
                    .Build();

                ITrigger warningTrigger = TriggerBuilder.Create()
                   .WithIdentity("warningTrigger", "basicGroup")
                   .StartNow()
                   .WithSimpleSchedule(x => x
                       .WithIntervalInHours(24)
                       .RepeatForever())
                   .Build();

                QuartzCron.scheduler.ScheduleJob(shortcutJob, shortcutTrigger);
                QuartzCron.scheduler.ScheduleJob(warningJob, warningTrigger);
            }
            catch (SchedulerException e)
            {
                throw;
            }
        }

        public void Dispose()
        {
            QuartzCron.scheduler.Shutdown();
        }
    }

    public abstract class Cron
    {
        static Cron()
        {
            Cron.ResolveDependencies();

            Cron.BookQueue = new List<Guid>();
            Cron.Warnings = new List<Warning>();
        }

        protected static IAuthorDao AuthorDao;
        protected static IBookDao BookDao;
        protected static EmailSender EmailSender;

        protected static readonly IList<Guid> BookQueue;
        protected static readonly IList<Warning> Warnings;

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

    public class ShortcutJob : Cron, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (!Cron.CanDoWork())
            {
                return;
            }

            var unindexedBookIds = Cron.BookDao.SelectBooksUnindexedByShortcutsCommand().ToList();

            Cron.BookQueue.AddRange(unindexedBookIds);

            var copy = Cron.BookQueue.ToList(); // I need copy due to I want to remove books from queue during enumeration on this queue

            foreach (var bookId in copy)
            {
                var book = Cron.BookDao.Get(bookId);
                var authors = Cron.BookDao.GetAuthorsForBook(bookId);

                book.AuthorShortcuts.Clear();
                book.AuthorShortcuts.AddRange(authors.Select(a => a.Shortcut));
                book.IsSynchronized = true;

                Cron.BookDao.Update(book);

                Cron.BookQueue.Remove(bookId);
            }
        }
    }

    public class WarningJob : Cron, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (!Cron.CanDoWork())
            {
                return;
            }

            var books = Cron.BookDao.Get(b => true);

            foreach (var book in books)
            {
                WarningJob.CheckWarnings(book);
                WarningJob.Trim(book);
            }

            var authors = Cron.AuthorDao.Get(a => true);

            foreach (var author in authors)
            {
                WarningJob.CheckWarnings(author);
                WarningJob.Trim(author);
            }

            var connections = Cron.BookDao.GetAllBookAuthorConnections();

            foreach (var connection in connections)
            {
                var bookId = connection.Key;
                var authorId = connection.Value;

                try
                {
                    var checker = Cron.BookDao.Get(bookId);
                }
                catch
                {
                    Cron.Warnings.Add(new Warning(bookId)
                    {
                        Message = "Can't get book by id",
                    });
                }

                try
                {
                    var checker = Cron.AuthorDao.Get(authorId);
                }
                catch
                {
                    Cron.Warnings.Add(new Warning(authorId)
                    {
                        Message = "Can't get author by id",
                    });
                }
            }

            var copy = Cron.Warnings.ToList();

            foreach (var warning in copy)
            {
                Cron.EmailSender.Send(warning);
                Cron.Warnings.Remove(warning);
            }
        }

        private static void Trim(Author author)
		{
			if (author.GivenName.NeedToBeTrimed() ||
				author.FullMiddleName.NeedToBeTrimed() ||
				author.FamilyName.NeedToBeTrimed() ||
				(author.Pseudonym?.GivenName.NeedToBeTrimed() ?? false) ||
				(author.Pseudonym?.FullMiddleName.NeedToBeTrimed() ?? false) ||
				(author.Pseudonym?.FamilyName.NeedToBeTrimed() ?? false))
			{
				author.GivenName = author.GivenName.Trim();
				author.FamilyName = author.FamilyName.Trim();
				author.FullMiddleName = author.FullMiddleName.Trim();
				author.Pseudonym.GivenName = author.Pseudonym?.GivenName.Trim();
				author.Pseudonym.FamilyName = author.Pseudonym?.FamilyName.Trim();
				author.Pseudonym.FullMiddleName = author.Pseudonym?.FullMiddleName.Trim();

				Cron.AuthorDao.Update(author);
			}
		}

        private static void CheckWarnings(Author author)
		{
			if (string.IsNullOrWhiteSpace(author.FamilyName))
			{
				Cron.Warnings.Add(new Warning(author.Id)
				{
					Message = $"Author '{author.Shortcut}' has empty family name",
				});
			}

			if (string.IsNullOrWhiteSpace(author.GivenName))
			{
				Cron.Warnings.Add(new Warning(author.Id)
				{
					Message = $"Author '{author.Shortcut}' has empty given name",
				});
			}
		}

		private static void Trim(Book book)
		{
			if (book.Title.NeedToBeTrimed())
			{
				book.Title = book.Title.Trim();

				Cron.BookDao.Update(book);
			}
		}

		private static void CheckWarnings(Book book)
		{
			if (book.PageCount <= 0)
			{
				Cron.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. has less or equals to zero pages",
				});
			}

			if (string.IsNullOrWhiteSpace(book.Title))
			{
				Cron.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. has empty title",
				});
			}

			if (book.Year == 0)
			{
				Cron.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. was published in 0 year. Is it ok or you just forgot to add the year?"
				});
			}

			if (!book.AuthorIds.Any())
			{
				Cron.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. has no authors",
				});
			}
		}
    }

    internal static class Extensions
    {
        internal static bool NeedToBeTrimed(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return str.StartsWith(" ") || str.EndsWith(" ");
        }
    }

    public class EmailSender
    {
        private readonly string username;
        private readonly SecureString password;
        private readonly SmtpClient smtpClient;

        private readonly string toAdmin;
        private const string DefaultRe = "Snowinmars system message";
        private const string WarningRe = "Snowinmars system warning you";
        private readonly string from;

        public EmailSender()
        {
            this.toAdmin = ConfigurationManager.AppSettings["emailUsername"];
            this.from = ConfigurationManager.AppSettings["emailUsername"];

            this.username = ConfigurationManager.AppSettings["emailUsername"];
            this.password = EmailSender.DecryptString(ConfigurationManager.AppSettings["emailPassword"]);

            this.smtpClient = new SmtpClient
            {
                Port = 587,
                Host = ConfigurationManager.AppSettings["emailHost"],
                EnableSsl = true,
                Timeout = (int)TimeSpan.FromMinutes(10).TotalMilliseconds,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.username, this.password)
            };
        }

        private static readonly byte[] Entropy = Encoding.Unicode.GetBytes(ConfigurationManager.AppSettings["emailSalt"]);

        public static string EncryptString(SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                Encoding.Unicode.GetBytes(EmailSender.ToInsecureString(input)),
                EmailSender.Entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    EmailSender.Entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return EmailSender.ToSecureString(Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);

            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }

            return returnValue;
        }

        public void Send(string message, string to, string re = EmailSender.DefaultRe)
        {
            MailMessage mailMessage = new MailMessage(this.from, to, re, message)
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            this.smtpClient.Send(mailMessage);
        }

        public void Send(string message, IEnumerable<string> to)
        {
            foreach (var t in to)
            {
                this.Send(message, t);
            }
        }

        public void Send(Warning warning) => this.Send(warning, this.toAdmin);

        public void Send(Warning warning, string to, string re = EmailSender.WarningRe)
        {
            string message = "Snowinmars system warning: " + Environment.NewLine +
                             $"Warning id: {warning.Id}" + Environment.NewLine +
                             $"Entity id: {warning.EntityId}" + Environment.NewLine +
                             warning.Message + Environment.NewLine + Environment.NewLine +
                             "check " + Constant.SiteUrl + " for details";

            this.Send(message, to, re);
        }
    }
}