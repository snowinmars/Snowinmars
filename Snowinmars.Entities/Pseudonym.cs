using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
