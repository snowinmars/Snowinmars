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
			Minutes = new List<int>();
			Hours = new List<int>();

			Type = type;

			TimeZoneInfo = TimeZoneInfo.Utc;
		}
		public DaemonSettingsType Type { get; set; }
		public TimeZoneInfo TimeZoneInfo { get; set; }
		public IList<int> Minutes { get; }
		public IList<int> Hours { get; }
	}
}
