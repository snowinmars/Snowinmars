using Snowinmars.Common;
using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Snowinmars.Ui.AppStartHelpers
{
    public class EmailSender
    {
        private const string DefaultRe = "Snowinmars system message";
        private const string WarningRe = "Snowinmars system warning you";
        private static readonly byte[] Entropy = Encoding.Unicode.GetBytes(ConfigurationManager.AppSettings["emailSalt"]);
        private readonly string from;
        private readonly SmtpClient smtpClient;
        private readonly string toAdmin;

        public EmailSender()
        {
            this.toAdmin = ConfigurationManager.AppSettings["emailUsername"];
            this.from = ConfigurationManager.AppSettings["emailUsername"];

            var username = ConfigurationManager.AppSettings["emailUsername"];
            var password = EmailSender.DecryptString(ConfigurationManager.AppSettings["emailPassword"]);

            this.smtpClient = new SmtpClient
            {
                Port = 587,
                Host = ConfigurationManager.AppSettings["emailHost"],
                EnableSsl = true,
                Timeout = (int)TimeSpan.FromMinutes(10).TotalMilliseconds,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData),
                                                                EmailSender.Entropy,
                                                                DataProtectionScope.CurrentUser);
                return EmailSender.ToSecureString(Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static string EncryptString(SecureString input)
        {
            byte[] encryptedData = ProtectedData.Protect(Encoding.Unicode.GetBytes(EmailSender.ToInsecureString(input)),
                                                            EmailSender.Entropy,
                                                            DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue;
            IntPtr ptr = Marshal.SecureStringToBSTR(input);

            try
            {
                returnValue = Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }

            return returnValue;
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();

            foreach (char c in input)
            {
                secure.AppendChar(c);
            }

            secure.MakeReadOnly();

            return secure;
        }

        public void Send(string message, string to, string re = EmailSender.DefaultRe)
        {
            MailMessage mailMessage = new MailMessage(this.from, to, re, message)
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            this.smtpClient.Send(mailMessage);
        }

        public void Send(string message, IEnumerable<string> to)
        {
            foreach (var t in to)
            {
                this.Send(message, t);
            }
        }

        public void Send(Warning warning) => this.Send(warning, this.toAdmin);

        public void Send(Warning warning, string to, string re = EmailSender.WarningRe)
        {
            string message = "Snowinmars system warning: " + Environment.NewLine +
                                
                                warning.Message + Environment.NewLine + Environment.NewLine +

                                $"Warning id: {warning.Id}" + Environment.NewLine +
                                $"Entity id: {warning.EntityId}" + Environment.NewLine +

                                "check " + Constant.SiteUrl + " for details";

            this.Send(message, to, re);
        }
    }
}