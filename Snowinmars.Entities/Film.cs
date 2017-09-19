using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	public class Film: Entity
	{
		public Film(string title)
		{
			this.Title = title;

			this.AuthorIds = new List<Guid>();
			this.AuthorShortcuts = new List<string>();
		}

		public string Title { get; set; }
		public int Year { get; set; }
		public ICollection<Guid> AuthorIds { get; private set; }
		public ICollection<string> AuthorShortcuts { get; private set; }
		public string KinopoiskUrl { get; set; }
		public string Description { get; set; }
	}
}
