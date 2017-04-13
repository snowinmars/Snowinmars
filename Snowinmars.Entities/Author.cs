using System;

namespace Snowinmars.Entities
{
	public class Author
	{
		public Author(string name, string surname)
		{
			this.Name = name;
			this.Surname = surname;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Tag { get; set; }
	}
}