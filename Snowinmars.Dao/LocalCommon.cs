using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Entities;

namespace Snowinmars.Dao
{
	internal static class LocalCommon
	{
		internal static T ConvertFromDbValue<T>(object obj)
		{
			if (obj == null || obj == DBNull.Value)
			{
				return default(T);
			}
			else
			{
				return (T)obj;
			}
		}

		internal static object ConvertToDbValue<T>(T obj)
		{
			if (obj == null)
			{
				return DBNull.Value;
			}

			return obj;
		}

		internal static Author MapAuthor(IDataRecord reader)
		{
			string fullMiddleName = LocalCommon.ConvertFromDbValue<string>(reader[LocalConst.Author.Column.FullMiddleName])?.Trim() ?? string.Empty;
			string familyName = LocalCommon.ConvertFromDbValue<string>(reader[LocalConst.Author.Column.FamilyName])?.Trim() ?? string.Empty;
			string givenName = LocalCommon.ConvertFromDbValue<string>(reader[LocalConst.Author.Column.GivenName])?.Trim() ?? string.Empty;
			string shortcut = LocalCommon.ConvertFromDbValue<string>(reader[LocalConst.Author.Column.Shortcut])?.Trim() ?? string.Empty;
			Guid authorId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.Author.Column.Id]);

			var pseudonym = LocalCommon.MapPseudonym(reader);

			Author author = new Author(givenName)
			{
				Id = authorId,
				FullMiddleName = fullMiddleName,
				FamilyName = familyName,
				Shortcut = shortcut,
				Pseudonym = pseudonym,
			};

			return author;
		}

		internal static IEnumerable<Author> MapAuthors(IDataReader reader)
		{
			List<Author> authors = new List<Author>();

			while (reader.Read())
			{
				Author author = LocalCommon.MapAuthor(reader);

				authors.Add(author);
			}

			return authors;
		}

		internal static Pseudonym MapPseudonym(IDataRecord reader)
		{
			string pseudonymFullMiddleName = LocalCommon.ConvertFromDbValue<string>(reader[LocalConst.Author.Column.PseudonymFullMiddleName])?.Trim() ?? string.Empty;
			string pseudonymFamilyName = LocalCommon.ConvertFromDbValue<string>(reader[LocalConst.Author.Column.PseudonymFamilyName])?.Trim() ?? string.Empty;
			string pseudonymGivenName = LocalCommon.ConvertFromDbValue<string>(reader[LocalConst.Author.Column.PseudonymGivenName])?.Trim() ?? string.Empty;

			if (pseudonymGivenName != string.Empty)
			{
				return new Pseudonym()
				{
					GivenName = pseudonymGivenName,
					FullMiddleName = pseudonymFullMiddleName,
					FamilyName = pseudonymFamilyName,
				};
			}

			return Pseudonym.None;
		}

		internal static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> add)
		{
			foreach (var item in add)
			{
				collection.Add(item);
			}
		}
	}
}
