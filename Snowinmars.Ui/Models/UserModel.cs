using Snowinmars.Entities;
using Snowinmars.Ui.App_LocalResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Snowinmars.Ui.Models
{
    public class UserModel
    {
        private static readonly UserModel EmptyUser = new UserModel
        {
            Id = Guid.Empty,
            Email = "",
            Username = "",
            Password = "",
            PasswordConfirm = "",
            Language = Language.En,
            Roles = UserRoles.Banned,
        };

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@".*\@.*\..*", ErrorMessage = "This field must be email")]
        [Display(Name = "UserModel_Email", ResourceType = typeof(Global))]
        public string Email { get; set; }

        public static UserModel Empty => UserModel.EmptyUser;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "UserModel_Language", ResourceType = typeof(Global))]
        public Language Language { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "UserModel_Password", ResourceType = typeof(Global))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "UserModel_PasswordConfirmError", ErrorMessageResourceType = typeof(Global))]
        [Display(Name = "UserModel_PasswordConfirm", ResourceType = typeof(Global))]
        public string PasswordConfirm { get; set; }

        [Display(Name = "UserModel_Roles", ResourceType = typeof(Global))]
        public UserRoles Roles { get; set; }

        [Required]
        [Display(Name = "UserModel_Username", ResourceType = typeof(Global))]
        public string Username { get; set; }
    }
}