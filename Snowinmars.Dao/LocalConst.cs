using System;
using System.Collections.Generic;

namespace Snowinmars.Dao
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
						"," + Column.GivenName +
						"," + Column.FullMiddleName +
						"," + Column.FamilyName +
						"," + Column.Shortcut +
						"," + Column.PseudonymGivenName +
						"," + Column.PseudonymFullMiddleName +
						"," + Column.PseudonymFamilyName +
						"," + Column.MustInformAboutWarnings +
						@")
				values
					( " + Parameter.Id +
						"," + Parameter.FirstName +
						"," + Parameter.LastName +
						"," + Parameter.Surname +
						"," + Parameter.Shortcut +
						"," + Parameter.PseudonymGivenName +
						"," + Parameter.PseudonymFullMiddleName +
						"," + Parameter.PseudonymFamilyName +
						"," + Parameter.MustInformAboutWarnings + " ) ";

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
			    " from " + Author.TableName;

			internal const string SelectCommand =
				Author.SelectAllCommand +
				" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string TableName = "[Authors]";

			internal const string UpdateCommand = @"
				update " + Author.TableName + @"
				set " + Column.GivenName + " = " + Parameter.FirstName +
						"," + Column.FullMiddleName + " = " + Parameter.LastName +
						"," + Column.FamilyName + " = " + Parameter.Surname +
						"," + Column.Shortcut + " = " + Parameter.Shortcut +
						"," + Column.PseudonymGivenName + " = " + Parameter.PseudonymGivenName +
						"," + Column.PseudonymFullMiddleName + " = " + Parameter.PseudonymFullMiddleName +
						"," + Column.PseudonymFamilyName + " = " + Parameter.PseudonymFamilyName +
						"," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings +
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
			}

			internal class Parameter
			{
				internal const string FirstName = "@givenName";
				internal const string Id = "@authorId";
				internal const string LastName = "@fullMiddleName";
				internal const string Shortcut = "@shortcut";
				internal const string Surname = "@familyName";
				internal const string PseudonymFullMiddleName = "@pseudonymFullMiddleName";
				internal const string PseudonymFamilyName = "@pseudonymFamilyName";
				internal const string PseudonymGivenName = "@pseudonymGivenName";
				internal const string MustInformAboutWarnings = "@mustInformAboutWarnings";
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
						 @")
					values
							( " + Parameter.Id +
							"," + Parameter.Title +
							"," + Parameter.PageCount +
							"," + Parameter.Year +
							"," + Parameter.AuthorsShortcuts +
							"," + Parameter.MustInformAboutWarnings +
							" ) ";

			internal const string SelectAllCommand =
					" select " + Column.Id +
							"," + Column.Title +
							"," + Column.PageCount +
							"," + Column.Year +
							"," + Column.AuthorsShortcuts +
							"," + Column.MustInformAboutWarnings +
					" from " + Book.TableName;

			internal const string SelectCommand =
					Book.SelectAllCommand +
					" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string SelectBooksUnindexedByShortcutsCommand =
					" select " + Column.Id +
					" from " + Book.TableName+
					" where ( " + Column.AuthorsShortcuts + " is null or " + 
								Column.AuthorsShortcuts + " = '' ) ";

			internal const string TableName = "[Books]";

			internal const string UpdateCommand =
					" update " + Book.TableName +
					" set " + Column.Title + " = " + Parameter.Title +
								"," + Column.PageCount + " = " + Parameter.PageCount +
								"," + Column.Year + " = " + Parameter.Year +
								"," + Column.AuthorsShortcuts + " = " + Parameter.AuthorsShortcuts +
								"," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings + 
					" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal class Column
			{
				internal const string Authors = "Authors";
				internal const string Id = "BookId";
				internal const string PageCount = "PageCount";
				internal const string Title = "Title";
				internal const string Year = "Year";
				internal const string AuthorsShortcuts = "AuthorShortcuts";
				internal const string MustInformAboutWarnings = "MustInformAboutWarnings";
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