using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Snowinmars.Dao
{
    public class UserDao : IUserDao, ILayer<User>
    {
        public void AddUsersToRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles)
        {
            foreach (var username in usernames)
            {
                var user = this.Get(username);

                foreach (var role in roles)
                {
                    user.Roles |= role;
                }
            }
        }

        public void Create(User item)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("User_Insert");

                var id = LocalCommon.ConvertToDbValue(item.Id);
                var username = LocalCommon.ConvertToDbValue(item.Username);
                var passwordHash = LocalCommon.ConvertToDbValue(item.PasswordHash);
                var roles = LocalCommon.ConvertToDbValue(item.Roles);
                var email = LocalCommon.ConvertToDbValue(item.Email);
                var salt = LocalCommon.ConvertToDbValue(item.Salt);
                var language = LocalCommon.ConvertToDbValue(item.Language);

				databaseCommand.AddInputParameter(LocalConst.User.Parameter.Id,SqlDbType.UniqueIdentifier,  id);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Username, SqlDbType.NVarChar, username);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.PasswordHash, SqlDbType.NVarChar, passwordHash);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Roles, SqlDbType.Int, roles);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Email, SqlDbType.NVarChar, email);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Salt, SqlDbType.NVarChar, salt);
	            databaseCommand.AddInputParameter(LocalConst.User.Parameter.LanguageCode, SqlDbType.Int, language);

				var command = databaseCommand.GetSqlCommand(sqlConnection);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public User Get(Guid id)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("User_Get");

                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Id, SqlDbType.UniqueIdentifier, LocalCommon.ConvertToDbValue(id));

				var command = databaseCommand.GetSqlCommand(sqlConnection);

                sqlConnection.Open();
                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    throw new ObjectNotFoundException();
                }

                User user = LocalCommon.MapUser(reader);
                sqlConnection.Close();

                return user;
            }
        }

        public User Get(string username)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
	            DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("User_GetByUsername");

	            databaseCommand.AddInputParameter(LocalConst.User.Parameter.Username, SqlDbType.NVarChar, LocalCommon.ConvertToDbValue(username));

				var command = databaseCommand.GetSqlCommand(sqlConnection);

                sqlConnection.Open();
                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    throw new ObjectNotFoundException();
                }

                User user = LocalCommon.MapUser(reader);
                sqlConnection.Close();

                return user;
            }
        }

        public IEnumerable<User> Get(Func<User, bool> filter)
		{
			return UserDao.GetAll();
		}

		private static IEnumerable<User> GetAll()
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("User_GetAll");

				var command = databaseCommand.GetSqlCommand(sqlConnection);

				IList<User> users = new List<User>();

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var user = LocalCommon.MapUser(reader);
					users.Add(user);
				}

				sqlConnection.Close();

				return users;
			}
		}

		public UserRoles GetRolesForUser(string username)
        {
            var user = this.Get(username);

            return user.Roles;
        }

        public IEnumerable<string> GetUsersInRole(UserRoles role)
        {
            return this.Get(u => true).Where(u => u.Roles.HasFlag(role)).Select(u => u.Username);
        }

        public bool IsUserInRole(string username, UserRoles role)
        {
            var user = this.Get(username);

            return user.Roles.HasFlag(role);
        }

        public bool IsUsernameExist(string username)
        {
            try
            {
                var user = this.Get(username);
            }
            catch (ObjectNotFoundException)
            {
                return false;
            }

            return true;
        }

        public void Remove(Guid id)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("User_Delete");

	            databaseCommand.AddInputParameter(LocalConst.User.Parameter.Id, SqlDbType.NVarChar, LocalCommon.ConvertToDbValue(id));

				var command = databaseCommand.GetSqlCommand(sqlConnection);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void Remove(string username)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("User_DeleteByUsername");

	            databaseCommand.AddInputParameter(LocalConst.User.Parameter.Username, SqlDbType.NVarChar, LocalCommon.ConvertToDbValue(username));

				var command = databaseCommand.GetSqlCommand(sqlConnection);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void RemoveUsersFromRoles(IEnumerable<string> usernames, IEnumerable<UserRoles> roles)
        {
            foreach (var username in usernames)
            {
                var user = this.Get(username);

                foreach (var role in roles)
                {
                    user.Roles -= role;
                }
            }
        }

        public void Update(User item)
        {
            using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
            {
				DatabaseCommand databaseCommand = DatabaseCommand.StoredProcedure("User_Update");

                var id = LocalCommon.ConvertToDbValue(item.Id);
                var username = LocalCommon.ConvertToDbValue(item.Username);
                var roles = LocalCommon.ConvertToDbValue(item.Roles);
                var email = LocalCommon.ConvertToDbValue(item.Email);
                var language = LocalCommon.ConvertToDbValue(item.Language);
				
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Id,SqlDbType.UniqueIdentifier,  id);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Username, SqlDbType.NVarChar, username);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Roles,SqlDbType.Int,  roles);
                databaseCommand.AddInputParameter(LocalConst.User.Parameter.Email, SqlDbType.NVarChar, email);
				databaseCommand.AddInputParameter(LocalConst.User.Parameter.LanguageCode, SqlDbType.Int, language);

				var command = databaseCommand.GetSqlCommand(sqlConnection);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}