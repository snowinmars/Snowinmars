using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snowinmars.Ui.Models
{
    public class GetSystemSettings
    {
        public bool IsShortcutJobSmtpServerReady { get; set; }
        public bool IsWarningJobSmtpServerReady { get; set; }
    }
}