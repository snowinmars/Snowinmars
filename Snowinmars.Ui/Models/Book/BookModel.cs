using Snowinmars.Ui.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Models
{
    public class BookModel : EntityModel
    {
        private static readonly BookModel EmptyBook = new BookModel
        {
            Id = Guid.Empty,
            Year = 0,
            Owner = "",
            Title = "",
			Status = BookStatus.Wished,
            PageCount = 0,
            Bookshelf = "",
            LiveLibUrl = "",
            FlibustaUrl = "",
            LibRusEcUrl = "",
            AdditionalInfo = "",
            IsSynchronized = true,
            MustInformAboutWarnings = true,
            AuthorModelIds = new List<Guid>(),
            AuthorShortcuts = new List<string>(),
        };

		[Display(Name = "BookModel_Status", ResourceType = typeof(Global))]
		public BookStatus Status { get; set; }

		[Display(Name = "BookModel_AdditionalInfo", ResourceType = typeof(Global))]
        public string AdditionalInfo { get; set; }

        [Display(Name = "BookModel_Authors", ResourceType = typeof(Global))]
        public IEnumerable<Guid> AuthorModelIds { get; set; }

        [Display(Name = "BookModel_Authors", ResourceType = typeof(Global))]
        public IEnumerable<string> AuthorShortcuts { get; set; }

        [Display(Name = "BookModel_Bookshelf", ResourceType = typeof(Global))]
        public string Bookshelf { get; set; }

        public static BookModel Empty => BookModel.EmptyBook;

        [DataType(DataType.Url)]
        [Display(Name = "BookModel_FlibustaUrl", ResourceType = typeof(Global))]
        public string FlibustaUrl { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "BookModel_LibRusEcUrl", ResourceType = typeof(Global))]
        public string LibRusEcUrl { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "BookModel_LiveLibUrl", ResourceType = typeof(Global))]
        public string LiveLibUrl { get; set; }

        [Display(Name = "Model_InformAboutWarnings", ResourceType = typeof(Global))]
        public bool MustInformAboutWarnings { get; set; }

        [Required]
        [Display(Name = "BookModel_Owner", ResourceType = typeof(Global))]
        public string Owner { get; set; }

        [Required]
        [Display(Name = "BookModel_PageCount", ResourceType = typeof(Global))]
        public int PageCount { get; set; }

        [Required]
        [Display(Name = "BookModel_Title", ResourceType = typeof(Global))]
        public string Title { get; set; }

        [Display(Name = "BookModel_Year", ResourceType = typeof(Global))]
        public int Year { get; set; }
    }
}