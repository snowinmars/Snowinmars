using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	public class DaemonSettings
	{
		public DaemonSettings(DaemonSettingsType type)
		{
			Type = type;

			Minutes = new List<int>();
			Hours = new List<int>();

			TimeZoneInfo = TimeZoneInfo.Utc;
		}

		public DaemonSettingsType Type { get; set; }
		public IList<int> Minutes { get; }
		public IList<int> Hours { get; }
		public TimeZoneInfo TimeZoneInfo { get; set; }
	}
}
