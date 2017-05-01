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

		public IEnumerable<string> AuthorShortcuts { get; set; }
		public string Bookshelf { get; set; }
		public string AdditionalInfo { get; set; }
		public string LiveLibUrl { get; set; }
		public string LibRusEcUrl { get; set; }
		public string FlibustaUrl { get; set; }
		public bool MustInformAboutWarnings { get; set; }

		public string Owner { get; set; }

		private static readonly BookModel EmptyBook = new BookModel
		{
			Owner = "",
			AdditionalInfo = "",
			AuthorModelIds = new List<Guid>(),
			AuthorShortcuts = new List<string>(),
			Bookshelf = "",
			FlibustaUrl = "",
			LibRusEcUrl = "",
			LiveLibUrl = "",
			MustInformAboutWarnings = true,
			PageCount = 0,
			Title = "",
			Year = 0,
			Id = Guid.Empty,
		};

		public static BookModel Empty => BookModel.EmptyBook;

		public static BookModel Map(Book book)
		{
			BookModel bookModel = new BookModel
			{
				Id = book.Id,
				PageCount = book.PageCount,
				Title = book.Title,
				Year = book.Year,
				AuthorModelIds = book.AuthorIds.ToList(),
				AuthorShortcuts = book.AuthorShortcuts.ToList(),
				AdditionalInfo = book.AdditionalInfo,
				Bookshelf = book.Bookshelf,
				FlibustaUrl = book.FlibustaUrl,
				LibRusEcUrl = book.LibRusEcUrl,
				LiveLibUrl = book.LiveLibUrl,
				Owner = book.Owner,
				MustInformAboutWarnings = book.MustInformAboutWarnings,
			};

			return bookModel;
		}
	}
}