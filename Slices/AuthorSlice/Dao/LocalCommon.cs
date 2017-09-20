using System;
using System.Collections.Generic;
using System.Data;
using Snowinmars.AuthorSlice.AuthorEntities;
using Snowinmars.Common;

namespace Snowinmars.AuthorSlice.AuthorDao
{
	internal static class LocalCommon
	{
		internal static Author MapAuthor(IDataRecord reader)
		{
			Guid authorId = reader[LocalConst.Author.Column.Id].ConvertFromDbValue<Guid>();
			bool isSynchronized = reader[LocalConst.Author.Column.IsSynchronized].ConvertFromDbValue<bool>();
			bool mustInformAboutWarnings = reader[LocalConst.Author.Column.MustInformAboutWarnings].ConvertFromDbValue<bool>();
			string shortcut = reader[LocalConst.Author.Column.Shortcut].ConvertFromDbValueToString();
			Name name = LocalCommon.MapName(reader);

			Name pseudonym = LocalCommon.MapPseudonym(reader);
			

			Author author = new Author(shortcut)
			{
				Id = authorId,
				Name = name,
				Pseudonym = pseudonym,
				IsSynchronized = isSynchronized,
				MustInformAboutWarnings = mustInformAboutWarnings,
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
		internal static Name MapName(IDataRecord reader)
		{
			string givenName = reader[LocalConst.Author.Column.GivenName].ConvertFromDbValueToString();
			string familyName = reader[LocalConst.Author.Column.FamilyName].ConvertFromDbValueToString();
			string fullMiddleName = reader[LocalConst.Author.Column.FullMiddleName].ConvertFromDbValueToString();

			return new Name()
			{
				GivenName = givenName,
				FullMiddleName = fullMiddleName,
				FamilyName = familyName,
			};
		}

		internal static Name MapPseudonym(IDataRecord reader)
		{
			string pseudonymFullMiddleName = reader[LocalConst.Author.Column.PseudonymFullMiddleName].ConvertFromDbValueToString();
			string pseudonymFamilyName = reader[LocalConst.Author.Column.PseudonymFamilyName].ConvertFromDbValueToString();
			string pseudonymGivenName = reader[LocalConst.Author.Column.PseudonymGivenName].ConvertFromDbValueToString();

			return new Name()
			{
				GivenName = pseudonymGivenName,
				FullMiddleName = pseudonymFullMiddleName,
				FamilyName = pseudonymFamilyName,
			};
		}
	}
}
