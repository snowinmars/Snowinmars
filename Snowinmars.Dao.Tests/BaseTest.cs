using SandS.Algorithm.Library.GeneratorNamespace;
using Snowinmars.Dao.Interfaces;
using System;
using System.Data;

namespace Snowinmars.Dao.Tests
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