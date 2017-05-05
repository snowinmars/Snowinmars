using Snowinmars.Entities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Snowinmars.Bll.Tests.Authors
{
	public class Negative : BaseTest
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("  ")]
		[InlineData(" ")] // &emsp;
		public void Author_Create_MustWork(string shortcut)
		{
			Assert.Throws<ArgumentException>(() => new Author(shortcut));
		}

		[Fact]
		public void Author_Get_MustWork()
		{
			try
			{
				Assert.Throws<ArgumentException>(() => this.authorDao.Get(Guid.Empty));
				Assert.Throws<ObjectNotFoundException>(() => this.authorDao.Get(UnexistingAuthor));

				// expressions
				Assert.Throws<ArgumentNullException>(() => this.authorDao.Get(null));
			}
			catch
			{
				this.Dispose();
			}
		}

		[Fact]
		public void Author_Remove_MustWork()
		{
			try
			{
				Assert.Throws<ArgumentException>(() => this.authorDao.Remove(Guid.Empty));
				Assert.Throws<ObjectNotFoundException>(() => this.authorDao.Remove(UnexistingAuthor));
			}
			catch
			{
				this.Dispose();
			}
		}

		[Fact]
		public void Author_Create_IdValidation_MustWork()
		{
			try
			{
				var author = new Author(AnyWord) { Id = Guid.Empty };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		public static IEnumerable<object[]> GetWrongGivenNames
		{
			get
			{
				return new List<object[]>
				{
					new object[]
					{
						TextGenerator.GetNewWord(Common.Constant.GivenNameLength + 10, Common.Constant.GivenNameLength + 20),
					}
				};
			}
		}

		[Theory]
		[MemberData(nameof(GetWrongGivenNames))]
		public void Author_Create_GivenNameValidation_MustWork(string givenName)
		{
			try
			{
				var author = new Author(AnyWord) { GivenName = null };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});

				author = new Author(AnyWord) { GivenName = givenName };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		public static IEnumerable<object[]> GetWrongFullMiddleNames
		{
			get
			{
				return new List<object[]>
				{
					new object[]
					{
						TextGenerator.GetNewWord(Common.Constant.FullMiddleNameLength + 10, Common.Constant.FullMiddleNameLength + 20),
					}
				};
			}
		}

		[Theory]
		[MemberData(nameof(GetWrongFullMiddleNames))]
		public void Author_Create_FullMiddleNameValidation_MustWork(string fullMiddleName)
		{
			try
			{
				var author = new Author(AnyWord) { FullMiddleName = null };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});

				author = new Author(AnyWord) { FullMiddleName = fullMiddleName };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		public static IEnumerable<object[]> GetWrongFamilyNames
		{
			get
			{
				return new List<object[]>
				{
					new object[]
					{
						TextGenerator.GetNewWord(Common.Constant.FamilyNameLength + 10, Common.Constant.FamilyNameLength + 20),
					}
				};
			}
		}

		[Theory]
		[MemberData(nameof(GetWrongFamilyNames))]
		public void Author_Create_FamilyNameValidation_MustWork(string familyName)
		{
			try
			{
				var author = new Author(AnyWord) { FamilyName = null };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});

				author = new Author(AnyWord) { FamilyName = familyName };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		public static IEnumerable<object[]> GetWrongShortcuts
		{
			get
			{
				return new List<object[]>
				{
					new object[]
					{
						"",
					" ",
					" ", // &emsp;
					TextGenerator.GetNewWord(Common.Constant.ShortcutLength + 10, Common.Constant.ShortcutLength + 20),
					}
				};
			}
		}

		[Theory]
		[MemberData(nameof(GetWrongShortcuts))]
		public void Author_Create_ShortcutValidation_MustWork(string shortcut)
		{
			try
			{
				var author = new Author(null);
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});

				author = new Author(shortcut);
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		[Fact]
		public void Author_Create_PseudonymValidation_MustWork()
		{
			try
			{
				var author = new Author(AnyWord) { Pseudonym = null };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		[Theory]
		[MemberData(nameof(GetWrongGivenNames))]
		public void Author_Create_PseudonymGivenNameValidation_MustWork(string givenName)
		{
			try
			{
				var author = new Author(AnyWord) { Pseudonym = new Pseudonym { GivenName = null } };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});

				author = new Author(AnyWord) { Pseudonym = new Pseudonym { GivenName = givenName } };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		[Theory]
		[MemberData(nameof(GetWrongFullMiddleNames))]
		public void Author_Create_PseudonymFullMiddleNameValidation_MustWork(string fullMiddleName)
		{
			try
			{
				var author = new Author(AnyWord) { Pseudonym = new Pseudonym { FullMiddleName = null } };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});

				author = new Author(AnyWord) { Pseudonym = new Pseudonym { FullMiddleName = fullMiddleName } };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}

		[Theory]
		[MemberData(nameof(GetWrongFamilyNames))]
		public void Author_Create_PseudonymFamilyNameValidation_MustWork(string familyName)
		{
			try
			{
				var author = new Author(AnyWord) { Pseudonym = new Pseudonym { FamilyName = null } };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});

				author = new Author(AnyWord) { Pseudonym = new Pseudonym { FamilyName = familyName } };
				Assert.Throws<ValidationException>(() =>
				{
					this.authorDao.Create(author);
				});
			}
			finally
			{
				this.Dispose();
			}
		}
	}
}