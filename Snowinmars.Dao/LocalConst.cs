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
						@")
				values
					( " + Parameter.Id +
						"," + Parameter.GivenName +
						"," + Parameter.FullMiddleName +
						"," + Parameter.FamilyName +
						"," + Parameter.Shortcut +
						"," + Parameter.PseudonymGivenName +
						"," + Parameter.PseudonymFullMiddleName +
						"," + Parameter.PseudonymFamilyName + " ) ";

			internal const string SelectAllCommand = @"
				select " + Column.Id +
						"," + Column.GivenName +
						"," + Column.FullMiddleName +
						"," + Column.FamilyName +
						"," + Column.Shortcut +
						"," + Column.PseudonymGivenName +
						"," + Column.PseudonymFullMiddleName +
						"," + Column.PseudonymFamilyName +
						" from " + Author.TableName;

			internal const string SelectCommand = @"
				select " + Column.Id +
						"," + Column.GivenName +
						"," + Column.FullMiddleName +
						"," + Column.FamilyName +
						"," + Column.Shortcut +
						"," + Column.PseudonymGivenName +
						"," + Column.PseudonymFullMiddleName +
						"," + Column.PseudonymFamilyName +
				" from " + Author.TableName +
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
				" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal class Column
			{
				internal const string GivenName = "GivenName";
				internal const string Id = "AuthorId";
				internal const string FullMiddleName = "FullMiddleName";
				internal const string Shortcut = "Shortcut";
				internal const string FamilyName = "FamilyName";
				internal const string PseudonymGivenName = "PseudonymGivenName";
				internal const string PseudonymFullMiddleName = "PseudonymFullMiddleName";
				internal const string PseudonymFamilyName = "PseudonymFamilyName";
			}

			internal class Parameter
			{
				internal const string GivenName = "@givenName";
				internal const string Id = "@authorId";
				internal const string FullMiddleName = "@fullMiddleName";
				internal const string Shortcut = "@shortcut";
				internal const string FamilyName = "@familyName";
				internal const string PseudonymGivenName = "@pseudonymGivenName";
				internal const string PseudonymFullMiddleName = "@pseudonymFullMiddleName";
				internal const string PseudonymFamilyName = "@pseudonymFamilyName";
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
						 @")
					values
							( " + Parameter.Id +
							"," + Parameter.Title +
							"," + Parameter.PageCount +
							"," + Parameter.Year +
							" ) ";

			internal const string SelectAllCommand =
				" select b." + Column.Id +
							",b." + Column.Title +
							",b." + Column.PageCount +
							",b." + Column.Year +

							",a." + Author.Column.Id + 
				
							",a." + Author.Column.GivenName +
							",a." + Author.Column.FullMiddleName +
							",a." + Author.Column.FamilyName +
				
							",a." + Author.Column.PseudonymGivenName +
							",a." + Author.Column.PseudonymFullMiddleName +
							",a." + Author.Column.PseudonymFamilyName +
				
							",a." + Author.Column.Shortcut +

				" from " + Book.TableName + " b " +
					" inner join " + BookAuthor.TableName + " ba " +
						" on b." + Column.Id + " = ba." + BookAuthor.Column.BookId +
					" inner join " + Author.TableName + " a " +
						" on a." + Author.Column.Id + " = ba." + BookAuthor.Column.AuthorId;

			internal const string SelectCommand =
					Book.SelectAllCommand +
					" where ( b." + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string TableName = "[Books]";

			internal const string UpdateCommand =
					" update " + Book.TableName +
					" set " + Column.Title + " = " + Parameter.Title +
								"," + Column.PageCount + " = " + Parameter.PageCount +
								"," + Column.Year + " = " + Parameter.Year +
					" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal class Column
			{
				internal const string Authors = "Authors";
				internal const string Id = "BookId";
				internal const string PageCount = "PageCount";
				internal const string Title = "Title";
				internal const string Year = "Year";
			}

			internal class Parameter
			{
				internal const string Authors = "@authors";
				internal const string Id = "@bookId";
				internal const string PageCount = "@pageCount";
				internal const string Title = "@title";
				internal const string Year = "@year";
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