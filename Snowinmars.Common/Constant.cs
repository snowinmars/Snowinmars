using System.Collections.Generic;
using System.Configuration;

namespace Snowinmars.Common
{
	public static class Constant
	{
		public const string ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Snowinmars.DataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		public readonly static string SiteUrl = ConfigurationManager.AppSettings["siteUrl"];
		public const int GivenNameLength = 100;
		public const int FullMiddleNameLength = 100;
		public const int FamilyNameLength = 100;
		public const int ShortcutLength = 100;
		public const int MinUsernameLength = 3;
		public const int MaxUsernameLength = 50;
		public const int TitleLength = 200;
		public const int BookshelfLength = 50;
		public const int AdditionalInfoLength = 1000;
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