using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Snowinmars.Entities
{
	public class Daemon : IDaemon
	{
		public Daemon(string name, DaemonSettingsType type)
		{
			this.Name = name;
			this.isReady = false;

			this.Id = Guid.NewGuid();

			Settings = new DaemonSettings(type);
		}

		private bool isReady;
		[JsonIgnore]
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

		public DaemonSettings Settings { get; set; }
	}
}
