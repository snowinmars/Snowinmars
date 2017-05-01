using Snowinmars.Entities;
using Xunit;

namespace Snowinmars.Dao.Tests.Authors
{
	public class Smoke : BaseTest
	{
		[Fact]
		public void AuthorDefalutCtorMustWork()
		{
			Author author = new Author("");

			Assert.True(author.MustInformAboutWarnings);
			Assert.NotNull(author.GivenName);
			Assert.NotNull(author.FullMiddleName);
			Assert.NotNull(author.FamilyName);
			Assert.NotNull(author.Shortcut);
			Assert.NotNull(author.Pseudonym);
			Assert.NotNull(author.Pseudonym.GivenName);
			Assert.NotNull(author.Pseudonym.FullMiddleName);
			Assert.NotNull(author.Pseudonym.FamilyName);
		}
	}
}
