using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandS.Algorithm.Library.GeneratorNamespace;
using Snowinmars.Dao.Interfaces;

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

		protected static Random Random { get; } = new Random();
		protected static TextGenerator TextGenerator { get; } = new TextGenerator();
		protected double AnyDouble => BaseTest.Random.NextDouble();
		protected int AnyInt => BaseTest.Random.Next();
		protected string AnyWord => BaseTest.TextGenerator.GetNewWord(4, 12, isFirstLerretUp: true);
		protected Guid UnexistingAuthor { get; private set; }

		public void Dispose()
		{
			this.authorDao.Remove(this.UnexistingAuthor);
		}
	}
}
