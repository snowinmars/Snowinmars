using Snowinmars.Common;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Snowinmars.Dao
{
    internal static class LocalCommon
    {
        public static User MapUser(SqlDataReader reader)
        {
	        if (!reader.Read())
	        {
		        throw new ObjectNotFoundException();
	        }

            Guid id = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.User.Column.Id]);
            UserRoles roles = LocalCommon.ConvertFromDbValue<UserRoles>(reader[LocalConst.User.Column.Roles]);
            Language language = LocalCommon.ConvertFromDbValue<Language>(reader[LocalConst.User.Column.LanguageCode]);
            string salt = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.User.Column.Salt]);
            string email = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.User.Column.Email]);
            string username = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.User.Column.Username]);
            string passwordHash = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.User.Column.PasswordHash]);

            User user = new User(username)
            {
                Id = id,
                Salt = salt,
                Email = email,
                Roles = roles,
                Language = language,
                PasswordHash = passwordHash,
            };

            return user;
        }

        internal static T ConvertFromDbValue<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }

            return (T)obj;
        }

        internal static string ConvertFromDbValueToString(object obj)
        {
            return LocalCommon.ConvertFromDbValue<string>(obj)?.Trim() ?? string.Empty;
        }

        internal static object ConvertToDbValue<T>(T obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }

            return obj;
        }

        internal static (Author author, Guid forBookId) MapAuthor(SqlDataReader reader)
        {
	        Guid bookId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.BookAuthor.Column.BookId]);
            Guid authorId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.Author.Column.Id]);
            string shortcut = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.Shortcut]);
            string givenName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.GivenName]);
            string familyName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.FamilyName]);
            string fullMiddleName = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Author.Column.FullMiddleName]);
            bool isSynchronized = LocalCommon.ConvertFromDbValue<bool>(reader[LocalConst.Author.Column.IsSynchronized]);

            var pseudonym = LocalCommon.MapPseudonym(reader);

            Author author = new Author(shortcut)
            {
                Id = authorId,
                GivenName = givenName,
                Pseudonym = pseudonym,
                FamilyName = familyName,
                FullMiddleName = fullMiddleName,
                IsSynchronized = isSynchronized,
            };

            return (author: author, forBookId: bookId);
        }

        internal static IEnumerable<(Author author, Guid forBookId)> MapAuthors(SqlDataReader reader)
        {
            var authors = new List<(Author author, Guid forBookId)>();

            while (reader.Read())
            {
                var authorPair = LocalCommon.MapAuthor(reader);

                authors.Add(authorPair);
            }

            return authors;
        }

	    internal static Book MapBook(SqlDataReader reader) => LocalCommon.MapBooks(reader).First();

        internal static IEnumerable<Book> MapBooks(SqlDataReader reader)
        {
			// I don't want to let anyone see or reuse the MapBook method due to this method returns an uncomplete book: the result doesn't have authors
			Book MapBookPartial()
	        {
				int year = LocalCommon.ConvertFromDbValue<int>(reader[LocalConst.Book.Column.Year]);
		        Guid bookId = LocalCommon.ConvertFromDbValue<Guid>(reader[LocalConst.Book.Column.Id]);
		        string owner = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.Owner]);
		        string title = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.Title]);
		        int pageCount = LocalCommon.ConvertFromDbValue<int>(reader[LocalConst.Book.Column.PageCount]);
		        string bookshelf = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.Bookshelf]);
		        BookStatus status = LocalCommon.ConvertFromDbValue<BookStatus>(reader[LocalConst.Book.Column.Status]);
		        string liveLibUrl = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.LiveLibUrl]);
		        string libRusEcUrl = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.LibRusEcUrl]);
		        string flibustaUrl = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.FlibustaUrl]);
		        bool isSynchronized = LocalCommon.ConvertFromDbValue<bool>(reader[LocalConst.Book.Column.IsSynchronized]);
		        string additionalInfo = LocalCommon.ConvertFromDbValueToString(reader[LocalConst.Book.Column.AdditionalInfo]);

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
		        };

		        return book;
	        }

			List<Book> books = new List<Book>();
	        bool wasBookRead = false;

            while (reader.Read())
            {
                Book book = MapBookPartial();

                books.Add(book);

	            wasBookRead = true;
            }

	        if (!wasBookRead)
	        {
		        throw new ObjectNotFoundException();
	        }

	        reader.NextResult();

			IList<(Author author, Guid forBookId)> authorsPairs = new List<(Author author, Guid forBookId)>();
	        
			while (reader.Read())
	        {
		        var authorPair = LocalCommon.MapAuthor(reader);

				authorsPairs.Add(authorPair);
	        }

	        LocalCommon.Merge(books, authorsPairs);

            return books;
        }

	    private static void Merge(IEnumerable<Book> books, IList<(Author author, Guid forBookId)> authors)
	    {
		    foreach (var book in books)
		    {
			    book.Authors.AddRange(authors.Where(p => p.forBookId == book.Id).Select(p => p.author));
		    }
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
    }
}