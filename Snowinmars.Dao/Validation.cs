using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Activities;
using System.Data;

namespace Snowinmars.Dao
{
    internal sealed class Validation
    {
        private static IAuthorDao AuthorDao { get; set; }

        private static IBookDao BookDao { get; set; }

        public static void Check(Author author, bool mustbeUnique = true)
        {
            Validation.Check(author.Id);
            Validation.Check(author.Pseudonym);

            if (mustbeUnique)
            {
                try
                {
                    Validation.AuthorDao.Get(author.Id);
                }
                catch (ObjectNotFoundException)
                {
                    return;
                }

                // if there was no exception that means that there is author with that ID, so I have to take care

                throw new ValidationException($"Author with id {author.Id} already exists");
            }
        }

        public static void Check(Book book, bool mustbeUnique = true)
        {
            Validation.Check(book.Id);

            foreach (var author in book.Authors)
            {
                Validation.Check(author, false); // TODO this is bug
            }

            if (mustbeUnique)
            {
                try
                {
                    Validation.BookDao.Get(book.Id);
                }
                catch (ObjectNotFoundException)
                {
                    return; // there's no book with this ID
                }

                // if there was no exception that means that there is book with that ID, so I have to take care

                throw new ValidationException($"Book with id {book.Id} already exists");
            }
        }

        public static void Check(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ValidationException("Author's id can't be empty");
            }
        }

        public static void Set(IBookDao bookDao)
        {
            Validation.BookDao = bookDao;
        }

        public static void Set(IAuthorDao authorDao)
        {
            Validation.AuthorDao = authorDao;
        }

        private static void Check(Pseudonym pseudonym)
        {
            if (pseudonym == null)
            {
                throw new ValidationException("Pseudonym can't be null");
            }

            if (pseudonym.GivenName == null)
            {
                throw new ValidationException("Pseudonym's given name can't be null");
            }

            if (pseudonym.FullMiddleName == null)
            {
                throw new ValidationException("Pseudonym's full middle name can't be null");
            }

            if (pseudonym.FamilyName == null)
            {
                throw new ValidationException("Pseudonym's family name can't be null");
            }
        }
    }
}