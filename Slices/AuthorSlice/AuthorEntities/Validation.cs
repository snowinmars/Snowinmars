using System;
using System.ComponentModel.DataAnnotations;
using Snowinmars.Common;

namespace Snowinmars.AuthorSlice.AuthorEntities
{
	public sealed class Validation
	{
		public static void BllCheck(Author author)
		{
			Validation.Check(author.Id);

			Validation.CheckName(author.Name);
			Validation.CheckName(author.Pseudonym);

			Validation.CheckShortcut(author.Shortcut);
		}

		public static void DaoCheck(Author author)
		{
			Validation.Check(author.Id);
			Validation.CheckName(author.Pseudonym);
		}

		public static void Check(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ValidationException("Author's id can't be empty");
			}
		}

		private static void CheckFamilyName(string familyName)
		{
			if (familyName == null)
			{
				throw new ValidationException("Author's family name is null");
			}

			if (familyName.Length > Constant.MaxFamilyNameLength)
			{
				throw new ValidationException("Author's family name can't be longer than " + Constant.MaxGivenNameLength);
			}
		}

		private static void CheckFullMiddleName(string fullMiddleName)
		{
			if (fullMiddleName == null)
			{
				throw new ValidationException("Author full middle name is null");
			}

			if (fullMiddleName.Length > Constant.MaxFullMiddleNameLength)
			{
				throw new ValidationException("Author's full middle name can't be longer than " + Constant.MaxGivenNameLength);
			}
		}

		private static void CheckGivenName(string givenName)
		{
			if (givenName == null)
			{
				throw new ValidationException("Author given name is null");
			}

			if (givenName.Length > Constant.MaxGivenNameLength)
			{
				throw new ValidationException("Author's given name can't be longer than " + Constant.MaxGivenNameLength);
			}
		}

		private static void CheckName(Name name)
		{
			Validation.CheckGivenName(name.GivenName);
			Validation.CheckFullMiddleName(name.FullMiddleName);
			Validation.CheckFamilyName(name.FamilyName);
		}

		private static void CheckShortcut(string shortcut)
		{
			if (string.IsNullOrWhiteSpace(shortcut))
			{
				throw new ValidationException("Author's shortcut can't be empty");
			}

			if (shortcut.Length > Constant.MaxShortcutLength)
			{
				throw new ValidationException("Author's shortcut can't be longer than " + Constant.MaxGivenNameLength);
			}
		}
	}
}
