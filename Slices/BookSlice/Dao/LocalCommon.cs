using System;
using System.Collections.Generic;
using System.Data;
using Snowinmars.BookSlice.BookEntities;
using Snowinmars.Common;

namespace Snowinmars.BookSlice.BookDao
{
	internal static class LocalCommon
	{
		internal static Book MapBook(IDataRecord reader)
		{
			string bookshelf = reader[LocalConst.Book.Column.Bookshelf].ConvertFromDbValueToString();
			string liveLibUrl = reader[LocalConst.Book.Column.LiveLibUrl].ConvertFromDbValueToString();
			string libRusEcUrl = reader[LocalConst.Book.Column.LibRusEcUrl].ConvertFromDbValueToString();
			string flibustaUrl = reader[LocalConst.Book.Column.FlibustaUrl].ConvertFromDbValueToString();
			string additionalInfo = reader[LocalConst.Book.Column.AdditionalInfo].ConvertFromDbValueToString();
			string authorShortcuts = reader[LocalConst.Book.Column.AuthorsShortcuts].ConvertFromDbValueToString();
			string owner = reader[LocalConst.Book.Column.Owner].ConvertFromDbValueToString();
			string title = reader[LocalConst.Book.Column.Title].ConvertFromDbValueToString();
			int year = reader[LocalConst.Book.Column.Year].ConvertFromDbValue<int>();
			Guid bookId = reader[LocalConst.Book.Column.Id].ConvertFromDbValue<Guid>();
			BookStatus status = reader[LocalConst.Book.Column.Status].ConvertFromDbValue<BookStatus>();
			int pageCount = reader[LocalConst.Book.Column.PageCount].ConvertFromDbValue<int>();
			bool isSynchronized = reader[LocalConst.Book.Column.IsSynchronized].ConvertFromDbValue<bool>();
			bool mustInformAboutWarnings = reader[LocalConst.Book.Column.MustInformAboutWarnings].ConvertFromDbValue<bool>();

			var book = new Book(title, pageCount)
			{
				Id = bookId,
				Year = year,
				Owner = owner,
				Status = status,
				Bookshelf = bookshelf,
				LiveLibUrl = liveLibUrl,
				LibRusEcUrl = libRusEcUrl,
				FlibustaUrl = flibustaUrl,
				AdditionalInfo = additionalInfo,
				IsSynchronized = isSynchronized,
				MustInformAboutWarnings = mustInformAboutWarnings,
			};

			book.AuthorShortcuts.AddRange(authorShortcuts.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

			return book;
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
	}
}
