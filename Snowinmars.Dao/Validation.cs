using System;
using System.Activities;
using System.Data;
using System.Runtime.CompilerServices;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;

namespace Snowinmars.Dao
{
	internal sealed class Validation
	{
		public static void Check(Author author, bool mustbeUnique = true)
		{
			Check(author.Id);
			Check(author.Pseudonym);

			if (mustbeUnique)
			{
				try
				{
					AuthorDao.Get(author.Id);
				}
				catch (ObjectNotFoundException)
				{
					return;
				}

				throw new ValidationException($"Author with id {author.Id} already exists");
			}
		}

		private static void Check(Pseudonym pseudonym)
		{
			if (pseudonym == null)
			{
				throw new ValidationException("Pseudonym can't be null");
			}
		}

		public static void Check(Book book, bool mustbeUnique = true)
		{
			Check(book.Id);

			foreach (var authorId in book.AuthorIds)
			{
				var author = AuthorDao.Get(authorId);
				Check(author);
			}

			try
			{
				BookDao.Get(book.Id);
			}
			catch (ObjectNotFoundException)
			{
				throw new ValidationException($"Book with id {book.Id} already exists");
			}
		}

		public static void Check(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ValidationException("Author's id can't be empty");
			}
		}

		public static void Set(IBookDao bookDao)
		{
			BookDao = bookDao;
		}

		private static IBookDao BookDao { get; set; }

		public static void Set(IAuthorDao authorDao)
		{
			AuthorDao = authorDao;
		}

		private static IAuthorDao AuthorDao { get; set; }
	}
}