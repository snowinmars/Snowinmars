using System;
using System.Data;
using Snowinmars.Common;
using Snowinmars.UserSlice.UserEntites;

namespace Snowinmars.UserSlice.UserDao
{
	internal static class LocalCommon
	{
		public static ApplicationUser MapUser(IDataRecord reader)
		{
			Guid id = reader[LocalConst.User.Column.Id].ConvertFromDbValue<Guid>();
			UserRoles roles = reader[LocalConst.User.Column.Roles].ConvertFromDbValue<UserRoles>();
			Language language = reader[LocalConst.User.Column.LanguageCode].ConvertFromDbValue<Language>();
			string salt = reader[LocalConst.User.Column.Salt].ConvertFromDbValueToString();
			string email = reader[LocalConst.User.Column.Email].ConvertFromDbValueToString();
			string username = reader[LocalConst.User.Column.Username].ConvertFromDbValueToString();
			string passwordHash = reader[LocalConst.User.Column.PasswordHash].ConvertFromDbValueToString();

			ApplicationUser user = new ApplicationUser(username)
			{
				Id = id,
				Salt = salt,
				Email = email,
				Roles = roles,
				Language = language,
				PasswordHash = passwordHash,
			};

			return user;
		}
	}
}
