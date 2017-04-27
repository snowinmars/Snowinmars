using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Snowinmars.Ui.ViewModels.Common;

namespace Snowinmars.Ui.ViewModels.Book
{
	public class DetailsViewModel : EntityViewModel
	{
		[DisplayName("Authors")]
		public IEnumerable<Entities.Author> AuthorModels { get; set; }

		[DisplayName("Pages")]
		public int PageCount { get; set; }

		[DisplayName("Title")]
		public string Title { get; set; }

		[DisplayName("Year")]
		public int Year { get; set; }

		public static DetailsViewModel Map(Entities.Book book)
		{
			DetailsViewModel createModel = new DetailsViewModel
			{
				Id = book.Id,
				PageCount = book.PageCount,
				Title = book.Title,
				Year = book.Year,
				AuthorModels = book.Authors.ToList(),
			};

			return createModel;
		}
	}
}