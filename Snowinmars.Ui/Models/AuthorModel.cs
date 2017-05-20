using System;
using Snowinmars.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Snowinmars.Ui.App_LocalResources;

namespace Snowinmars.Ui.Models
{
	public class AuthorModel : EntityModel
	{
		[Display(Name = "AuthorModel_GivenName", ResourceType = typeof(Global))]
		public string GivenName { get; set; }

		[Display(Name = "AuthorModel_FullMiddleName", ResourceType = typeof(Global))]
		public string FullMiddleName { get; set; }

        [Required]
		[Display(Name = "AuthorModel_Shortcut", ResourceType = typeof(Global))]
        public string Shortcut { get; set; }

		[DisplayName("Pseudonym given name")]
		[Display(Name = "AuthorModel_PseudonymGivenName", ResourceType = typeof(Global))]
	    public string PseudonymGivenName { get; set; }

        [DisplayName("Pseudonym full middle name")]
		[Display(Name = "AuthorModel_PseudonymFullMiddleName", ResourceType = typeof(Global))]
	    public string PseudonymFullMiddleName { get; set; }

        [DisplayName("Pseudonym family name")]
		[Display(Name = "AuthorModel_PseudonymFamilyName", ResourceType = typeof(Global))]
	    public string PseudonymFamilyName { get; set; }

        [DisplayName("Have I inform you about warnings?")]
		[Display(Name = "AuthorModel_InformAboutWarnings", ResourceType = typeof(Global))]
        public bool MustInformAboutWarnings { get; set; }

        [DisplayName("Family name")]
		[Display(Name = "AuthorModel_FamilyName", ResourceType = typeof(Global))]
		public string FamilyName { get; set; }

        private static readonly AuthorModel EmptyAuthor = new AuthorModel
		{
			Id = Guid.Empty,
			Shortcut = "",
			FamilyName = "",
			GivenName = "",
			FullMiddleName = "",
		};

		public static AuthorModel Emtpy => AuthorModel.EmptyAuthor;

		public static AuthorModel Map(Author author)
		{
            return new AuthorModel
			{
				Id = author.Id,
				GivenName = author.GivenName,
				FullMiddleName = author.FullMiddleName,
				FamilyName = author.FamilyName,
				Shortcut = author.Shortcut,
                IsSynchronized = author.IsSynchronized,
                MustInformAboutWarnings = author.MustInformAboutWarnings,
                PseudonymGivenName = author.Pseudonym?.GivenName ?? "",
                PseudonymFullMiddleName= author.Pseudonym?.FullMiddleName ?? "",
                PseudonymFamilyName= author.Pseudonym?.FamilyName ?? "",
            };
		}
	}
}