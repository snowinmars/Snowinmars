using Snowinmars.Ui.App_LocalResources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Snowinmars.Ui.Models
{
    public class AuthorModel : EntityModel
    {
        private static readonly AuthorModel EmptyAuthor = new AuthorModel
        {
            Id = Guid.Empty,
            Shortcut = "",
            GivenName = "",
            FamilyName = "",
            FullMiddleName = "",
            PseudonymGivenName = "",
            PseudonymFamilyName = "",
            PseudonymFullMiddleName = "",
            IsSynchronized = true,
            MustInformAboutWarnings = true,
        };

        public static AuthorModel Emtpy => AuthorModel.EmptyAuthor;

        [DisplayName("Family name")]
        [Display(Name = "AuthorModel_FamilyName", ResourceType = typeof(Global))]
        public string FamilyName { get; set; }

        [Display(Name = "AuthorModel_FullMiddleName", ResourceType = typeof(Global))]
        public string FullMiddleName { get; set; }

        [Display(Name = "AuthorModel_GivenName", ResourceType = typeof(Global))]
        public string GivenName { get; set; }

        [DisplayName("Have I inform you about warnings?")]
        [Display(Name = "Model_InformAboutWarnings", ResourceType = typeof(Global))]
        public bool MustInformAboutWarnings { get; set; }

        [DisplayName("Pseudonym family name")]
        [Display(Name = "AuthorModel_PseudonymFamilyName", ResourceType = typeof(Global))]
        public string PseudonymFamilyName { get; set; }

        [DisplayName("Pseudonym full middle name")]
        [Display(Name = "AuthorModel_PseudonymFullMiddleName", ResourceType = typeof(Global))]
        public string PseudonymFullMiddleName { get; set; }

        [DisplayName("Pseudonym given name")]
        [Display(Name = "AuthorModel_PseudonymGivenName", ResourceType = typeof(Global))]
        public string PseudonymGivenName { get; set; }

        [Required]
        [Display(Name = "AuthorModel_Shortcut", ResourceType = typeof(Global))]
        public string Shortcut { get; set; }
    }
}