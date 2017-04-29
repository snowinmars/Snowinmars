using Snowinmars.BackgroundWorkers;
using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BackgroundDaoWorker), "Start")]

namespace Snowinmars.BackgroundWorkers
{
	public static class BackgroundDaoWorker
	{
		private static bool NeedToBeTrimed(this string str)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				return false;
			}

			return str.StartsWith(" ") || str.EndsWith(" ");
		}

		private static readonly Timer TinyTimer;
		private static readonly Timer FatTimer;
		private static readonly JobHost JobHost;
		private static IBookDao bookDao;
		private static IAuthorDao authorDao;

		public static void Start()
		{
			BackgroundDaoWorker.TinyTimer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(1));

			// Figure how much time until the night time
			DateTime now = DateTime.Now;
			DateTime nightTime = DateTime.Today.AddHours(23);

			// If it's already past night time, wait until night time tomorrow
			if (now > nightTime)
			{
				nightTime = nightTime.AddDays(1.0);
			}

			int msUntilFour = (int)((nightTime - now).TotalMilliseconds);

			//msUntilFour = 10000;

			BackgroundDaoWorker.FatTimer.Change(msUntilFour, (int)TimeSpan.FromDays(1).TotalMilliseconds);
		}

		private static void OnTinyTimerElapsed(object sender)
		{
			BackgroundDaoWorker.JobHost.DoWork(() => { BackgroundDaoWorker.DoWork(); });
		}

		private static readonly IList<Guid> BookQueue;

		static BackgroundDaoWorker()
		{
			BackgroundDaoWorker.EmailSender = DependencyResolver.Current.GetService<EmailSender>();
			BackgroundDaoWorker.BookQueue = new List<Guid>();
			BackgroundDaoWorker.Warnings = new List<Warning>();

			BackgroundDaoWorker.JobHost = new JobHost();
			BackgroundDaoWorker.TinyTimer = new Timer(BackgroundDaoWorker.OnTinyTimerElapsed);
			BackgroundDaoWorker.FatTimer = new Timer(BackgroundDaoWorker.OnFatTimerElapsed);
		}

		private readonly static EmailSender EmailSender;

		private static readonly IList<Warning> Warnings;

		private static void OnFatTimerElapsed(object state)
		{
			if (!BackgroundDaoWorker.CanDoWork())
			{
				return;
			}

			var books = BackgroundDaoWorker.bookDao.Get(b => true);

			foreach (var book in books)
			{
				BackgroundDaoWorker.CheckWarnings(book);
				BackgroundDaoWorker.Trim(book);
			}

			var authors = BackgroundDaoWorker.authorDao.Get(a => true);

			foreach (var author in authors)
			{
				BackgroundDaoWorker.CheckWarnings(author);
				BackgroundDaoWorker.Trim(author);
			}

			var connections = BackgroundDaoWorker.bookDao.GetAllBookAuthorConnections();

			foreach (var connection in connections)
			{
				var bookId = connection.Key;
				var authorId = connection.Value;

				try
				{
					var checker = BackgroundDaoWorker.bookDao.Get(bookId);
				}
				catch
				{
					BackgroundDaoWorker.Warnings.Add(new Warning(bookId)
					{
						Message = "Can't get book by id",
					});
				}

				try
				{
					var checker = BackgroundDaoWorker.authorDao.Get(authorId);
				}
				catch
				{
					BackgroundDaoWorker.Warnings.Add(new Warning(authorId)
					{
						Message = "Can't get author by id",
					});
				}
			}

			foreach (var warning in BackgroundDaoWorker.Warnings)
			{
				BackgroundDaoWorker.EmailSender.Send(warning);
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

				BackgroundDaoWorker.authorDao.Update(author);
			}
		}

		private static void CheckWarnings(Author author)
		{
			if (string.IsNullOrWhiteSpace(author.FamilyName))
			{
				BackgroundDaoWorker.Warnings.Add(new Warning(author.Id)
				{
					Message = $"Author '{author.Shortcut}' has empty family name",
				});
			}

			if (string.IsNullOrWhiteSpace(author.GivenName))
			{
				BackgroundDaoWorker.Warnings.Add(new Warning(author.Id)
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

				BackgroundDaoWorker.bookDao.Update(book);
			}
		}

		private static void CheckWarnings(Book book)
		{
			if (book.PageCount <= 0)
			{
				BackgroundDaoWorker.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. has less or equals to zero pages",
				});
			}

			if (string.IsNullOrWhiteSpace(book.Title))
			{
				BackgroundDaoWorker.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. has empty title",
				});
			}

			if (book.Year == 0)
			{
				BackgroundDaoWorker.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. was published in 0 year. Is it ok or you just forgot to add the year?"
				});
			}

			if (!book.AuthorIds.Any())
			{
				BackgroundDaoWorker.Warnings.Add(new Warning(book.Id)
				{
					Message = $"Book '{book.Title}'/{book.Year} y. has no authors",
				});
			}
		}

		public static void AddBookToQueue(Guid bookId)
		{
			BackgroundDaoWorker.BookQueue.Add(bookId);
		}

		private static void DoWork()
		{
			if (!BackgroundDaoWorker.CanDoWork())
			{
				return;
			}

			var unindexedBookIds = BackgroundDaoWorker.bookDao.SelectBooksUnindexedByShortcutsCommand().ToList();

			if (unindexedBookIds.Any())
			{
				BackgroundDaoWorker.BookQueue.AddRange(unindexedBookIds);
			}

			var copy = BackgroundDaoWorker.BookQueue.ToList();

			foreach (var bookId in copy)
			{
				BackgroundDaoWorker.DoWork(bookId);
				BackgroundDaoWorker.BookQueue.Remove(bookId);
			}
		}

		private static bool CanDoWork()
		{
			if (BackgroundDaoWorker.bookDao == null || BackgroundDaoWorker.authorDao == null)
			{
				BackgroundDaoWorker.ResolveDependencies();

				if (BackgroundDaoWorker.bookDao == null || BackgroundDaoWorker.authorDao == null)
				{
					return false;
				}
			}

			return true;
		}

		private static void ResolveDependencies()
		{
			BackgroundDaoWorker.bookDao = DependencyResolver.Current.GetService<IBookDao>();
			BackgroundDaoWorker.authorDao = DependencyResolver.Current.GetService<IAuthorDao>();
		}

		private static void DoWork(Guid bookId)
		{
			var book = BackgroundDaoWorker.bookDao.Get(bookId);
			var authors = BackgroundDaoWorker.bookDao.GetAuthorsForBook(bookId);

			book.AuthorShortcuts.AddRange(authors.Select(a => a.Shortcut));

			BackgroundDaoWorker.bookDao.Update(book);
		}
	}
}