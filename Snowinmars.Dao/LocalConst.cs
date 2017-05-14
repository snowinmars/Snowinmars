using System.Security.Cryptography;

namespace Snowinmars.Dao
{
	internal static class LocalConst
	{
		internal class User
		{
			internal const string InsertCommand =
				" insert into " + User.TableName +
						" ( " + Column.Id +
							"," + Column.Username +
							"," + Column.PasswordHash +
							"," + Column.Roles +
							"," + Column.Email +
							"," + Column.Salt + " ) " +
				" values ( " + Parameter.Id +
							"," + Parameter.Username +
							"," + Parameter.PasswordHash +
							"," + Parameter.Roles +
							"," + Parameter.Email +
							"," + Parameter.Salt + " ) ";

			internal const string SelectAllCommand =
				" select " + Column.Id +
							"," + Column.Username +
							"," + Column.PasswordHash +
							"," + Column.Roles +
							"," + Column.Email +
							"," + Column.Salt +
				" from " + User.TableName;

			internal const string SelectById =
				User.SelectAllCommand +
				" where " + Column.Id + " = " + Parameter.Id;

			internal const string SelectByUsername =
				User.SelectAllCommand +
				" where " + Column.Username + " = " + Parameter.Username;

			internal const string DeleteByIdCommand =
				" delete " +
				" from " + User.TableName +
				" where " + Column.Id + " = " + Parameter.Id;

			internal const string DeleteByUsernameCommand =
				" delete " +
				" from " + User.TableName +
				" where " + Column.Username + " = " + Parameter.Username;

			internal const string UpdateCommand =
				" update " + User.TableName +
					" set " + Column.Username + " = " + Parameter.Username +
								"," + Column.PasswordHash + " = " + Parameter.PasswordHash +
								"," + Column.Roles + " = " + Parameter.Roles +
								"," + Column.Email + " = " + Parameter.Email +
								"," + Column.Salt + " = " + Parameter.Salt +
				" where " + Column.Id + " = " + Parameter.Id;

			internal const string TableName = "[Users]";

			internal class Column
			{
				internal const string Id = "Id";
				internal const string Username = "Username";
				internal const string PasswordHash = "PasswordHash";
				internal const string Roles = "Roles";
				internal const string Email = "Email";
				internal const string Salt = "Salt";
			}

			internal class Parameter
			{
				internal const string Id = "@id";
				internal const string Username = "@username";
				internal const string PasswordHash = "@passwordHash";
				internal const string Roles = "@roles";
				internal const string Email = "@email";
				internal const string Salt = "@salt";
			}
		}

		internal class Author
		{
			internal const string DeleteCommand = @"
				delete
				from " + Author.TableName +
				" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string InsertCommand =
				" insert into " + Author.TableName +
					"( " + Column.Id +
						"," + Column.GivenName +
						"," + Column.FullMiddleName +
						"," + Column.FamilyName +
						"," + Column.Shortcut +
						"," + Column.PseudonymGivenName +
						"," + Column.PseudonymFullMiddleName +
						"," + Column.PseudonymFamilyName +
						"," + Column.MustInformAboutWarnings +
						"," + Column.IsSynchronized +
                        @")
				values
					( " + Parameter.Id +
						"," + Parameter.GivenName +
						"," + Parameter.FullMiddleName +
						"," + Parameter.FamilyName +
						"," + Parameter.Shortcut +
						"," + Parameter.PseudonymGivenName +
						"," + Parameter.PseudonymFullMiddleName +
						"," + Parameter.PseudonymFamilyName +
						"," + Parameter.MustInformAboutWarnings +
                        "," + Parameter.IsSynchronized + " ) ";

			internal const string SelectAllCommand =
				" select " + Column.Id +
						"," + Column.GivenName +
						"," + Column.FullMiddleName +
						"," + Column.FamilyName +
						"," + Column.Shortcut +
						"," + Column.PseudonymGivenName +
						"," + Column.PseudonymFullMiddleName +
						"," + Column.PseudonymFamilyName +
						"," + Column.MustInformAboutWarnings +
						"," + Column.IsSynchronized +
				" from " + Author.TableName;

			internal const string SelectCommand =
				Author.SelectAllCommand +
				" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string TableName = "[Authors]";

			internal const string UpdateCommand = @"
				update " + Author.TableName + @"
				set " + Column.GivenName + " = " + Parameter.GivenName +
						"," + Column.FullMiddleName + " = " + Parameter.FullMiddleName +
						"," + Column.FamilyName + " = " + Parameter.FamilyName +
						"," + Column.Shortcut + " = " + Parameter.Shortcut +
						"," + Column.PseudonymGivenName + " = " + Parameter.PseudonymGivenName +
						"," + Column.PseudonymFullMiddleName + " = " + Parameter.PseudonymFullMiddleName +
						"," + Column.PseudonymFamilyName + " = " + Parameter.PseudonymFamilyName +
						"," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings +
                        "," + Column.IsSynchronized + " = " + Parameter.IsSynchronized +
                " where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal class Column
			{
				internal const string GivenName = "GivenName";
				internal const string Id = "AuthorId";
				internal const string FullMiddleName = "FullMiddleName";
				internal const string Shortcut = "Shortcut";
				internal const string FamilyName = "FamilyName";
				internal const string PseudonymFullMiddleName = "PseudonymFullMiddleName";
				internal const string PseudonymFamilyName = "PseudonymFamilyName";
				internal const string PseudonymGivenName = "PseudonymGivenName";
				internal const string MustInformAboutWarnings = "MustInformAboutWarnings";
                internal const string IsSynchronized = "IsSynchronized";
            }

			internal class Parameter
			{
				internal const string GivenName = "@givenName";
				internal const string Id = "@authorId";
				internal const string FullMiddleName = "@fullMiddleName";
				internal const string Shortcut = "@shortcut";
				internal const string FamilyName = "@familyName";
				internal const string PseudonymFullMiddleName = "@pseudonymFullMiddleName";
				internal const string PseudonymFamilyName = "@pseudonymFamilyName";
				internal const string PseudonymGivenName = "@pseudonymGivenName";
				internal const string MustInformAboutWarnings = "@mustInformAboutWarnings";
                internal const string IsSynchronized = "@isSynchronized";
            }
		}

		internal class Book
		{
			internal const string DeleteCommand =
					" delete " +
					" from " + Book.TableName +
					" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string InsertCommand =
					" insert into " + Book.TableName +
						"( " + Column.Id +
							"," + Column.Title +
							"," + Column.PageCount +
							"," + Column.Year +
							"," + Column.AuthorsShortcuts +
							"," + Column.MustInformAboutWarnings +
							"," + Column.Bookshelf +
							"," + Column.AdditionalInfo +
							"," + Column.LiveLibUrl +
							"," + Column.LibRusEcUrl +
							"," + Column.FlibustaUrl +
							"," + Column.Owner +
							"," + Column.IsSynchronized +
						 @")
					values
							( " + Parameter.Id +
							"," + Parameter.Title +
							"," + Parameter.PageCount +
							"," + Parameter.Year +
							"," + Parameter.AuthorsShortcuts +
							"," + Parameter.MustInformAboutWarnings +
							"," + Parameter.Bookshelf +
							"," + Parameter.AdditionalInfo +
							"," + Parameter.LiveLibUrl +
							"," + Parameter.LibRusEcUrl +
							"," + Parameter.FlibustaUrl +
							"," + Parameter.Owner +
							"," + Parameter.IsSynchronized +
							" ) ";

			internal const string SelectAllCommand =
					" select " + Column.Id +
							"," + Column.Title +
							"," + Column.PageCount +
							"," + Column.Year +
							"," + Column.AuthorsShortcuts +
							"," + Column.MustInformAboutWarnings +
							"," + Column.Bookshelf +
							"," + Column.AdditionalInfo +
							"," + Column.LiveLibUrl +
							"," + Column.LibRusEcUrl +
							"," + Column.FlibustaUrl +
							"," + Column.Owner +
							"," + Column.IsSynchronized +
					" from " + Book.TableName;

			internal const string SelectCommand =
					Book.SelectAllCommand +
					" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string SelectBooksUnindexedByShortcutsCommand =
					" select " + Column.Id +
					" from " + Book.TableName +
					" where ( " + Column.AuthorsShortcuts + " is null or " +
								Column.AuthorsShortcuts + " = '' and " + 
                                Column.IsSynchronized + " = 0 ) ";

			internal const string TableName = "[Books]";

			internal const string UpdateCommand =
					" if (" + Parameter.AuthorsShortcuts + " = '__ignore'" + ") " +
					" begin " +
						" update " + Book.TableName +
						" set " + Column.Title + " = " + Parameter.Title +
									"," + Column.PageCount + " = " + Parameter.PageCount +
									"," + Column.Year + " = " + Parameter.Year +
									"," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings +
									"," + Column.Bookshelf + " = " + Parameter.Bookshelf +
									"," + Column.AdditionalInfo + " = " + Parameter.AdditionalInfo +
									"," + Column.LiveLibUrl + " = " + Parameter.LiveLibUrl +
									"," + Column.LibRusEcUrl + " = " + Parameter.LibRusEcUrl +
									"," + Column.FlibustaUrl + " = " + Parameter.FlibustaUrl +
									"," + Column.Owner + " = " + Parameter.Owner +
                                    "," + Column.IsSynchronized + " = " + Parameter.IsSynchronized +
						" where ( " + Column.Id + " = " + Parameter.Id + " ) " +
					" end " +
					" else " +
					" begin " +
						" update " + Book.TableName +
						" set " + Column.Title + " = " + Parameter.Title +
									"," + Column.PageCount + " = " + Parameter.PageCount +
									"," + Column.Year + " = " + Parameter.Year +
									"," + Column.AuthorsShortcuts + " = " + Parameter.AuthorsShortcuts +
									"," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings +
									"," + Column.Bookshelf + " = " + Parameter.Bookshelf +
									"," + Column.AdditionalInfo + " = " + Parameter.AdditionalInfo +
									"," + Column.LiveLibUrl + " = " + Parameter.LiveLibUrl +
									"," + Column.LibRusEcUrl + " = " + Parameter.LibRusEcUrl +
									"," + Column.FlibustaUrl + " = " + Parameter.FlibustaUrl +
									"," + Column.Owner + " = " + Parameter.Owner +
									"," + Column.IsSynchronized + " = " + Parameter.IsSynchronized +
						" where ( " + Column.Id + " = " + Parameter.Id + " ) " +
					" end ";

			internal class Column
			{
				internal const string Authors = "Authors";
				internal const string Id = "BookId";
				internal const string PageCount = "PageCount";
				internal const string Title = "Title";
				internal const string Year = "Year";
				internal const string AuthorsShortcuts = "AuthorShortcuts";
				internal const string MustInformAboutWarnings = "MustInformAboutWarnings";
				internal const string Bookshelf = "Bookshelf";
				internal const string AdditionalInfo = "AdditionalInfo";
				internal const string LiveLibUrl = "LiveLibUrl";
				internal const string LibRusEcUrl = "LibRusEcUrl";
				internal const string FlibustaUrl = "FlibustaUrl";
				internal const string Owner = "Owner";
                internal const string IsSynchronized = "IsSynchronized";
            }

			internal class Parameter
			{
				internal const string Authors = "@authors";
				internal const string Id = "@bookId";
				internal const string PageCount = "@pageCount";
				internal const string Title = "@title";
				internal const string Year = "@year";
				internal const string AuthorsShortcuts = "@authorShortcuts";
				internal const string MustInformAboutWarnings = "@mustInformAboutWarnings";
				internal const string Bookshelf = "@bookshelf";
				internal const string AdditionalInfo = "@additionalInfo";
				internal const string LiveLibUrl = "@liveLibUrl";
				internal const string LibRusEcUrl = "@libRusEcUrl";
				internal const string FlibustaUrl = "@flibustaUrl";
				internal const string Owner = "@owner";
                internal const string IsSynchronized = "@isSynchronized";
            }
        }

		internal class BookAuthor
		{
			internal const string DeleteBookAuthorCommand =
				" delete " +
				" from " + BookAuthor.TableName +
				" where ( " +
						Column.BookId + " = " + Parameter.BookId +
						" and " +
						Column.AuthorId + " = " + Parameter.AuthorId +
				" ) ";

			internal const string DeleteBookCommand =
				" delete " +
				" from " + BookAuthor.TableName +
				" where ( " + Column.BookId + " = " + Parameter.BookId + " ) ";

			internal const string InsertCommand =
				" insert into " + BookAuthor.TableName +
					" ( " + Column.BookId +
						", " + Column.AuthorId +
						@")
					 values
							( " + Parameter.BookId +
						"," + Parameter.AuthorId +
						" ) ";

			internal const string SelectAllCommand =
				" select " + Column.BookId +
						"," + Column.AuthorId +
				" from " + BookAuthor.TableName;

			internal const string SelectByAuthorCommand =
				" select " + Column.BookId +
						"," + Column.AuthorId +
				" from " + BookAuthor.TableName +
				" where ( " + Column.AuthorId + " = " + Parameter.AuthorId + " ) ";

			internal const string SelectByBookCommand =
				" select " + Column.BookId +
						"," + Column.AuthorId +
				" from " + BookAuthor.TableName +
				" where ( " + Column.BookId + " = " + Parameter.BookId + " ) ";

			internal const string TableName = "[BookAuthorConnection]";

			internal class Column
			{
				internal const string AuthorId = "AuthorId";
				internal const string BookId = "BookId";
			}

			internal class Parameter
			{
				internal const string AuthorId = "@authorId";
				internal const string BookId = "@bookId";
			}
		}
	}
}