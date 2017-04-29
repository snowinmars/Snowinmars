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

			this.AuthorIds = new List<Guid>();
			this.AuthorShortcuts = new List<string>();
		}

		private Book()
		{
		}

		public ICollection<Guid> AuthorIds { get; }
		public int PageCount { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public IList<string> AuthorShortcuts { get; }
	}
}