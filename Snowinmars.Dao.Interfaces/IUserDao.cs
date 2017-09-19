using Snowinmars.Entities;
using System;
using System.Collections.Generic;

namespace Snowinmars.Dao.Interfaces
{
    public interface IUserDao : ILayer<User>
    {
        void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);

        User Get(string username);

        UserRoles GetRolesForUser(string username);

        IEnumerable<string> GetUsersInRole(UserRoles role);

        bool IsUserInRole(string username, UserRoles role);

        bool IsUsernameExist(string username);

        void Remove(string username);

        void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles);
    }
}