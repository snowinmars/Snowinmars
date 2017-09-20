using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Common
{
	public static class SystemExtensions
	{
		public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> adders)
		{
			foreach (var adder in adders)
			{
				list.Add(adder);
			}
		}

		public static T ConvertFromDbValue<T>(this object obj)
		{
			if (obj == null || obj == DBNull.Value)
			{
				return default(T);
			}

			return (T)obj;
		}

		public static string ConvertFromDbValueToString(this object obj)
		{
			return ConvertFromDbValue<string>(obj)?.Trim() ?? string.Empty;
		}

		public static object ConvertToDbValue<T>(this T obj)
		{
			if (obj == null)
			{
				return DBNull.Value;
			}

			return obj;
		}
	}
}
