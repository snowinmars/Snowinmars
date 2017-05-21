using Snowinmars.Entities;
using System;
using System.Collections.Generic;

namespace Snowinmars.Bll.Interfaces
{
    public interface IUserLogic : ICRUD<User>
    {
        void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);

        bool Authenticate(User candidate, string userModelPassword);

        string CalculateHash(string password, string salt);

        User Get(string username);

        IEnumerable<User> Get(Func<User, bool> filter);

        UserRoles GetRolesForUser(string username);

        IEnumerable<string> GetUsersInRole(UserRoles role);

        bool IsUserInRole(string username, UserRoles role);

        bool IsUsernameExist(string username);

        void Remove(string username);

        void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
        void SetupCryptography(User user);
    }
}