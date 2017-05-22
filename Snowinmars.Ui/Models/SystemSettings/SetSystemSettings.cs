using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snowinmars.Ui.Models
{
    public class SetSystemSettings
    {
        public string ShortcutJobSmtpEntropy { get; set; }
        public string WarningJobSmtpEntropy { get; set; }
        public string EmailServiceSmtpEntropy { get; set; }
    }
}