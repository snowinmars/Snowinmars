﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Common;
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
				return (T) obj;
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
			string fullMiddleName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.FullMiddleName]);
			string familyName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.FamilyName]);
			string givenName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.GivenName]);
			string shortcut = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.Shortcut]);
			Guid authorId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.Author.Column.Id]);

			var pseudonym = LocalCommon.MapPseudonym(reader);

			Author author = new Author(shortcut)
			{
				Id = authorId,
				GivenName = givenName,
				FullMiddleName = fullMiddleName,
				FamilyName = familyName,
				Pseudonym = pseudonym,
			};

			return author;
		}

		internal static Book MapBook(IDataRecord reader)
		{
			Guid bookId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.Book.Column.Id]);
			string title = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.Title]);
			int year = LocalCommon.ConvertFromDbValue<int>(reader[LocalConst.Book.Column.Year]);
			int pageCount = LocalCommon.ConvertFromDbValue<int>(reader[LocalConst.Book.Column.PageCount]);

			string authorShortcuts = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.AuthorsShortcuts]);

			var book = new Book(title, pageCount)
			{
				Id = bookId,
				Year = year,
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

	}
}
