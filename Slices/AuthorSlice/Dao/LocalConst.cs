namespace Snowinmars.AuthorSlice.AuthorDao
{
	internal static class LocalConst
	{
		internal class Author
		{
			internal const string DeleteCommand = @"
				delete
				from " + Author.TableName +
			                                      " where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string InsertCommand =
				" insert into " + Author.TableName +
				"( " + Column.Id +
				"," + Column.Shortcut +
				"," + Column.GivenName +
				"," + Column.FamilyName +
				"," + Column.IsSynchronized +
				"," + Column.FullMiddleName +
				"," + Column.PseudonymGivenName +
				"," + Column.PseudonymFamilyName +
				"," + Column.PseudonymFullMiddleName +
				"," + Column.MustInformAboutWarnings +
				@")
				values
					( " + Parameter.Id +
				"," + Parameter.Shortcut +
				"," + Parameter.GivenName +
				"," + Parameter.FamilyName +
				"," + Parameter.IsSynchronized +
				"," + Parameter.FullMiddleName +
				"," + Parameter.PseudonymGivenName +
				"," + Parameter.PseudonymFamilyName +
				"," + Parameter.PseudonymFullMiddleName +
				"," + Parameter.MustInformAboutWarnings +
				" ) ";

			internal const string SelectAllCommand =
				" select " + Column.Id +
				"," + Column.Shortcut +
				"," + Column.GivenName +
				"," + Column.FamilyName +
				"," + Column.IsSynchronized +
				"," + Column.FullMiddleName +
				"," + Column.PseudonymGivenName +
				"," + Column.PseudonymFamilyName +
				"," + Column.PseudonymFullMiddleName +
				"," + Column.MustInformAboutWarnings +
				" from " + Author.TableName;

			internal const string SelectCommand =
				Author.SelectAllCommand +
				" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string TableName = "[Authors]";

			internal const string UpdateCommand = @"
				update " + Author.TableName + @"
				set " + Column.GivenName + " = " + Parameter.GivenName +
			                                      "," + Column.Shortcut + " = " + Parameter.Shortcut +
			                                      "," + Column.FamilyName + " = " + Parameter.FamilyName +
			                                      "," + Column.IsSynchronized + " = " + Parameter.IsSynchronized +
			                                      "," + Column.FullMiddleName + " = " + Parameter.FullMiddleName +
			                                      "," + Column.PseudonymGivenName + " = " + Parameter.PseudonymGivenName +
			                                      "," + Column.PseudonymFamilyName + " = " + Parameter.PseudonymFamilyName +
			                                      "," + Column.PseudonymFullMiddleName + " = " + Parameter.PseudonymFullMiddleName +
			                                      "," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings +
			                                      " where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal class Column
			{
				internal const string Id = "AuthorId";
				internal const string Shortcut = "Shortcut";
				internal const string GivenName = "GivenName";
				internal const string FamilyName = "FamilyName";
				internal const string IsSynchronized = "IsSynchronized";
				internal const string FullMiddleName = "FullMiddleName";
				internal const string PseudonymGivenName = "PseudonymGivenName";
				internal const string PseudonymFamilyName = "PseudonymFamilyName";
				internal const string MustInformAboutWarnings = "MustInformAboutWarnings";
				internal const string PseudonymFullMiddleName = "PseudonymFullMiddleName";
			}

			internal class Parameter
			{
				internal const string Id = "@authorId";
				internal const string Shortcut = "@shortcut";
				internal const string GivenName = "@givenName";
				internal const string FamilyName = "@familyName";
				internal const string FullMiddleName = "@fullMiddleName";
				internal const string IsSynchronized = "@isSynchronized";
				internal const string PseudonymGivenName = "@pseudonymGivenName";
				internal const string PseudonymFamilyName = "@pseudonymFamilyName";
				internal const string MustInformAboutWarnings = "@mustInformAboutWarnings";
				internal const string PseudonymFullMiddleName = "@pseudonymFullMiddleName";
			}
		}
	}
}
