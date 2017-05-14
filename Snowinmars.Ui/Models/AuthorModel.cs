using System;
using Snowinmars.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Snowinmars.Ui.Models
{
	public class AuthorModel : EntityModel
	{
		[DisplayName("Given name")]
		public string GivenName { get; set; }

		[DisplayName("Full middle name")]
		public string FullMiddleName { get; set; }

		[DisplayName("Shortcut")]
        [Required]
        public string Shortcut { get; set; }

		[DisplayName("Pseudonym given name")]
	    public string PseudonymGivenName { get; set; }

        [DisplayName("Pseudonym full middle name")]
	    public string PseudonymFullMiddleName { get; set; }

		[DisplayName("Pseudonym family name")]
	    public string PseudonymFamilyName { get; set; }

		[DisplayName("Have I inform you about warnings?")]
        public bool MustInformAboutWarnings { get; set; }

        [DisplayName("Family name")]
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
			};
		}
	}
}