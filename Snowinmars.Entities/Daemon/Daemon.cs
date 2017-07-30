using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	public class Daemon : IDaemon
	{
		public Daemon(string name)
		{
			this.Name = name;
			this.isReady = false;

			this.Id = Guid.NewGuid();
		}

		private bool isReady;

		public Guid Id { get; }
		public string Name { get; }

		public void Init()
		{
			this.isReady = true;
		}

		public void Work()
		{
			throw new NotImplementedException();
		}

		public void Finit()
		{
			this.isReady = false;
		}

		public DaemonSettings Settings { get; protected set; }
	}
}
