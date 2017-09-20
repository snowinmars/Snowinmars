using System;
using System.Collections.Generic;
using DevOne.Security.Cryptography.BCrypt;
using Snowinmars.Common;
using Snowinmars.UserSlice.UserBll.Interfaces;
using Snowinmars.UserSlice.UserDao.Interfaces;
using Snowinmars.UserSlice.UserEntites;
using Validation = Snowinmars.UserSlice.UserEntites.Validation;

namespace Snowinmars.UserSlice.UserBll
{
	public class UserLogic : IUserLogic
	{
		private readonly IUserDao userDao;

		public UserLogic(IUserDao userDao)
		{
			this.userDao = userDao;
		}

		public void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles)
		{
			foreach (var username in usernames)
			{
				Validation.CheckUsername(username);
			}

			this.userDao.AddUsersToRoles(usernames, roles);
		}

		public bool Authenticate(string username, string userModelPassword)
		{
			Validation.CheckUsername(username);

			var existingUser = this.Get(username);

			return BCryptHelper.HashPassword(userModelPassword, existingUser.Salt) == existingUser.PasswordHash;
		}

		public string CalculateHash(string password, string salt = null)
		{
			if (string.IsNullOrWhiteSpace(salt))
			{
				salt = BCryptHelper.GenerateSalt();
			}

			Validation.CheckPassword(password);
			Validation.CheckSalt(salt);

			return BCryptHelper.HashPassword(password, salt);
		}

		public void Create(ApplicationUser item)
		{
			Validation.Check(item);

			this.userDao.Create(item);
		}

		public ApplicationUser Get(Guid id)
		{
			Validation.Check(id);

			return this.userDao.Get(id);
		}

		public ApplicationUser Get(string username)
		{
			Validation.CheckUsername(username);

			return this.userDao.Get(username);
		}

		public IEnumerable<ApplicationUser> Get(Func<ApplicationUser, bool> filter)
		{
			return this.userDao.Get(filter);
		}

		public UserRoles GetRolesForUser(string username)
		{
			Validation.CheckUsername(username);

			return this.userDao.GetRolesForUser(username);
		}

		public IEnumerable<string> GetUsersInRole(UserRoles role)
		{
			return this.userDao.GetUsersInRole(role);
		}

		public bool IsUserInRole(string username, UserRoles role)
		{
			Validation.CheckUsername(username);

			return this.userDao.IsUserInRole(username, role);
		}

		public bool IsUsernameExist(string username)
		{
			return this.userDao.IsUsernameExist(username);
		}

		public void Remove(Guid id)
		{
			Validation.Check(id);

			this.userDao.Remove(id);
		}

		public void Remove(string username)
		{
			this.userDao.Remove(username);
		}

		public void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles)
		{
			foreach (var username in usernames)
			{
				Validation.CheckUsername(username);
			}

			this.userDao.RemoveUsersFromRoles(usernames, roles);
		}



		public void Update(ApplicationUser item)
		{
			Validation.Check(item);

			this.userDao.Update(item);
		}

		public void WriteCryptographicData(string basePassword, ApplicationUser toUser)
		{
			toUser.Salt = BCryptHelper.GenerateSalt();
			toUser.PasswordHash = this.CalculateHash(basePassword, toUser.Salt);
		}
	}
}
