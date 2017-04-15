using System;

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