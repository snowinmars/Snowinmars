using System;
using System.Collections.Generic;

namespace Snowinmars.Entities
{
	public class Book : Entity
	{
		private Book()
		{
		}

		public Book(string title, int pageCount)
		{
			this.Title = title;
			this.PageCount = pageCount;

			this.Authors = new List<Author>();
		}

		public ICollection<Author> Authors { get; }
		public int PageCount { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
	}
}