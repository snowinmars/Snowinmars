using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Entities;

namespace Snowinmars.Bll.Interfaces
{
	public interface IUserLogic : ICRUD<User>
	{
		string CalculateHash(string password, string salt);
		void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
		bool IsUserInRole(string username, UserRoles role);
		IEnumerable<string> GetUsersInRole(UserRoles role);
		UserRoles GetRolesForUser(string username);
		void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
		bool IsUsernameExist(string username);
		void Remove(string username);
		User Get(string username);
		bool Authenticate(User candidate, string userModelPassword);
		void SetupCryptography(User user);
	}
}
