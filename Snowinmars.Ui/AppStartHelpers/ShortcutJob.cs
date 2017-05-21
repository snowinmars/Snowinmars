using System;
using Quartz;
using Snowinmars.Common;
using System.Linq;

namespace Snowinmars.Ui.AppStartHelpers
{
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

            // I need copy due to I want to remove books from queue during enumeration on this queue
            var copy = Cron.BookQueue.ToList();

            foreach (var bookId in copy)
            {
                ShortcutJob.HandleBook(bookId);
            }
        }

        private static void HandleBook(Guid bookId)
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