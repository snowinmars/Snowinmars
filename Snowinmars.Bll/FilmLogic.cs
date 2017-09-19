using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;

namespace Snowinmars.Bll
{
	public class FilmLogic : IFilmLogic
	{
		private readonly IFilmDao filmDao;

		public FilmLogic(IFilmDao filmDao)
		{
			this.filmDao = filmDao;
		}

		public void Create(Film item)
		{
			Validation.Check(item);

			this.filmDao.Create(item);
		}

		public Film Get(Guid id)
		{
			Validation.Check(id);

			return this.filmDao.Get(id);
		}

		public void Remove(Guid id)
		{
			Validation.Check(id);

			this.filmDao.Remove(id);
		}

		public void Update(Film item)
		{
			Validation.Check(item);

			this.filmDao.Update(item);
		}

		public IEnumerable<Film> Get(Expression<Func<Film, bool>> filter)
		{
			return this.filmDao.Get(filter);
		}
	}
}
