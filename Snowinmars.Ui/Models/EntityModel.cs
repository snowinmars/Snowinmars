using System;

namespace Snowinmars.Ui.Models
{
	public abstract class EntityModel
	{
		public Guid Id { get; set; }
	    public bool IsSynchronized { get; set; }
	}
}