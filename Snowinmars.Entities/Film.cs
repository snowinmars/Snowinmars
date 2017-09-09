using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	public class Film: Entity
	{
		public Film(string name)
		{
			this.Name = name;

			this.AuthorIds = new List<Guid>();
		}

		public string Name { get; set; }
		public int Year { get; set; }
		public ICollection<Guid> AuthorIds { get; private set; }
	}
}
