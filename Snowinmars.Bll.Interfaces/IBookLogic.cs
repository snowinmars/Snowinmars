using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snowinmars.Bll.Interfaces
{
    public interface IBookLogic : ILayer<Book>
    {
        IEnumerable<Author> GetAuthors(Guid bookId);

        void StartInformAboutWarnings(Guid bookId);

        void StartInformAboutWarnings();

        void StopInformAboutWarnings(Guid bookId);

        void StopInformAboutWarnings();

	    IEnumerable<Book> GetWishlist(string username);
    }
}