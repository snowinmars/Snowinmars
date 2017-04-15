using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Snowinmars.Ui.Models
{
	public class BookModel : EntityModel
	{
		[DisplayName("Authors")]
		public IEnumerable<Guid> AuthorModelIds { get; set; }

		[DisplayName("Page count")]
		public int PageCount { get; set; }

		[DisplayName("Title")]
		public string Title { get; set; }

		[DisplayName("Year")]
		public int Year { get; set; }

		public static BookModel Map(Book book)
		{
			BookModel bookModel = new BookModel
			{
				Id = book.Id,
				PageCount = book.PageCount,
				Title = book.Title,
				Year = book.Year,
				AuthorModelIds = book.AuthorIds.ToList(),
			};

			return bookModel;
		}
	}
}