using Snowinmars.Entities;
using System.ComponentModel;

namespace Snowinmars.Ui.Models
{
	public class AuthorModel : EntityModel
	{
		//Family Name(Last Name)
		//Given Name(First Name)
		//Full Middle Name(If applicable)

		[DisplayName("Given name")]
		public string GivenName { get; set; }

		[DisplayName("Full middle name")]
		public string FullMiddleName { get; set; }

		[DisplayName("Shortcut")]
		public string Shortcut { get; set; }

		[DisplayName("Family name")]
		public string FamilyName { get; set; }

		public static AuthorModel Map(Author author)
		{
			AuthorModel authorModel = new AuthorModel
			{
				Id = author.Id,
				GivenName = author.GivenName,
				FullMiddleName = author.FullMiddleName,
				FamilyName = author.FamilyName,
				Shortcut = author.Shortcut,
			};

			return authorModel;
		}
	}
}