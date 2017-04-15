using System.Collections;
using System.Collections.Generic;

namespace Snowinmars.Common
{
	public static class Constant
	{
		public const string ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Snowinmars.DataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
	}

	public static class Extensions
	{
		public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> adders)
		{
			foreach (var adder in adders)
			{
				list.Add(adder);
			}
		}
	}
}