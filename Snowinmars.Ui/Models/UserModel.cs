using System;
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

		[DataType(DataType.EmailAddress)]
		[RegularExpression(@".*\@.*\..*", ErrorMessage = "This field must be email")]
		public string Email { get; set; }

		public static UserModel Map(User user)
		{
			return new UserModel
			{
				Id = user.Id,
				Username = user.Username,
				Roles = user.Roles,
				Email = user.Email,
			};
		}

		private static readonly UserModel EmptyUser = new UserModel
		{
			Username = "",
			Email = "",
			Password = "",
			PasswordConfirm = "",
			Roles = UserRoles.Banned,
			Id = Guid.Empty,
		};

		public static UserModel Empty => UserModel.EmptyUser;
	}
}