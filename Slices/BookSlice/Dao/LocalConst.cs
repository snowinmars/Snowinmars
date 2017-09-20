namespace Snowinmars.BookSlice.BookDao
{
	internal static class LocalConst
	{

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
	}
}
