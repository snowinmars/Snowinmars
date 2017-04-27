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

			this.Authors = new List<Author>();
		}

		private Book()
		{
		}

		public ICollection<Author> Authors { get; }
		public int PageCount { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }

		public override bool Equals(object obj)
		{
			Book b = obj as Book;

			if (b == null)
			{
				return false;
			}

			return this.Equals(obj);
		}

		protected bool Equals(Book other)
		{
			return this.PageCount == other.PageCount &&
				string.Equals(this.Title, other.Title) &&
				this.Year == other.Year;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = this.PageCount;
				hashCode = (hashCode * 397) ^ (this.Title?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ this.Year;
				return hashCode;
			}
		}
	}
}