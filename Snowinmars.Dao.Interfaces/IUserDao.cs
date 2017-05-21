using Snowinmars.Entities;
using System;
using System.Collections.Generic;

namespace Snowinmars.Dao.Interfaces
{
    public interface IUserDao : ICRUD<User>
    {
        void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);

        User Get(string username);

        IEnumerable<User> Get(Func<User, bool> filter);

        UserRoles GetRolesForUser(string username);

        IEnumerable<string> GetUsersInRole(UserRoles role);

        bool IsUserInRole(string username, UserRoles role);

        bool IsUsernameExist(string username);

        void Remove(string username);

        void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
    }
}