namespace Snowinmars.UserSlice.UserDao
{
	internal static class LocalConst
	{

		internal class User
		{
			internal const string DeleteByIdCommand =
				" delete " +
				" from " + User.TableName +
				" where " + Column.Id + " = " + Parameter.Id;

			internal const string DeleteByUsernameCommand =
				" delete " +
				" from " + User.TableName +
				" where " + Column.Username + " = " + Parameter.Username;

			internal const string InsertCommand =
				" insert into " + User.TableName +
				" ( " + Column.Id +
				"," + Column.Salt +
				"," + Column.Roles +
				"," + Column.Email +
				"," + Column.Username +
				"," + Column.PasswordHash +
				"," + Column.LanguageCode + " ) " +
				" values ( " + Parameter.Id +
				"," + Parameter.Salt +
				"," + Parameter.Roles +
				"," + Parameter.Email +
				"," + Parameter.Username +
				"," + Parameter.PasswordHash +
				"," + Parameter.LanguageCode + " ) ";

			internal const string SelectAllCommand =
				" select " + Column.Id +
				"," + Column.Salt +
				"," + Column.Roles +
				"," + Column.Email +
				"," + Column.Username +
				"," + Column.PasswordHash +
				"," + Column.LanguageCode +
				" from " + User.TableName;

			internal const string SelectById =
				User.SelectAllCommand +
				" where " + Column.Id + " = " + Parameter.Id;

			internal const string SelectByUsername =
				User.SelectAllCommand +
				" where " + Column.Username + " = " + Parameter.Username;

			internal const string TableName = "[Users]";

			internal const string UpdateCommand =
				" update " + User.TableName +
				" set " + Column.Username + " = " + Parameter.Username +
				"," + Column.Roles + " = " + Parameter.Roles +
				"," + Column.Email + " = " + Parameter.Email +
				"," + Column.LanguageCode + " = " + Parameter.LanguageCode +
				" where " + Column.Id + " = " + Parameter.Id;

			internal class Column
			{
				internal const string Id = "Id";
				internal const string Salt = "Salt";
				internal const string Roles = "Roles";
				internal const string Email = "Email";
				internal const string Username = "Username";
				internal const string LanguageCode = "LanguageCode";
				internal const string PasswordHash = "PasswordHash";
			}

			internal class Parameter
			{
				internal const string Id = "@id";
				internal const string Salt = "@salt";
				internal const string Roles = "@roles";
				internal const string Email = "@email";
				internal const string Username = "@username";
				internal const string LanguageCode = "@languageCode";
				internal const string PasswordHash = "@passwordHash";
			}
		}
	}
}
