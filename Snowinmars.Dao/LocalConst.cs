using System.Reflection;

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

	    internal class Film
	    {
			internal const string TableName = "[Films]";

			internal const string DeleteCommand =
				" delete " +
			    " from " + Film.TableName +
			    " where ( " + Column.Id + " = " + Parameter.Id + " ) ";

		    internal const string InsertCommand =
			    " insert into " + Film.TableName +
					" ( " + Column.Id +
						"," + Column.Title +
						"," + Column.Year +
						"," + Column.KinopoiskUrl +
						"," + Column.Description +
					@")
				values
						( " + Parameter.Id +
						"," + Parameter.Title +
						"," + Parameter.Year +
						"," + Parameter.KinopoiskUrl +
						"," + Parameter.Description +
						")";

		    internal const string SelectAllCommand =
			    " select " + Column.Id +
					"," + Column.Title +
					"," + Column.Year +
					"," + Column.KinopoiskUrl +
					"," + Column.Description +
				" from " + Film.TableName;

		    internal const string SelectCommand = 
					Film.SelectAllCommand +
			    " where ( " + Column.Id + " = " + Parameter.Id + " ) ";

		    internal const string UpdateCommand =
			    " update " + TableName +
			    " set " + Column.Title + " = " + Parameter.Title +
					"," + Column.Year + " = " + Parameter.Year +
					"," + Column.KinopoiskUrl + " = " + Parameter.KinopoiskUrl +
					"," + Column.Description + " = " + Parameter.Description +
			    " where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal class Column
		    {
                internal const string Id = "Id";
			    internal const string Title = "Title";
			    internal const string Year = "Year";
			    internal const string KinopoiskUrl = "KinopoiskUrl";
			    internal const string AuthorIds = "Authors";
			    internal const string IsSynchronized = "IsSynchronized";
			    internal const string AuthorsShortcuts = "AuthorsShortcuts";
			    internal const string Description = "Description";
			}

		    internal class Parameter
		    {
			    internal const string Id = "@filmId";
				internal const string Title = "@title";
			    internal const string Year = "@year";
			    internal const string KinopoiskUrl = "@kinopoiskUrl";
			    internal const string AuthorIds = "@authors";
			    internal const string IsSynchronized = "@isSynchronized";
			    internal const string AuthorsShortcuts = "@authorsShortcuts";
			    internal const string Description = "@description";
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
                            "," + Column.Year +
                            "," + Column.Title +
                            "," + Column.Owner +
							"," + Column.Status +
                            "," + Column.PageCount +
                            "," + Column.Bookshelf +
                            "," + Column.LiveLibUrl +
                            "," + Column.LibRusEcUrl +
                            "," + Column.FlibustaUrl +
                            "," + Column.IsSynchronized +
                            "," + Column.AdditionalInfo +
                            "," + Column.AuthorsShortcuts +
                            "," + Column.MustInformAboutWarnings +
                         @")
					values
							( " + Parameter.Id +
                            "," + Parameter.Year +
                            "," + Parameter.Title +
                            "," + Parameter.Owner +
							"," + Parameter.Status +
                            "," + Parameter.PageCount +
                            "," + Parameter.Bookshelf +
                            "," + Parameter.LiveLibUrl +
                            "," + Parameter.LibRusEcUrl +
                            "," + Parameter.FlibustaUrl +
                            "," + Parameter.IsSynchronized +
                            "," + Parameter.AdditionalInfo +
                            "," + Parameter.AuthorsShortcuts +
                            "," + Parameter.MustInformAboutWarnings +
                            " ) ";

            internal const string SelectAllCommand =
                    " select " + Column.Id +
                            "," + Column.Year +
                            "," + Column.Title +
                            "," + Column.Owner +
							"," + Column.Status +
                            "," + Column.PageCount +
                            "," + Column.Bookshelf +
                            "," + Column.LiveLibUrl +
                            "," + Column.LibRusEcUrl +
                            "," + Column.FlibustaUrl +
                            "," + Column.IsSynchronized +
                            "," + Column.AdditionalInfo +
                            "," + Column.AuthorsShortcuts +
                            "," + Column.MustInformAboutWarnings +
                    " from " + Book.TableName;

            internal const string SelectBooksUnindexedByShortcutsCommand =
                    " select " + Column.Id +
                    " from " + Book.TableName +
                    " where ( " + Column.AuthorsShortcuts + " is null or " +
                                Column.AuthorsShortcuts + " = '' and " +
                                Column.IsSynchronized + " = 0 ) ";

            internal const string SelectCommand =
                                Book.SelectAllCommand +
                    " where ( " + Column.Id + " = " + Parameter.Id + " ) ";

	        internal const string GetWishlist =
		        Book.SelectAllCommand +
					" where ( " + Column.Owner + " = " + Parameter.Owner + " and " +
						Column.Status + " = " + Parameter.Status + " ) ";

			internal const string TableName = "[Books]";

            internal const string UpdateCommand =
                    " if (" + Parameter.AuthorsShortcuts + " = '__ignore'" + ") " +
                    " begin " +
                        " update " + Book.TableName +
                        " set " + Column.Title + " = " + Parameter.Title +
                                    "," + Column.Year + " = " + Parameter.Year +
                                    "," + Column.Owner + " = " + Parameter.Owner +
                                    "," + Column.Status + " = " + Parameter.Status +
                                    "," + Column.PageCount + " = " + Parameter.PageCount +
                                    "," + Column.Bookshelf + " = " + Parameter.Bookshelf +
                                    "," + Column.LiveLibUrl + " = " + Parameter.LiveLibUrl +
                                    "," + Column.LibRusEcUrl + " = " + Parameter.LibRusEcUrl +
                                    "," + Column.FlibustaUrl + " = " + Parameter.FlibustaUrl +
                                    "," + Column.IsSynchronized + " = " + Parameter.IsSynchronized +
                                    "," + Column.AdditionalInfo + " = " + Parameter.AdditionalInfo +
                                    "," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings +
                        " where ( " + Column.Id + " = " + Parameter.Id + " ) " +
                    " end " +
                    " else " +
                    " begin " +
                        " update " + Book.TableName +
                        " set " + Column.Title + " = " + Parameter.Title +
                                    "," + Column.Year + " = " + Parameter.Year +
                                    "," + Column.Owner + " = " + Parameter.Owner +
                                    "," + Column.Status + " = " + Parameter.Status +
                                    "," + Column.PageCount + " = " + Parameter.PageCount +
                                    "," + Column.Bookshelf + " = " + Parameter.Bookshelf +
                                    "," + Column.LiveLibUrl + " = " + Parameter.LiveLibUrl +
                                    "," + Column.LibRusEcUrl + " = " + Parameter.LibRusEcUrl +
                                    "," + Column.FlibustaUrl + " = " + Parameter.FlibustaUrl +
                                    "," + Column.IsSynchronized + " = " + Parameter.IsSynchronized +
                                    "," + Column.AdditionalInfo + " = " + Parameter.AdditionalInfo +
                                    "," + Column.AuthorsShortcuts + " = " + Parameter.AuthorsShortcuts +
                                    "," + Column.MustInformAboutWarnings + " = " + Parameter.MustInformAboutWarnings +
                        " where ( " + Column.Id + " = " + Parameter.Id + " ) " +
                    " end ";

            internal class Column
            {
                internal const string Id = "BookId";
                internal const string Year = "Year";
                internal const string Title = "Title";
                internal const string Owner = "Owner";
                internal const string Status = "Status";
                internal const string Authors = "Authors";
                internal const string Bookshelf = "Bookshelf";
                internal const string PageCount = "PageCount";
                internal const string LiveLibUrl = "LiveLibUrl";
                internal const string FlibustaUrl = "FlibustaUrl";
                internal const string LibRusEcUrl = "LibRusEcUrl";
                internal const string IsSynchronized = "IsSynchronized";
                internal const string AdditionalInfo = "AdditionalInfo";
                internal const string AuthorsShortcuts = "AuthorShortcuts";
                internal const string MustInformAboutWarnings = "MustInformAboutWarnings";
            }

            internal class Parameter
            {
                internal const string Id = "@bookId";
                internal const string Year = "@year";
                internal const string Title = "@title";
                internal const string Owner = "@owner";
                internal const string Status = "@status";
                internal const string Authors = "@authors";
                internal const string Bookshelf = "@bookshelf";
                internal const string PageCount = "@pageCount";
                internal const string LiveLibUrl = "@liveLibUrl";
                internal const string FlibustaUrl = "@flibustaUrl";
                internal const string LibRusEcUrl = "@libRusEcUrl";
                internal const string IsSynchronized = "@isSynchronized";
                internal const string AdditionalInfo = "@additionalInfo";
                internal const string AuthorsShortcuts = "@authorShortcuts";
                internal const string MustInformAboutWarnings = "@mustInformAboutWarnings";
            }
        }

		internal class FilmAuthor
		{
			internal const string DeleteFilmAuthorCommand =
				" delete " +
				" from " + FilmAuthor.TableName +
				" where ( " +
					Column.FilmId + " = " + Parameter.FilmId +
						" and " +
							Column.AuthorId + " = " + Parameter.AuthorId +
				" ) ";

			internal const string DeleteFilmCommand =
				" delete " +
				" from " + FilmAuthor.TableName +
				" where ( " + Column.FilmId + " = " + Parameter.FilmId + " ) ";

			internal const string InsertCommand =
				" insert into " + FilmAuthor.TableName +
					" ( " + Column.FilmId +
						", " + Column.AuthorId +
					@")
					 values
						( " + Parameter.FilmId +
						"," + Parameter.AuthorId +
					" ) ";

			internal const string SelectAllCommand =
				" select " + Column.FilmId +
					"," + Column.AuthorId +
				" from " + FilmAuthor.TableName;

			internal const string SelectByAuthorCommand =
				" select " + Column.FilmId +
					"," + Column.AuthorId +
				" from " + FilmAuthor.TableName +
				" where ( " + Column.AuthorId + " = " + Parameter.AuthorId + " ) ";

			internal const string SelectByFilmCommand =
				" select " + Column.FilmId +
					"," + Column.AuthorId +
				" from " + FilmAuthor.TableName +
				" where ( " + Column.FilmId + " = " + Parameter.FilmId + " ) ";

			internal const string TableName = "[FilmAuthorConnection]";

			internal class Column
			{
				internal const string FilmId = "FilmId";
				internal const string AuthorId = "AuthorId";
			}

			internal class Parameter
			{
				internal const string FilmId = "@FilmId";
				internal const string AuthorId = "@authorId";
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
                internal const string BookId = "BookId";
                internal const string AuthorId = "AuthorId";
            }

            internal class Parameter
            {
                internal const string BookId = "@bookId";
                internal const string AuthorId = "@authorId";
            }
        }

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