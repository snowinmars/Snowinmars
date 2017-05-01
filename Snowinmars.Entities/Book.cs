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

			this.AuthorIds = new List<Guid>();
			this.AuthorShortcuts = new List<string>();
		}

		private Book()
		{
		}

		public ICollection<Guid> AuthorIds { get; }
		public int PageCount { get; set; }
		public bool MustInformAboutWarnings { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public IList<string> AuthorShortcuts { get; }
		public string Bookshelf { get; set; }
		public string AdditionalInfo { get; set; }
		public string LiveLibUrl { get; set; }
		public string LibRusEcUrl { get; set; }
		public string Owner { get; set; }
		public string FlibustaUrl { get; set; }
	}
}