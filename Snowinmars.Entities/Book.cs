using System;
using System.Collections.Generic;

namespace Snowinmars.Entities
{
	public class Book : Entity
	{
		public Book(string title, uint pageCount)
		{
			this.Title = title;
			this.PageCount = pageCount;

			this.Authors = new List<Author>();
		}

		public IEnumerable<Author> Authors { get; }
		public uint PageCount { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
	}
}