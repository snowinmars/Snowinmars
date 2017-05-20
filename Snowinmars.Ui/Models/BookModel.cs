using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Snowinmars.Ui.App_LocalResources;

namespace Snowinmars.Ui.Models
{
	public class BookModel : EntityModel
	{
		[Display(Name = "BookModel_Authors", ResourceType = typeof(Global))]
		public IEnumerable<Guid> AuthorModelIds { get; set; }

        [Required]
		[Display(Name = "BookModel_PageCount", ResourceType = typeof(Global))]
        public int PageCount { get; set; }

        [Required]
		[Display(Name = "BookModel_Title", ResourceType = typeof(Global))]
        public string Title { get; set; }

		[Display(Name = "BookModel_Year", ResourceType = typeof(Global))]
		public int Year { get; set; }

		[Display(Name = "BookModel_Authors", ResourceType = typeof(Global))]
        public IEnumerable<string> AuthorShortcuts { get; set; }

		[Display(Name = "BookModel_Bookshelf", ResourceType = typeof(Global))]
        public string Bookshelf { get; set; }

		[Display(Name = "BookModel_AdditionalInfo", ResourceType = typeof(Global))]
        public string AdditionalInfo { get; set; }

        [DataType(DataType.Url)]
		[Display(Name = "BookModel_LiveLibUrl", ResourceType = typeof(Global))]
        public string LiveLibUrl { get; set; }

        [DataType(DataType.Url)]
		[Display(Name = "BookModel_LibRusEcUrl", ResourceType = typeof(Global))]
        public string LibRusEcUrl { get; set; }

        [DataType(DataType.Url)]
		[Display(Name = "BookModel_FlibustaUrl", ResourceType = typeof(Global))]
        public string FlibustaUrl { get; set; }

		[Display(Name = "BookModel_InformAboutWarnings", ResourceType = typeof(Global))]
        public bool MustInformAboutWarnings { get; set; }

        [Required]
		[Display(Name = "BookModel_Owner", ResourceType = typeof(Global))]
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
            return new BookModel
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
                IsSynchronized = book.IsSynchronized,
			};
		}
	}
}