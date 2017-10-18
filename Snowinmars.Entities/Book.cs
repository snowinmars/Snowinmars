using System;
using System.Collections.Generic;

namespace Snowinmars.Entities
{
    public class Book : Entity
    {
        public Book(string title, int pageCount)
        {
            this.Title = title;
            this.PageCount = pageCount;

            this.MustInformAboutWarnings = true;
			this.Status = BookStatus.Wished;

            this.Authors = new List<Author>();
        }

        private Book() : this("", 0)
        {
        }

		public BookStatus Status { get; set; }
        public string AdditionalInfo { get; set; }
        public ICollection<Author> Authors { get; }
        public string Bookshelf { get; set; }
        public string FlibustaUrl { get; set; }
        public string LibRusEcUrl { get; set; }
        public string LiveLibUrl { get; set; }
        public bool MustInformAboutWarnings { get; set; }
        public string Owner { get; set; }
        public int PageCount { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}