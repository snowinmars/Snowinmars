using DevOne.Security.Cryptography.BCrypt;
using Snowinmars.Bll.Interfaces;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;

namespace Snowinmars.Bll
{
    public class UserLogic : IUserLogic, Interfaces.ICRUD<User>
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

        public bool Authenticate(User candidate, string userModelPassword)
        {
            Validation.Check(candidate);

            var existingUser = this.Get(candidate.Username);

            return BCryptHelper.HashPassword(userModelPassword, existingUser.Salt) == existingUser.PasswordHash;
        }

        public string CalculateHash(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                salt = BCryptHelper.GenerateSalt();
            }

            Validation.CheckPassword(password);
            Validation.CheckSalt(salt);

            return BCryptHelper.HashPassword(password, salt);
        }

        public void Create(User item)
        {
            if (string.IsNullOrWhiteSpace(item.Salt))
            {
                item.Salt = BCryptHelper.GenerateSalt();
            }

            Validation.Check(item);

            this.userDao.Create(item);
        }

        public User Get(Guid id)
        {
            Validation.Check(id);

            return this.userDao.Get(id);
        }

        public User Get(string username)
        {
            Validation.CheckUsername(username);

            return this.userDao.Get(username);
        }

        public IEnumerable<User> Get(Func<User, bool> filter)
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

        public void SetupCryptography(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Salt))
            {
                user.Salt = BCryptHelper.GenerateSalt();
            }
        }

        public void Update(User item)
        {
            Validation.Check(item);

            this.userDao.Update(item);
        }
    }
}