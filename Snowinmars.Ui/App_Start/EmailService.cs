using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snowinmars.Ui.AppStartHelpers;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Snowinmars.Ui.App_Start.EmailService), "Start")]

namespace Snowinmars.Ui.App_Start
{
    public static class EmailService
    {
        private static EmailSender EmailSender { get; set; }

        public static void Start()
        {
            EmailService.EmailSender = new EmailSender();
        }

        public static bool IsSmtpReady => EmailService.EmailSender.IsReady;

        public static void TryLogin(string entropy) => EmailService.EmailSender.TryLogin(entropy);

        public static bool SendToAdmin(string message)
        {
            if (!IsSmtpReady)
            {
                return false;
            }

            EmailService.EmailSender.Send(message);

            return true;
        }
    }
}