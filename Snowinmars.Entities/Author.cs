using System;

namespace Snowinmars.Entities
{
	public class Author : Entity
	{
		private Author()
		{
		}

		public Author(string firstName, string lastName, string surname)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Surname = surname;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string Surname { get; set; }

		public string Shortcut { get; set; }
	}
}