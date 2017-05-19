using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	[Flags]
	public enum UserRoles
	{
		Banned = 0,
		User = 1,
		Admin = 2,
		Root = 4,
	}
}
