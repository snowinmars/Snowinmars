using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Snowinmars.Common;
using Snowinmars.UserSlice.UserDao.Interfaces;
using Snowinmars.UserSlice.UserEntites;

namespace Snowinmars.UserSlice.UserDao
{
	public class UserDao : IUserDao
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

		public void Create(ApplicationUser item)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.InsertCommand, sqlConnection);

				var id = item.Id.ConvertToDbValue();
				var username = item.Username.ConvertToDbValue();
				var passwordHash = item.PasswordHash.ConvertToDbValue();
				var roles = item.Roles.ConvertToDbValue();
				var email = item.Email.ConvertToDbValue();
				var salt = item.Salt.ConvertToDbValue();
				var language = item.Language.ConvertToDbValue();

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, username);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.PasswordHash, passwordHash);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Roles, roles);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Email, email);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Salt, salt);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.LanguageCode, language);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public ApplicationUser Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.SelectById, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, id.ConvertToDbValue());

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				if (!reader.Read())
				{
					throw new ObjectNotFoundException();
				}

				ApplicationUser user = LocalCommon.MapUser(reader);
				sqlConnection.Close();

				return user;
			}
		}

		public ApplicationUser Get(string username)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.SelectByUsername, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, username.ConvertToDbValue());

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				if (!reader.Read())
				{
					throw new ObjectNotFoundException();
				}

				ApplicationUser user = LocalCommon.MapUser(reader);
				sqlConnection.Close();

				return user;
			}
		}

		public IEnumerable<ApplicationUser> Get(Func<ApplicationUser, bool> filter)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.SelectAllCommand, sqlConnection);

				IList<ApplicationUser> users = new List<ApplicationUser>();

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
				var command = new SqlCommand(LocalConst.User.DeleteByIdCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, id.ConvertToDbValue());

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public void Remove(string username)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.DeleteByUsernameCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, username.ConvertToDbValue());

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

		public void Update(ApplicationUser item)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.UpdateCommand, sqlConnection);

				var id = item.Id.ConvertToDbValue();
				var username = item.Username.ConvertToDbValue();
				var roles = item.Roles.ConvertToDbValue();
				var email = item.Email.ConvertToDbValue();
				var language = item.Language.ConvertToDbValue();

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, username);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Roles, roles);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Email, email);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.LanguageCode, language);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}
	}
}
