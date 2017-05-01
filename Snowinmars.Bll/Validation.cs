using Snowinmars.Entities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Snowinmars.Bll
{
	internal sealed class Validation
	{
		public static void Check(Author author)
		{
			Validation.Check(author.Id);

			Validation.CheckGivenName(author.GivenName);
			Validation.CheckFullMiddleName(author.FullMiddleName);
			Validation.CheckFamilyName(author.FamilyName);

			Validation.CheckShortcut(author.Shortcut);

			Validation.CheckPseudonym(author.Pseudonym);
		}

		private static void CheckPseudonym(Pseudonym pseudonym)
		{
			Validation.CheckGivenName(pseudonym.GivenName);
			Validation.CheckFullMiddleName(pseudonym.FullMiddleName);
			Validation.CheckFamilyName(pseudonym.FamilyName);
		}

		private static void CheckShortcut(string shortcut)
		{
			if (shortcut == null)
			{
				throw new ValidationException("Author's shortcut is null");
			}

			if (string.IsNullOrWhiteSpace(shortcut))
			{
				throw new ValidationException("Author's shortcut can't be empty");
			}

			if (shortcut.Length > Common.Constant.ShortcutLength)
			{
				throw new ValidationException("Author's shortcut can't be longer than " + Common.Constant.GivenNameLength);
			}
		}

		private static void CheckFamilyName(string familyName)
		{
			if (familyName == null)
			{
				throw new ValidationException("Author's family name is null");
			}

			if (familyName.Length > Common.Constant.FamilyNameLength)
			{
				throw new ValidationException("Author's family name can't be longer than " + Common.Constant.GivenNameLength);
			}
		}

		private static void CheckFullMiddleName(string fullMiddleName)
		{
			if (fullMiddleName == null)
			{
				throw new ValidationException("Author full middle name is null");
			}

			if (fullMiddleName.Length > Common.Constant.FullMiddleNameLength)
			{
				throw new ValidationException("Author's full middle name can't be longer than " + Common.Constant.GivenNameLength);
			}
		}

		private static void CheckGivenName(string givenName)
		{
			if (givenName == null)
			{
				throw new ValidationException("Author given name is null");
			}

			if (givenName.Length > Common.Constant.GivenNameLength)
			{
				throw new ValidationException("Author's given name can't be longer than " + Common.Constant.GivenNameLength);
			}
		}

		public static void Check(Book book)
		{
			Validation.Check(book.Id);

			Validation.CheckAdditionalInfo(book.AdditionalInfo);
			Validation.CheckAuthorIds (book.AuthorIds);
			Validation.CheckAuthorShortcuts (book.AuthorShortcuts);
			Validation.CheckBookshelf (book.Bookshelf);
			Validation.CheckUrl (book.FlibustaUrl);
			Validation.CheckUrl (book.LibRusEcUrl);
			Validation.CheckUrl (book.LiveLibUrl);
			Validation.CheckUsername(book.Owner);
			Validation.CheckPageCount(book.PageCount);
			Validation.CheckTitle(book.Title);
			Validation.CheckYear(book.Year);
		}

		private static void CheckYear(int year)
		{
			
		}

		private static void CheckTitle(string title)
		{
			if (title == null)
			{
				throw new ValidationException("Book's title can't be null");
			}

			if (title.Length > Common.Constant.TitleLength)
			{
				throw new ValidationException("Book's title is too long");
			}
		}

		private static void CheckPageCount(int pageCount)
		{
			if (pageCount < 0)
			{
				throw new ValidationException("Book's page count can't be less then zero");
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


		private static void CheckBookshelf(string bookshelf)
		{
			if (bookshelf == null)
			{
				throw new ValidationException("Book's bookshelf is null");
			}

			if (bookshelf.Length > Common.Constant.BookshelfLength)
			{
				throw new ValidationException("Book's bookshelf is to long");
			}
		}

		private static void CheckAuthorShortcuts(IList<string> authorShortcuts)
		{
			if (authorShortcuts == null)
			{
				throw new ValidationException("Book's author shortcut collection is null");
			}
		}

		private static void CheckAuthorIds(ICollection<Guid> authorIds)
		{
			if (authorIds == null)
			{
				throw new ValidationException("Book's author ids collection is null");
			}
		}

		private static void CheckAdditionalInfo(string additionalInfo)
		{
			if (additionalInfo == null)
			{
				throw new ValidationException("Book's additional info is null");
			}

			if (additionalInfo.Length > Common.Constant.AdditionalInfoLength)
			{
				throw new ValidationException("Book's additional info is too long");
			}
		}

		public static void Check(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ValidationException("Author's id can't be empty");
			}
		}

		public static void Check(User user)
		{
			Validation.Check(user.Id);

			Validation.CheckUsername(user.Username);
			Validation.CheckEmail(user.Email);
		}

		public static void CheckEmail(string email)
		{
			if (email == null)
			{
				throw new ValidationException("User's email is null");
			}

			if (!Regex.IsMatch(email, ".*\\@.*\\..*"))
			{
				throw new ValidationException("User's email have wrong format");
			}
		}

		#region top100Passwords



		private static string[] top100Passwords = new[]
		{
			"123456",
			"12345",
			"password",
			"DEFAULT",
			"123456789",
			"qwerty",
			"12345678",
			"abc123",
			"pussy",
			"1234567",
			"696969",
			"ashley",
			"fuckme",
			"football",
			"baseball",
			"fuckyou",
			"111111",
			"1234567890",
			"ashleymadison",
			"password1",
			"madison",
			"asshole",
			"superman",
			"mustang",
			"harley",
			"654321",
			"123123",
			"hello",
			"monkey",
			"000000",
			"hockey",
			"letmein",
			"11111",
			"soccer",
			"cheater",
			"kazuga",
			"hunter",
			"shadow",
			"michael",
			"121212",
			"666666",
			"iloveyou",
			"qwertyuiop",
			"secret",
			"buster",
			"horny",
			"jordan",
			"hosts",
			"zxcvbnm",
			"asdfghjkl",
			"affair",
			"dragon",
			"987654",
			"liverpool",
			"bigdick",
			"sunshine",
			"yankees",
			"asdfg",
			"freedom",
			"batman",
			"whatever",
			"charlie",
			"fuckoff",
			"money",
			"pepper",
			"jessica",
			"asdfasdf",
			"1qaz2wsx",
			"987654321",
			"andrew",
			"qazwsx",
			"dallas",
			"55555",
			"131313",
			"abcd1234",
			"anthony",
			"steelers",
			"asdfgh",
			"jennifer",
			"killer",
			"cowboys",
			"master",
			"jordan23",
			"robert",
			"maggie",
			"looking",
			"thomas",
			"george",
			"matthew",
			"7777777",
			"amanda",
			"summer",
			"qwert",
			"princess",
			"ranger",
			"william",
			"corvette",
			"jackson",
			"tigger",
			"computer",
		};
		#endregion

		public static void CheckPassword(string password)
		{
			if (password == null)
			{
				throw new ValidationException("User's password is null");
			}

			if (password.Length < 8 || 
				Validation.top100Passwords.Contains(password))
			{
				throw new UserIsMoronException("Password is in top 100");
			}
		}

		public static void CheckUsername(string username)
		{
			if (username == null)
			{
				throw new ValidationException("Username is null");
			}

			if (Regex.IsMatch(username, "[a-zA-Z]*"))
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

		public static void CheckSalt(string salt)
		{
			if (salt == null)
			{
				throw new ValidationException("Salt is null");
			}

			if (salt.Length < 16)
			{
				throw new ValidationException("You can't use this salt: it's too short");
			}
		}
	}
}