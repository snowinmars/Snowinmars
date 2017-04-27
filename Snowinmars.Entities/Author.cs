using System;

namespace Snowinmars.Entities
{
	public class Author : Entity
	{
		private Pseudonym pseudonym;

		public Author(string givenName)
		{
			this.GivenName = givenName;

			this.FamilyName = string.Empty;
			this.FullMiddleName = string.Empty;
			this.Shortcut = string.Empty;

			this.Pseudonym = Pseudonym.None;
		}

		private Author()
		{
		}

		public string FamilyName { get; set; }
		public string FullMiddleName { get; set; }
		public string GivenName { get; set; }
		public bool IsPseudonym => this.Pseudonym == Pseudonym.None;

		public Pseudonym Pseudonym
		{
			get
			{
				return this.pseudonym ?? Pseudonym.None;
			}

			set
			{
				this.pseudonym = value ?? Pseudonym.None;
			}
		}

		public string Shortcut { get; set; }

		public override bool Equals(object obj)
		{
			Author a = obj as Author;

			if (a == null)
			{
				return false;
			}

			return this.Equals(a);
		}

		protected bool Equals(Author other)
		{
			return string.Equals(this.FamilyName, other.FamilyName) &&
			       string.Equals(this.FullMiddleName, other.FullMiddleName) &&
			       string.Equals(this.GivenName, other.GivenName) &&
			       string.Equals(this.Shortcut, other.Shortcut);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = this.FamilyName?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (this.FullMiddleName?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (this.GivenName?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (this.Shortcut?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}