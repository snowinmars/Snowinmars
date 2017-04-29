using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Models
{
	public class UserModel
	{
		public Guid Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public UserRoles Roles { get; set; }

		public string Email { get; set; }
	}
}