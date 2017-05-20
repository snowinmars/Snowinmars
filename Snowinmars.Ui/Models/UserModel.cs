using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Snowinmars.Entities;
using Snowinmars.Ui.App_LocalResources;

namespace Snowinmars.Ui.Models
{
	public class UserModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "UserModel_Username", ResourceType = typeof(Global))]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "UserModel_Password", ResourceType = typeof(Global))]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessageResourceName = "UserModel_PasswordConfirmError", ErrorMessageResourceType = typeof(Global))]
		[Display(Name = "UserModel_PasswordConfirm", ResourceType = typeof(Global))]
		public string PasswordConfirm { get; set; }

        [Display(Name = "UserModel_Roles", ResourceType = typeof(Global))]
        public UserRoles Roles { get; set; }

        [DataType(DataType.EmailAddress)]
		[RegularExpression(@".*\@.*\..*", ErrorMessage = "This field must be email")]
        [Display(Name = "UserModel_Email", ResourceType = typeof(Global))]
		public string Email { get; set; }

	    public Language Language { get; set; }

        public static UserModel Map(User user)
		{
			return new UserModel
			{
				Id = user.Id,
				Username = user.Username,
				Roles = user.Roles,
				Email = user.Email,
                Language = user.Language,
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
            Language = Language.En,
        };

		public static UserModel Empty => UserModel.EmptyUser;
	}
}