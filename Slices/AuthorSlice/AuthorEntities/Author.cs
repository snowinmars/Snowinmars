using System;
using Snowinmars.Common;

namespace Snowinmars.AuthorSlice.AuthorEntities
{
	public class Author : Entity
	{
		private static readonly Author EmptyAuthor;

		static Author()
		{
			Author.EmptyAuthor = new Author("EmptyAuthor");
		}

		public Author(string shortcut)
		{
			if (string.IsNullOrWhiteSpace(shortcut))
			{
				throw new ArgumentException("Shortcut can't be empty");
			}

			this.Shortcut = shortcut;

			this.Name = new Name();
			this.Pseudonym = new Name();

			this.MustInformAboutWarnings = false;
		}

		protected Author() : this("")
		{
		}

		public static Author Empty => Author.EmptyAuthor;
		public bool MustInformAboutWarnings { get; set; }
		public Name Name { get; set; }
		public Name Pseudonym { get; set; }
		public string Shortcut { get; set; }
	}
}
