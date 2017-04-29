using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;

namespace Snowinmars.BackgroundWorkers
{
	public class EmailSender
	{
		private readonly string username;
		private readonly SecureString password;
		private readonly SmtpClient smtpClient;

		private readonly string toAdmin;
		private const string DefaultRe = "Snowinmars system message";
		private const string WarningRe = "Snowinmars system warning you";
		private readonly string from;

		public EmailSender()
		{
			this.toAdmin = ConfigurationManager.AppSettings["emailUsername"];
			this.from = ConfigurationManager.AppSettings["emailUsername"];

			this.username = ConfigurationManager.AppSettings["emailUsername"];
			this.password = DecryptString(ConfigurationManager.AppSettings["emailPassword"]);

			this.smtpClient = new SmtpClient
			{
				Port = 587,
				Host = ConfigurationManager.AppSettings["emailHost"],
				EnableSsl = true,
				Timeout = (int)TimeSpan.FromMinutes(10).TotalMilliseconds,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(this.username, this.password)
			};
		}

		private static readonly byte[] Entropy = Encoding.Unicode.GetBytes(ConfigurationManager.AppSettings["emailSalt"]);

		public static string EncryptString(SecureString input)
		{
			byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
				Encoding.Unicode.GetBytes(EmailSender.ToInsecureString(input)),
				EmailSender.Entropy,
				System.Security.Cryptography.DataProtectionScope.CurrentUser);
			return Convert.ToBase64String(encryptedData);
		}

		public static SecureString DecryptString(string encryptedData)
		{
			try
			{
				byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
					Convert.FromBase64String(encryptedData),
					EmailSender.Entropy,
					System.Security.Cryptography.DataProtectionScope.CurrentUser);
				return EmailSender.ToSecureString(Encoding.Unicode.GetString(decryptedData));
			}
			catch
			{
				return new SecureString();
			}
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

		public static string ToInsecureString(SecureString input)
		{
			string returnValue = string.Empty;
			IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
			try
			{
				returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
			}
			finally
			{
				System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
			}
			return returnValue;
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
							 $"Warning id: {warning.Id}" + Environment.NewLine +
							 $"Entity id: {warning.EntityId}" + Environment.NewLine +
							 warning.Message + Environment.NewLine + Environment.NewLine +
							 "check " + Common.Constant.SiteUrl + " for details";

			this.Send(message, to, re);
		}
	}
}