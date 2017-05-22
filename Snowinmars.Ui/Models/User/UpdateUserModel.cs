using Snowinmars.Entities;
using Snowinmars.Ui.App_LocalResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Snowinmars.Ui.Models
{
    public class UpdateUserModel : EntityModel
    {
        private static readonly UpdateUserModel EmptyUser = new UpdateUserModel
        {
            Email = "",
            Language = Language.En,
            Roles = UserRoles.Banned,
        };

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@".*\@.*\..*", ErrorMessage = "This field must be email")]
        [Display(Name = "UserModel_Email", ResourceType = typeof(Global))]
        public string Email { get; set; }

        public static UpdateUserModel Empty => UpdateUserModel.EmptyUser;

        [Display(Name = "UserModel_Language", ResourceType = typeof(Global))]
        public Language Language { get; set; }

        [Display(Name = "UserModel_Roles", ResourceType = typeof(Global))]
        public UserRoles Roles { get; set; }

        [Required]
        [Display(Name = "UserModel_Username", ResourceType = typeof(Global))]
        public string Username { get; set; }
    }
}