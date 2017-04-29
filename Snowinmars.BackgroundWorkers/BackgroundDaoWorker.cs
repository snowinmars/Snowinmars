using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Web.Mvc;
using Snowinmars.BackgroundWorkers;
using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BackgroundDaoWorker), "Start")]

namespace Snowinmars.BackgroundWorkers
{
	public static class BackgroundDaoWorker
	{
		private static readonly Timer Timer;
		private static readonly JobHost JobHost;
		private static IBookDao bookDao;

		public static void Start()
		{
			BackgroundDaoWorker.Timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(1));
		}

		private static void OnTimerElapsed(object sender)
		{
			BackgroundDaoWorker.JobHost.DoWork(() => { BackgroundDaoWorker.DoWork(); });
		}

		private static readonly IList<Guid> BookQueue;

		static BackgroundDaoWorker()
		{
			BackgroundDaoWorker.bookDao = DependencyResolver.Current.GetService<IBookDao>();

			BackgroundDaoWorker.JobHost = new JobHost();
			BackgroundDaoWorker.Timer = new Timer(BackgroundDaoWorker.OnTimerElapsed);
			BackgroundDaoWorker.BookQueue = new List<Guid>();
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
			if (BackgroundDaoWorker.bookDao == null)
			{
				BackgroundDaoWorker.ResolveDependencies();

				if (BackgroundDaoWorker.bookDao == null)
				{
					return false;
				}
			}

			return true;
		}

		private static void ResolveDependencies()
		{
			BackgroundDaoWorker.bookDao = DependencyResolver.Current.GetService<IBookDao>();
		}

		private static void DoWork(Guid bookId)
		{
			var book = BackgroundDaoWorker.bookDao.Get(bookId);
			var authors = BackgroundDaoWorker.bookDao.GetAuthors(bookId);

			book.AuthorShortcuts.AddRange(authors.Select(a => a.Shortcut));

			BackgroundDaoWorker.bookDao.Update(book);
		}
	}
}
