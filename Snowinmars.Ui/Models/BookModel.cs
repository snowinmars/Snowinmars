using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Models
{
	public class BookModel
	{
		public Guid Id { get; set; }
		public IEnumerable<AuthorModel> AuthorModels { get; set; }
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
				AuthorModels = book.Authors.Select(AuthorModel.Map).ToList(),
			};

			return bookModel;
		}
	}
}