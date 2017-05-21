using Quartz;
using Snowinmars.Entities;
using System.Linq;

namespace Snowinmars.Ui.AppStartHelpers
{
    public class WarningJob : Cron, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (!Cron.CanDoWork())
            {
                return;
            }

            WarningJob.CheckBooks();
            WarningJob.CheckAuthors();
            WarningJob.CheckBookAuthorConnection();

            WarningJob.SendAllEmails();
        }

        private static void Check(Author author)
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

        private static void Check(Book book)
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

        private static void CheckAuthors()
        {
            var authors = Cron.AuthorDao.Get(a => true);

            foreach (var author in authors)
            {
                WarningJob.Check(author);
                WarningJob.Trim(author);
            }
        }

        private static void CheckBookAuthorConnection()
        {
            var connections = Cron.BookDao.GetAllBookAuthorConnections();

            foreach (var connection in connections)
            {
                var bookId = connection.Key;
                var authorId = connection.Value;

                WarningJob.CheckIsBookExists(bookId);
                WarningJob.CheckIsAuthorExists(authorId);
            }
        }

        private static void CheckBooks()
        {
            var books = Cron.BookDao.Get(b => true);

            foreach (var book in books)
            {
                WarningJob.Check(book);
                WarningJob.Trim(book);
            }
        }

        private static void CheckIsAuthorExists(System.Guid authorId)
        {
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

        private static void CheckIsBookExists(System.Guid bookId)
        {
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
        }

        private static void SendAllEmails()
        {
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

        private static void Trim(Book book)
        {
            if (book.Title.NeedToBeTrimed())
            {
                book.Title = book.Title.Trim();

                Cron.BookDao.Update(book);
            }
        }
    }
}