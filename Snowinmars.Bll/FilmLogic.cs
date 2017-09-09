using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;

namespace Snowinmars.Bll
{
	public class FilmLogic : IFilmLogic
	{
		public void Create(Film item)
		{
			throw new NotImplementedException();
		}

		public Film Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(Film item)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Film> Get(Expression<Func<Film, bool>> filter)
		{
			throw new NotImplementedException();
		}
	}
}
