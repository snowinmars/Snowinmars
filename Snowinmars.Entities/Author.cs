namespace Snowinmars.Entities
{
	public class Author : Entity
	{
		public Author(string shortcut)
		{
			this.Shortcut = shortcut;

			this.MustInformAboutWarnings = false;
		}

		private Author()
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