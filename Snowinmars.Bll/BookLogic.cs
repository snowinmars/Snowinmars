using Snowinmars.Bll.Interfaces;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll
{
    // ReSharper disable once RedundantExtendsListEntry
    // ReSharper disable once RedundantNameQualifier
    public class BookLogic : IBookLogic, Bll.Interfaces.ILayer<Book>
    {
        private readonly IBookDao bookDao;

        public BookLogic(IBookDao bookDao)
        {
            this.bookDao = bookDao;
        }

        public void Create(Book item)
        {
            Validation.Check(item);

            this.bookDao.Create(item);
        }

        public Book Get(Guid id)
        {
            Validation.Check(id);

            return this.bookDao.Get(id);
        }

        public IEnumerable<Book> Get(Func<Book, bool> filter)
        {
            return this.bookDao.Get(filter);
        }

        public IEnumerable<Author> GetAuthors(Guid bookId)
        {
            Validation.Check(bookId);

            return this.bookDao.GetAuthorsForBook(bookId);
        }

        public void Remove(Guid id)
        {
            Validation.Check(id);

            this.bookDao.Remove(id);
        }

        public void StartInformAboutWarnings(Guid bookId)
        {
            var book = this.bookDao.Get(bookId);

            this.bookDao.Update(book);
        }

        public void StartInformAboutWarnings()
        {
            var books = this.bookDao.Get(b => true);

            foreach (var book in books)
            {
                this.bookDao.Update(book);
            }
        }

        public void StopInformAboutWarnings(Guid bookId)
        {
            var book = this.bookDao.Get(bookId);


            this.bookDao.Update(book);
        }

        public void StopInformAboutWarnings()
        {
            var books = this.bookDao.Get(b => true);

            foreach (var book in books)
            {
                this.bookDao.Update(book);
            }
        }

	    public IEnumerable<Book> GetWishlist(string username)
	    {
		    Validation.CheckUsername(username);

			return this.bookDao.GetWishlist(username, BookStatus.Wished);
	    }

	    public void Update(Book item)
        {
            Validation.Check(item);

            this.bookDao.Update(item);
        }
    }
}