using Snowinmars.Entities;
using System;
using System.Collections.Generic;

namespace Snowinmars.Bll
{
	internal sealed class Validation
	{
		public static void Check(Author author)
		{
		}

		public static void Check(Book book)
		{
		}

		public static void Check(Guid id)
		{
		}

		public static void Check(User author)
		{
			
		}

		public static void Check(string str)
		{
		}

		public static void ChackAll(IEnumerable<string> enumerable)
		{
			foreach (var item in enumerable)
			{
				Check(item);
			}
		}

		public static void CheckPassword(string password)
		{
			throw new NotImplementedException();
		}

		public static void CheckUsername(string username)
		{
			throw new NotImplementedException();
		}
	}
}