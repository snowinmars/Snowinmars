using Snowinmars.Entities;
using System.ComponentModel;
using Snowinmars.Ui.ViewModels.Common;

namespace Snowinmars.Ui.ViewModels.Author
{
	public class UpdateViewModel : EntityViewModel
	{
		[DisplayName("Given name")]
		public string GivenName { get; set; }

		[DisplayName("Middle name")]
		public string FullMiddleName { get; set; }

		[DisplayName("Family name")]
		public string FamilyName { get; set; }

		[DisplayName("Pseudonym given name")]
		public string PseudonymGivenName { get; set; }

		[DisplayName("Pseudonym middle name")]
		public string PseudonymFullMiddleName { get; set; }

		[DisplayName("Pseudonym family name")]
		public string PseudonymFamilyName { get; set; }

		[DisplayName("Shortcut")]
		public string Shortcut { get; set; }

		public static UpdateViewModel Map(Entities.Author author)
		{
			UpdateViewModel createModel = new UpdateViewModel
			{
				Id = author.Id,
				GivenName = author.GivenName,
				FullMiddleName = author.FullMiddleName,
				FamilyName = author.FamilyName,
				Shortcut = author.Shortcut,
				PseudonymGivenName = author.Pseudonym.GivenName,
				PseudonymFullMiddleName = author.Pseudonym.FullMiddleName,
				PseudonymFamilyName = author.Pseudonym.FamilyName,
			};

			return createModel;
		}
	}
}