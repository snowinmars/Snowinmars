namespace Snowinmars.AuthorSlice.AuthorEntities
{
	public class Name
	{
		public Name()
		{
			this.GivenName = "";
			this.FullMiddleName = "";
			this.FamilyName = "";
		}

		private static readonly Name NoName = new Name();
		public string FamilyName { get; set; }
		public string FullMiddleName { get; set; }
		public string GivenName { get; set; }
		public static Name None => Name.NoName;
	}
}
