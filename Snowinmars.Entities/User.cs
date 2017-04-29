using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowinmars.Entities
{
	public class User : Entity
	{
		public string Username { get; set; }

		public byte[] PasswordHash { get; set; }

		public UserRoles Roles { get; set; }

		public string Email { get; set; }

		public User(string username)
		{
			this.Username = username;
		}
	}
}
