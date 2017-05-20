using Snowinmars.Common;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

		internal static string ConvertFromDbValueToString(object obj)
		{
			return LocalCommon.ConvertFromDbValue<string>(obj)?.Trim() ?? string.Empty;
		}

		internal static Author MapAuthor(IDataRecord reader)
		{
			bool mustInformAboutWarnings = LocalCommon.ConvertFromDbValue<bool>(reader[LocalConst.Book.Column.MustInformAboutWarnings]);
			string fullMiddleName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.FullMiddleName]);
			string familyName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.FamilyName]);
			string givenName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.GivenName]);
			string shortcut = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.Shortcut]);
			Guid authorId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.Author.Column.Id]);
		    bool isSynchronized = LocalCommon.ConvertFromDbValue<bool>(reader[LocalConst.Author.Column.IsSynchronized]);

			var pseudonym = LocalCommon.MapPseudonym(reader);

			Author author = new Author(shortcut)
			{
				Id = authorId,
				GivenName = givenName,
				FullMiddleName = fullMiddleName,
				FamilyName = familyName,
				Pseudonym = pseudonym,
				MustInformAboutWarnings = mustInformAboutWarnings,
                IsSynchronized = isSynchronized,
            };

			return author;
		}

		internal static Book MapBook(IDataRecord reader)
		{
			int year = LocalCommon.ConvertFromDbValue<int>(reader[LocalConst.Book.Column.Year]);
			Guid bookId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.Book.Column.Id]);
			string owner = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.Owner]);
			string title = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.Title]);
			int pageCount = LocalCommon.ConvertFromDbValue<int>(reader[LocalConst.Book.Column.PageCount]);
			string bookshelf = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.Bookshelf]);
			string liveLibUrl = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.LiveLibUrl]);
			string libRusEcUrl = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.LibRusEcUrl]);
			string flibustaUrl = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.FlibustaUrl]);
            bool isSynchronized = LocalCommon.ConvertFromDbValue<bool>(reader[LocalConst.Book.Column.IsSynchronized]);
			string additionalInfo = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.AdditionalInfo]);
			string authorShortcuts = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.AuthorsShortcuts]);
			bool mustInformAboutWarnings = LocalCommon.ConvertFromDbValue<bool>(reader[LocalConst.Book.Column.MustInformAboutWarnings]);

            var book = new Book(title, pageCount)
			{
				Id = bookId,
				Year = year,
				MustInformAboutWarnings = mustInformAboutWarnings,
				Bookshelf = bookshelf,
				AdditionalInfo = additionalInfo,
				LiveLibUrl = liveLibUrl,
				LibRusEcUrl = libRusEcUrl,
				FlibustaUrl = flibustaUrl,
				Owner = owner,
                IsSynchronized = isSynchronized,
            };

			book.AuthorShortcuts.AddRange(authorShortcuts.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

			return book;
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

		internal static IEnumerable<Book> MapBooks(IDataReader reader)
		{
			List<Book> books = new List<Book>();

			while (reader.Read())
			{
				Book book = LocalCommon.MapBook(reader);

				books.Add(book);
			}

			return books;
		}

		internal static Pseudonym MapPseudonym(IDataRecord reader)
		{
			string pseudonymFullMiddleName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.PseudonymFullMiddleName]);
			string pseudonymFamilyName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.PseudonymFamilyName]);
			string pseudonymGivenName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.PseudonymGivenName]);

			return new Pseudonym()
			{
				GivenName = pseudonymGivenName,
				FullMiddleName = pseudonymFullMiddleName,
				FamilyName = pseudonymFamilyName,
			};
		}

		public static User MapUser(IDataRecord reader)
		{
			Guid id = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.User.Column.Id]);
			string username = ConvertFromDbValueToString(reader[LocalConst.User.Column.Username]);
			string passwordHash = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.User.Column.PasswordHash]);
			UserRoles roles = ConvertFromDbValue<UserRoles>(reader[LocalConst.User.Column.Roles]);
			string email = ConvertFromDbValueToString(reader[LocalConst.User.Column.Email]);
			string salt = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.User.Column.Salt]);
		    Language language = LocalCommon.ConvertFromDbValue<Language>(reader[LocalConst.User.Column.LanguageCode]);

			User user = new User(username)
			{
				Id = id,
				PasswordHash = passwordHash,
				Roles = roles,
				Email = email,
				Salt = salt,
                Language = language,
			};

			return user;
		}
	}
}