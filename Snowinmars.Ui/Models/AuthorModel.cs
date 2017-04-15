using Snowinmars.Entities;
using System.ComponentModel;

namespace Snowinmars.Ui.Models
{
	public class AuthorModel : EntityModel
	{
		//Family Name(Last Name)
		//Given Name(First Name)
		//Full Middle Name(If applicable)

		[DisplayName("First name")]
		public string FirstName { get; set; }

		[DisplayName("Last name")]
		public string LastName { get; set; }

		[DisplayName("Shortcut")]
		public string Shortcut { get; set; }

		[DisplayName("Surname")]
		public string Surname { get; set; }

		public static AuthorModel Map(Author author)
		{
			AuthorModel authorModel = new AuthorModel
			{
				Id = author.Id,
				FirstName = author.FirstName,
				LastName = author.LastName,
				Surname = author.Surname,
				Shortcut = author.Shortcut,
			};

			return authorModel;
		}
	}
}