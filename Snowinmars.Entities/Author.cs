using System;

namespace Snowinmars.Entities
{
	public class Author : Entity
	{
		public Author(string name, string surname)
		{
			this.Name = name;
			this.Surname = surname;
		}

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Tag { get; set; }
	}
}