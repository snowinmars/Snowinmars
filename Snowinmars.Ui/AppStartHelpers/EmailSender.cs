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
        private byte[] entropy;
        private readonly string from;
        private SmtpClient smtpClient;
        private readonly string toAdmin;

        public bool IsReady => this.entropy != null && this.smtpClient?.Credentials != null;

        public EmailSender()
        {
            this.toAdmin = Constant.EmailUsername;
            this.from = Constant.EmailUsername;
        }

        public void TryLogin(string entropy)
        {
            this.entropy = Encoding.Unicode.GetBytes(entropy);

            var username = Constant.EmailUsername;
            var password = EmailSender.DecryptString(Constant.EmailPassword, this.entropy);

            this.smtpClient = new SmtpClient
            {
                Port = 587,
                Host = Constant.EmailHost,
                EnableSsl = true,
                Timeout = (int)TimeSpan.FromMinutes(10).TotalMilliseconds,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            try
            {
                this.Send($"Smtp server started at {DateTime.Now}", this.toAdmin, "Snowinmars system notification");
            }
            catch (SmtpException)
            {
                this.smtpClient = null;
                this.entropy = null;
            }
        }

        public static SecureString DecryptString(string encryptedData, byte[] entropy)
        {
            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData),
                                                                entropy,
                                                                DataProtectionScope.CurrentUser);
                return EmailSender.ToSecureString(Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static string EncryptString(SecureString input, byte[] entropy)
        {
            byte[] encryptedData = ProtectedData.Protect(Encoding.Unicode.GetBytes(EmailSender.ToInsecureString(input)),
                                                            entropy,
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