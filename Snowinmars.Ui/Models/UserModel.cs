﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Models
{
	public class UserModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password aren't equals")]
		public string PasswordConfirm { get; set; }

		public UserRoles Roles { get; set; }

		public string Email { get; set; }
	}
}