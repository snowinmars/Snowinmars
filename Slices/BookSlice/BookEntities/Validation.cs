using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Snowinmars.BookSlice.BookEntities
{
	public sealed class Validation
	{
		public static void DaoCheck(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ValidationException("Author's id can't be empty");
			}
		}

		public static void Check(Book book)
		{
			Validation.Check(book.Id);

			Validation.CheckAdditionalInfo(book.AdditionalInfo);
			Validation.CheckAuthorIds(book.AuthorIds);
			Validation.CheckAuthorShortcuts(book.AuthorShortcuts);
			Validation.CheckBookshelf(book.Bookshelf);
			Validation.CheckUrl(book.FlibustaUrl);
			Validation.CheckUrl(book.LibRusEcUrl);
			Validation.CheckUrl(book.LiveLibUrl);
			Validation.CheckOwnerName(book.Owner);
			Validation.CheckPageCount(book.PageCount);
			Validation.CheckTitle(book.Title);
			Validation.CheckYear(book.Year);
		}
		public static void CheckOwnerName(string username)
		{
			if (username == null)
			{
				throw new ValidationException("Username is null");
			}

			if (!Regex.IsMatch(username, "[a-zA-Z]*"))
			{
				throw new ValidationException("Username can contains only english letters");
			}

			if (username.Length < Common.Constant.MinUsernameLength)
			{
				throw new ValidationException("Username is too short");
			}

			if (username.Length > Common.Constant.MaxUsernameLength)
			{
				throw new ValidationException("Username is too long");
			}
		}
		public static void Check(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ValidationException("Author's id can't be empty");
			}
		}

		private static void CheckAdditionalInfo(string additionalInfo)
		{
			if (additionalInfo == null)
			{
				throw new ValidationException("Book's additional info is null");
			}

			if (additionalInfo.Length > Common.Constant.MaxAdditionalInfoLength)
			{
				throw new ValidationException("Book's additional info is too long");
			}
		}

		private static void CheckAuthorIds(ICollection<Guid> authorIds)
		{
			if (authorIds == null)
			{
				throw new ValidationException("Book's author ids collection is null");
			}
		}

		private static void CheckAuthorShortcuts(IList<string> authorShortcuts)
		{
			if (authorShortcuts == null)
			{
				throw new ValidationException("Book's author shortcut collection is null");
			}
		}

		private static void CheckBookshelf(string bookshelf)
		{
			if (bookshelf == null)
			{
				throw new ValidationException("Book's bookshelf is null");
			}

			if (bookshelf.Length > Common.Constant.MaxBookshelfLength)
			{
				throw new ValidationException("Book's bookshelf is to long");
			}
		}


		private static void CheckPageCount(int pageCount)
		{
			if (pageCount < 0)
			{
				throw new ValidationException("Book's page count can't be less then zero");
			}
		}


		private static void CheckTitle(string title)
		{
			if (title == null)
			{
				throw new ValidationException("Book's title can't be null");
			}

			if (title.Length > Common.Constant.MaxTitleLength)
			{
				throw new ValidationException("Book's title is too long");
			}
		}

		private static void CheckUrl(string url)
		{
			if (url == null)
			{
				throw new ValidationException("Book's url is null");
			}

			if (Regex.IsMatch(url, @"/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/"))
			{
				throw new ValidationException("Url is in wrong format");
			}
		}

		private static void CheckYear(int year)
		{
		}
	}
}
