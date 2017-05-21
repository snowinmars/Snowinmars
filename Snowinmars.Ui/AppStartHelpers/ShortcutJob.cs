using System;
using Quartz;
using Snowinmars.Common;
using System.Linq;

namespace Snowinmars.Ui.AppStartHelpers
{
    public class ShortcutJob : Cron<Guid>
    {
        public override void Execute(IJobExecutionContext context)
        {
            if (QuartzCron.ShortcutJob == null)
            {
                QuartzCron.ShortcutJob = this;
            }

            if (!CanDoWork())
            {
                return;
            }

            var unindexedBookIds = BookDao.SelectBooksUnindexedByShortcutsCommand().ToList();

            Queue.AddRange(unindexedBookIds);

            // I need copy due to I want to remove books from queue during enumeration on this queue
            var copy = Queue.ToList();

            foreach (var bookId in copy)
            {
                HandleBook(bookId);
            }
        }

        private void HandleBook(Guid bookId)
        {
            var book = BookDao.Get(bookId);
            var authors = BookDao.GetAuthorsForBook(bookId);

            book.AuthorShortcuts.Clear();
            book.AuthorShortcuts.AddRange(authors.Select(a => a.Shortcut));
            book.IsSynchronized = true;

            BookDao.Update(book);

            Queue.Remove(bookId);
        }
    }
}