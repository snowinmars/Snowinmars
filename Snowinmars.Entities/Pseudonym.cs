namespace Snowinmars.Entities
{
	public class Pseudonym
	{
		private static readonly Pseudonym NoneAuthor = new Pseudonym();
		public static Pseudonym None => Pseudonym.NoneAuthor;
		public string FamilyName { get; set; }
		public string FullMiddleName { get; set; }
		public string GivenName { get; set; }
	}
}