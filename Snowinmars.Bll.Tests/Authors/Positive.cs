using Snowinmars.Entities;
using System;
using System.Linq;
using Xunit;

namespace Snowinmars.Bll.Tests.Authors
{
	public class Positive : BaseTest
	{
		[Theory, CombinatorialData]
		public void Author_Create_MustWork(
			[CombinatorialValues("", "asd")]string givenName,
			[CombinatorialValues("", "asd")]string fullMiddleName,
			[CombinatorialValues("", "asd")]string familyName,
			[CombinatorialValues("asd")]string shortcut,
			[CombinatorialValues("", "asd")]string pseudonymGivenName,
			[CombinatorialValues("", "asd")]string pseudonymFullMiddleName,
			[CombinatorialValues("", "asd")]string pseudonymFamilyName,
			bool mustInformAboutWarnings)
		{
			try
			{
				int authorsCount = this.authorDao.Get(a => true).Count();

				Author author = CreateAuthor(UnexistingAuthor,
					givenName,
					fullMiddleName,
					familyName,
					shortcut,
					pseudonymGivenName,
					pseudonymFullMiddleName,
					pseudonymFamilyName,
					mustInformAboutWarnings);

				int newAuthorsCount = this.authorDao.Get(a => true).Count();

				Assert.True(newAuthorsCount == authorsCount + 1);

				Author createdAuthor = this.authorDao.Get(author.Id);

				Assert.True(!string.IsNullOrWhiteSpace(author.Shortcut));
				Assert.NotEqual(Guid.Empty, createdAuthor.Id);
				Assert.NotNull(createdAuthor.Pseudonym);

				Assert.Equal(givenName, createdAuthor.GivenName);
				Assert.Equal(fullMiddleName, createdAuthor.FullMiddleName);
				Assert.Equal(familyName, createdAuthor.FamilyName);
				Assert.Equal(shortcut, createdAuthor.Shortcut);
				Assert.Equal(pseudonymGivenName, createdAuthor.Pseudonym.GivenName);
				Assert.Equal(pseudonymFullMiddleName, createdAuthor.Pseudonym.FullMiddleName);
				Assert.Equal(pseudonymFamilyName, createdAuthor.Pseudonym.FamilyName);
				Assert.Equal(mustInformAboutWarnings, createdAuthor.MustInformAboutWarnings);
			}
			finally
			{
				this.Dispose();
			}
		}

		[Theory, CombinatorialData]
		public void Author_Ctor_MustWork(
			[CombinatorialValues("", "asd")]string givenName,
			[CombinatorialValues("", "asd")]string fullMiddleName,
			[CombinatorialValues("", "asd")]string familyName,
			[CombinatorialValues("asd")]string shortcut,
			[CombinatorialValues("", "asd")]string pseudonymGivenName,
			[CombinatorialValues("", "asd")]string pseudonymFullMiddleName,
			[CombinatorialValues("", "asd")]string pseudonymFamilyName,
			bool mustInformAboutWarnings)
		{
			Author author = new Author(shortcut)
			{
				GivenName = givenName,
				FullMiddleName = fullMiddleName,
				FamilyName = familyName,
				Pseudonym = new Pseudonym
				{
					GivenName = pseudonymGivenName,
					FullMiddleName = pseudonymFullMiddleName,
					FamilyName = pseudonymFamilyName,
				},
				MustInformAboutWarnings = mustInformAboutWarnings,
			};

			Assert.True(!string.IsNullOrWhiteSpace(author.Shortcut));
			Assert.NotEqual(Guid.Empty, author.Id);
			Assert.NotNull(author.Pseudonym);

			Assert.Equal(givenName, author.GivenName);
			Assert.Equal(fullMiddleName, author.FullMiddleName);
			Assert.Equal(familyName, author.FamilyName);
			Assert.Equal(shortcut, author.Shortcut);
			Assert.Equal(pseudonymGivenName, author.Pseudonym.GivenName);
			Assert.Equal(pseudonymFullMiddleName, author.Pseudonym.FullMiddleName);
			Assert.Equal(pseudonymFamilyName, author.Pseudonym.FamilyName);
			Assert.Equal(mustInformAboutWarnings, author.MustInformAboutWarnings);
		}

		[Fact]
		public void Author_DefalutCtor_MustWork()
		{
			Author author = new Author("asd");

			Assert.True(!string.IsNullOrWhiteSpace(author.Shortcut));
			Assert.NotEqual(Guid.Empty, author.Id);
			Assert.NotNull(author.Pseudonym);

			Assert.True(author.MustInformAboutWarnings);
			Assert.NotNull(author.GivenName);
			Assert.NotNull(author.FullMiddleName);
			Assert.NotNull(author.FamilyName);
			Assert.NotNull(author.Shortcut);
			Assert.NotNull(author.Pseudonym.GivenName);
			Assert.NotNull(author.Pseudonym.FullMiddleName);
			Assert.NotNull(author.Pseudonym.FamilyName);
		}

		[Fact]
		public void Author_Get_MustWork()
		{
			try
			{
				var author = this.CreateAnyAuthor();

				int authorsCount = this.authorDao.Get(a => true).Count();

				var createdAuthor = this.authorDao.Get(author.Id);
				int newAuthorsCount = this.authorDao.Get(a => true).Count();

				Assert.Equal(authorsCount, newAuthorsCount);

				Assert.NotNull(createdAuthor);
				Assert.True(!string.IsNullOrWhiteSpace(author.Shortcut));
				Assert.NotEqual(Guid.Empty, createdAuthor.Id);
				Assert.NotNull(createdAuthor.Pseudonym);
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
				var author = this.CreateAnyAuthor();
				int authorsCount = this.authorDao.Get(a => true).Count();

				this.authorDao.Remove(author.Id);
				int newAuthorsCount = this.authorDao.Get(a => true).Count();

				Assert.True(newAuthorsCount == authorsCount - 1);
			}
			catch
			{
				this.Dispose();
			}
		}

		[Theory, CombinatorialData]
		public void Author_Update_MustWork(
			[CombinatorialValues("", "asd")]string givenName,
			[CombinatorialValues("", "asd")]string fullMiddleName,
			[CombinatorialValues("", "asd")]string familyName,
			[CombinatorialValues("asd")]string shortcut,
			[CombinatorialValues("", "asd")]string pseudonymGivenName,
			[CombinatorialValues("", "asd")]string pseudonymFullMiddleName,
			[CombinatorialValues("", "asd")]string pseudonymFamilyName,
			bool mustInformAboutWarnings)
		{
			try
			{
				var author = CreateAuthor(UnexistingAuthor,
					givenName,
					fullMiddleName,
					familyName,
					shortcut,
					pseudonymGivenName,
					pseudonymFullMiddleName,
					pseudonymFamilyName,
					mustInformAboutWarnings);

				var newGivenName = givenName + AnyWord;
				var newFullMiddleName = fullMiddleName + AnyWord;
				var newFamilyName = familyName + AnyWord;
				var newShortcut = shortcut + AnyWord;
				var newPseudonymGivenName = pseudonymGivenName + AnyWord;
				var newPseudonymFullMiddleName = pseudonymFullMiddleName + AnyWord;
				var newPseudonymFamilyName = pseudonymFamilyName + AnyWord;

				author.GivenName = newGivenName;
				author.FullMiddleName = newFullMiddleName;
				author.FamilyName = newFamilyName;
				author.Shortcut = newShortcut;
				author.Pseudonym.GivenName = newPseudonymGivenName;
				author.Pseudonym.FullMiddleName = newPseudonymFullMiddleName;
				author.Pseudonym.FamilyName = newPseudonymFamilyName;
				author.MustInformAboutWarnings = !mustInformAboutWarnings;

				int authorsCount = this.authorDao.Get(a => true).Count();

				this.authorDao.Update(author);

				int newAuthorsCount = this.authorDao.Get(a => true).Count();

				Assert.Equal(newAuthorsCount, authorsCount);

				Author createdAuthor = this.authorDao.Get(author.Id);

				Assert.True(!string.IsNullOrWhiteSpace(author.Shortcut));
				Assert.NotEqual(Guid.Empty, createdAuthor.Id);
				Assert.NotNull(createdAuthor.Pseudonym);

				Assert.Equal(newGivenName, createdAuthor.GivenName);
				Assert.Equal(newFullMiddleName, createdAuthor.FullMiddleName);
				Assert.Equal(newFamilyName, createdAuthor.FamilyName);
				Assert.Equal(newShortcut, createdAuthor.Shortcut);
				Assert.Equal(newPseudonymGivenName, createdAuthor.Pseudonym.GivenName);
				Assert.Equal(newPseudonymFullMiddleName, createdAuthor.Pseudonym.FullMiddleName);
				Assert.Equal(newPseudonymFamilyName, createdAuthor.Pseudonym.FamilyName);
				Assert.Equal(!mustInformAboutWarnings, createdAuthor.MustInformAboutWarnings);
			}
			finally
			{
				this.Dispose();
			}
		}

		
	}
}