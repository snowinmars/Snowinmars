﻿using Snowinmars.Bll.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Snowinmars.Ui.Provider
{
    public class SnowinmarsRoleProvider : RoleProvider
    {
        private readonly string[] allRoles = Enum.GetNames(typeof(UserRoles));
        private readonly IUserLogic userLogic;

        public SnowinmarsRoleProvider(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        public SnowinmarsRoleProvider() : this(DependencyResolver.Current.GetService<IUserLogic>())
        {
        }

        public override string ApplicationName { get; set; } = "SnowinmarsApp";

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            IEnumerable<UserRoles> roles = SnowinmarsRoleProvider.GetRoles(roleNames);

            this.userLogic.AddUsersToRoles(usernames, roles);
        }

        public override string[] GetAllRoles()
        {
            return this.allRoles;
        }

        public override string[] GetRolesForUser(string username)
        {
            UserRoles roles = this.userLogic.GetRolesForUser(username);

            return roles.ToString().Split(new[] { ',', }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            UserRoles role = SnowinmarsRoleProvider.GetRole(roleName);

            IEnumerable<string> usernames = this.userLogic.GetUsersInRole(role);

            return usernames.ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            UserRoles role;

            if (!Enum.TryParse(roleName, out role))
            {
                return false;
            }

            return this.userLogic.IsUserInRole(username, role);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            IEnumerable<UserRoles> roles = SnowinmarsRoleProvider.GetRoles(roleNames);

            this.userLogic.RemoveUsersFromRoles(usernames, roles);
        }

        public override bool RoleExists(string roleName)
        {
            UserRoles userRole;
            return Enum.TryParse(roleName, out userRole);
        }

        private static UserRoles GetRole(string roleName)
        {
            UserRoles role;
            if (!Enum.TryParse(roleName, out role))
            {
                throw new InvalidOperationException($"Can't recognize role {roleName}");
            }

            return role;
        }

        private static IEnumerable<UserRoles> GetRoles(string[] roleNames)
        {
            IList<UserRoles> roles = new List<UserRoles>();

            foreach (var roleName in roleNames)
            {
                UserRoles role = SnowinmarsRoleProvider.GetRole(roleName);

                roles.Add(role);
            }

            return roles;
        }

        #region NotImplementedException

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        #endregion NotImplementedException
    }
}