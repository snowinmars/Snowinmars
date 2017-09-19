using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Entities;

namespace Snowinmars.Dao.Interfaces
{
	public interface IFilmDao : ICRUD<Film>
	{
		IEnumerable<Film> Get(Expression<Func<Film, bool>> filter);
	}
}
