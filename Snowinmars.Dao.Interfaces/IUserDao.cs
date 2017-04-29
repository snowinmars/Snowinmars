using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Entities;

namespace Snowinmars.Dao.Interfaces
{
	public interface IUserDao : ICRUD<User>
	{
		void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
		bool IsUserInRole(string username, UserRoles role);
		IEnumerable<string> GetUsersInRole(UserRoles role);
		UserRoles GetRolesForUser(string username);
		void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
		bool IsUsernameExist(string username);
		User Get(string username);
		void Remove(string username);
	}
}
