using System;

namespace Snowinmars.Entities
{
	public class Author : Entity
	{
		private static readonly Author EmptyAuthor = new Author();

		public static Author Empty => Author.EmptyAuthor;

		public Author(string shortcut)
		{
			if (string.IsNullOrWhiteSpace(shortcut))
			{
				throw new ArgumentException("Shortcut can't be empty");
			}

			this.Shortcut = shortcut;

			this.GivenName = "";
			this.FullMiddleName = "";
			this.FamilyName = "";

			this.Pseudonym = new Pseudonym()
			{
				GivenName = "",
				FullMiddleName = "",
				FamilyName = "",
			};

			this.MustInformAboutWarnings = true;
		}

		private Author() : this("")
		{
		}

		public string GivenName { get; set; }
		public string FullMiddleName { get; set; }
		public string Shortcut { get; set; }
		public bool MustInformAboutWarnings { get; set; }
		public string FamilyName { get; set; }
		public Pseudonym Pseudonym { get; set; }
	}
}