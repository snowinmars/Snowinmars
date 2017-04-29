namespace Snowinmars.Entities
{
	public class Author : Entity
	{
		public Author(string shortcut)
		{
			this.Shortcut = shortcut;
		}

		private Author()
		{
		}

		public string GivenName { get; set; }
		public string FullMiddleName { get; set; }
		public string Shortcut { get; set; }
		public string FamilyName { get; set; }
		public Pseudonym Pseudonym { get; set; }
	}
}