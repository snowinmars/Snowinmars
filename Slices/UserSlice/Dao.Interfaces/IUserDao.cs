using System.Collections.Generic;
using Snowinmars.Common;
using Snowinmars.UserSlice.UserEntites;

namespace Snowinmars.UserSlice.UserDao.Interfaces
{
	public interface IUserDao : ILayer<ApplicationUser>
	{
		void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);

		ApplicationUser Get(string username);

		UserRoles GetRolesForUser(string username);

		IEnumerable<string> GetUsersInRole(UserRoles role);

		bool IsUserInRole(string username, UserRoles role);

		bool IsUsernameExist(string username);

		void Remove(string username);

		void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
	}
}
