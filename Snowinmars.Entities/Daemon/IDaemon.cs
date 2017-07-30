using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	public interface IDaemon
	{
		Guid Id { get; }
		string Name { get; }

		void Init();
		void Work();
		void Finit();

		DaemonSettings Settings { get; }
	}
}
