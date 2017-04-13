using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	public abstract class Entity
	{
		protected Entity()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }
	}
}
