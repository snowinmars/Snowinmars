using System.Collections.Generic;
using Snowinmars.Common;
using Snowinmars.UserSlice.UserEntites;

namespace Snowinmars.UserSlice.UserBll.Interfaces
{
	public interface IUserLogic : ILayer<ApplicationUser>
	{
		void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);

		bool Authenticate(string username, string userModelPassword);

		string CalculateHash(string password, string salt);

		ApplicationUser Get(string username);

		UserRoles GetRolesForUser(string username);

		IEnumerable<string> GetUsersInRole(UserRoles role);

		bool IsUserInRole(string username, UserRoles role);

		bool IsUsernameExist(string username);

		void Remove(string username);

		void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);

		void WriteCryptographicData(string basePassword, ApplicationUser toUser);
	}
}
