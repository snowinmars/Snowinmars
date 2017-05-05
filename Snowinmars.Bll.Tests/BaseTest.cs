using SandS.Algorithm.Library.GeneratorNamespace;
using Snowinmars.Dao.Interfaces;
using System;
using System.Data;
using Snowinmars.Dao;
using Snowinmars.Entities;

namespace Snowinmars.Bll.Tests
{
	public class BaseTest
	{
		protected readonly IAuthorDao authorDao;

		protected BaseTest()
		{
			this.authorDao = new AuthorDao();

			this.UnexistingAuthor = Guid.NewGuid();
			this.EnsureThatAuthorDoesntExist();
		}
		protected Author CreateAnyAuthor()
		{
			return CreateAuthor(UnexistingAuthor, AnyWord, AnyWord, AnyWord, AnyWord, AnyWord, AnyWord, AnyWord, false);
		}

		protected Author CreateAuthor(Guid id, string givenName, string fullMiddleName, string familyName, string shortcut, string pseudonymGivenName, string pseudonymFullMiddleName, string pseudonymFamilyName, bool mustInformAboutWarnings)
		{
			Author author = new Author(shortcut)
			{
				Id = id,
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
			this.authorDao.Create(author);
			return author;
		}
		protected static Random Random { get; } = new Random();

		protected static TextGenerator TextGenerator { get; } = new TextGenerator();

		protected byte AnyByte => (byte)BaseTest.Random.Next(0, 255);

		protected double AnyDouble => BaseTest.Random.NextDouble();

		protected int AnyInt => BaseTest.Random.Next();

		protected short AnyShort => (short)BaseTest.Random.Next(-32768, 32767);

		protected string AnyWord => BaseTest.TextGenerator.GetNewWord(4, 12, isFirstLerretUp: true);

		protected Guid UnexistingAuthor { get; private set; }

		public void Dispose()
		{
			try
			{
				this.authorDao.Remove(this.UnexistingAuthor);
			}
			catch
			{
				// ignored due to this is test case
			}
		}

		private void EnsureThatAuthorDoesntExist()
		{
			bool found = false;

			while (!found)
			{
				try
				{
					var a = this.authorDao.Get(this.UnexistingAuthor);

					// if I'm on this line, that means that there's author, so I have to try another guid
					this.UnexistingAuthor = Guid.NewGuid();
				}
				catch (ObjectNotFoundException e)
				{
					// Exception means that there's no author with this guid, so I found what I wanted
					found = true;
				}
			}
		}
	}
}