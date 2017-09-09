using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Bll.Interfaces
{
	public interface IFilmLogic : ICRUD<Film>
	{
		IEnumerable<Film> Get(Expression<Func<Film, bool>> filter);
	}
}
