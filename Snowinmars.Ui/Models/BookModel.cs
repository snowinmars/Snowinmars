using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Models
{
	public class BookModel : EntityModel
	{
		public IEnumerable<Guid> AuthorModelIds { get; set; }
		public int PageCount { get; set; }
		public string Title { get; set; }
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