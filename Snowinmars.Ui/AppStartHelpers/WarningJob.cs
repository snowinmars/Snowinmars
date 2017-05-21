using Quartz;
using Snowinmars.Entities;
using System.Linq;

namespace Snowinmars.Ui.AppStartHelpers
{
    public class WarningJob : Cron<Warning>
    {
        public override void Execute(IJobExecutionContext context)
        {
            if (QuartzCron.WarningJob == null)
            {
                QuartzCron.WarningJob = this;
            }

            if (!CanDoWork())
            {
                return;
            }

            CheckBooks();
            CheckAuthors();
            CheckBookAuthorConnection();

            SendAllEmails();
        }

        private void Check(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.FamilyName))
            {
                Queue.Add(new Warning(author.Id)
                {
                    Message = $"Author '{author.Shortcut}' has empty family name",
                });
            }

            if (string.IsNullOrWhiteSpace(author.GivenName))
            {
                Queue.Add(new Warning(author.Id)
                {
                    Message = $"Author '{author.Shortcut}' has empty given name",
                });
            }
        }

        private void Check(Book book)
        {
            if (book.PageCount <= 0)
            {
                Queue.Add(new Warning(book.Id)
                {
                    Message = $"Book '{book.Title}'/{book.Year} y. has less or equals to zero pages",
                });
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                Queue.Add(new Warning(book.Id)
                {
                    Message = $"Book '{book.Title}'/{book.Year} y. has empty title",
                });
            }

            if (book.Year == 0)
            {
                Queue.Add(new Warning(book.Id)
                {
                    Message = $"Book '{book.Title}'/{book.Year} y. was published in 0 year. Is it ok or you just forgot to add the year?"
                });
            }

            if (!book.AuthorIds.Any())
            {
                Queue.Add(new Warning(book.Id)
                {
                    Message = $"Book '{book.Title}'/{book.Year} y. has no authors",
                });
            }
        }

        private void CheckAuthors()
        {
            var authors = AuthorDao.Get(a => true);

            foreach (var author in authors)
            {
                Check(author);
                Trim(author);
            }
        }

        private void CheckBookAuthorConnection()
        {
            var connections = BookDao.GetAllBookAuthorConnections();

            foreach (var connection in connections)
            {
                var bookId = connection.Key;
                var authorId = connection.Value;

                CheckIsBookExists(bookId);
                CheckIsAuthorExists(authorId);
            }
        }

        private void CheckBooks()
        {
            var books = BookDao.Get(b => true);

            foreach (var book in books)
            {
                Check(book);
                Trim(book);
            }
        }

        private  void CheckIsAuthorExists(System.Guid authorId)
        {
            try
            {
                var checker = AuthorDao.Get(authorId);
            }
            catch
            {
                Queue.Add(new Warning(authorId)
                {
                    Message = "Can't get author by id",
                });
            }
        }

        private void CheckIsBookExists(System.Guid bookId)
        {
            try
            {
                var checker = BookDao.Get(bookId);
            }
            catch
            {
                Queue.Add(new Warning(bookId)
                {
                    Message = "Can't get book by id",
                });
            }
        }

        private void SendAllEmails()
        {
            var copy = Queue.ToList();

            foreach (var warning in copy)
            {
                EmailSender.Send(warning);
                Queue.Remove(warning);
            }
        }

        private void Trim(Author author)
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

                AuthorDao.Update(author);
            }
        }

        private void Trim(Book book)
        {
            if (book.Title.NeedToBeTrimed())
            {
                book.Title = book.Title.Trim();

                BookDao.Update(book);
            }
        }
    }
}