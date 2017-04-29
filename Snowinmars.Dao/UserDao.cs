using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;

namespace Snowinmars.Dao
{
	public class UserDao : IUserDao, ICRUD<User>
	{
		public void Create(User item)
		{
			throw new NotImplementedException();
		}

		public User Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(User item)
		{
			throw new NotImplementedException();
		}

		public void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles)
		{
			throw new NotImplementedException();
		}

		public bool IsUserInRole(string username, UserRoles role)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetUsersInRole(UserRoles role)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<UserRoles> GetRolesForUser(string username)
		{
			throw new NotImplementedException();
		}

		public void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles)
		{
			throw new NotImplementedException();
		}

		public bool IsUsernameExist(string username)
		{
			throw new NotImplementedException();
		}

		public User Get(string username)
		{
			throw new NotImplementedException();
		}
	}
}
