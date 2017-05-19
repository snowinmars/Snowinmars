using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;

namespace Snowinmars.Dao
{
	public class UserDao : IUserDao, ICRUD<User>
	{
		public void Create(User item)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.InsertCommand, sqlConnection);

				var id = LocalCommon.ConvertToDbValue(item.Id);
				var username = LocalCommon.ConvertToDbValue(item.Username);
				var passwordHash = LocalCommon.ConvertToDbValue(item.PasswordHash);
				var roles = LocalCommon.ConvertToDbValue(item.Roles);
				var email = LocalCommon.ConvertToDbValue(item.Email);
				var salt = LocalCommon.ConvertToDbValue(item.Salt);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, username);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.PasswordHash, passwordHash);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Roles, roles);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Email, email);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Salt, salt);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public User Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.SelectById, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, LocalCommon.ConvertToDbValue(id));

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
				var command = new SqlCommand(LocalConst.User.SelectByUsername, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, LocalCommon.ConvertToDbValue(username));

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

		public void Remove(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.DeleteByIdCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, LocalCommon.ConvertToDbValue(id));

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

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, LocalCommon.ConvertToDbValue(username));

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public void Update(User item)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.UpdateCommand, sqlConnection);

				var id = LocalCommon.ConvertToDbValue(item.Id);
				var username = LocalCommon.ConvertToDbValue(item.Username);
				var passwordHash = LocalCommon.ConvertToDbValue(item.PasswordHash);
				var roles = LocalCommon.ConvertToDbValue(item.Roles);
				var email = LocalCommon.ConvertToDbValue(item.Email);
				var salt = LocalCommon.ConvertToDbValue(item.Salt);

				command.Parameters.AddWithValue(LocalConst.User.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Username, username);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.PasswordHash, passwordHash);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Roles, roles);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Email, email);
				command.Parameters.AddWithValue(LocalConst.User.Parameter.Salt, salt);

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

		public bool IsUserInRole(string username, UserRoles role)
		{
			var user = this.Get(username);

			return user.Roles.HasFlag(role);
		}

		public IEnumerable<User> Get(Func<User, bool> filter)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.User.SelectAllCommand, sqlConnection);

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

		public IEnumerable<string> GetUsersInRole(UserRoles role)
		{
			return this.Get(u => true).Where(u => u.Roles.HasFlag(role)).Select(u =>u.Username);
		}

		public UserRoles GetRolesForUser(string username)
		{
			var user = this.Get(username);

			return user.Roles;
		}

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
	}
}
