namespace Snowinmars.Dao
{
	internal static class LocalConst
	{
		internal class Author
		{
			internal const string TableName = "[Authors]";

			internal class Parameter
			{
				internal const string FirstName = "@firstName";
				internal const string LastName = "@lastName";
				internal const string Surname = "@surname";
				internal const string Shortcut = "@shortcut";
				internal const string Id = "@id";
			}

			internal class Column
			{
				internal const string FirstName = "FirstName";
				internal const string LastName = "LastName";
				internal const string Surname = "Surname";
				internal const string Shortcut = "Shortcut";
				internal const string Id = "Id";
			}

			internal const string InsertCommand =
				" insert into " + Author.TableName +
					"( " + Column.Id +
						"," + Column.FirstName +
						"," + Column.LastName +
						"," + Column.Surname +
						"," + Column.Shortcut +
						@")
				values
					( " + Parameter.Id +
						"," + Parameter.FirstName +
						"," + Parameter.LastName +
						"," + Parameter.Surname +
						"," + Parameter.Shortcut + " ) ";

			internal const string SelectCommand = @"
				select " + Column.Id +
						"," + Column.FirstName +
						"," + Column.LastName +
						"," + Column.Surname +
						"," + Column.Shortcut +
				" from " + Author.TableName +
				" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string DeleteCommand = @"
				delete
				from " + Author.TableName +
				" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string UpdateCommand = @"
				update " + Author.TableName + @"
				set
					( " + Column.Id + " = " + Parameter.Id +
						"," + Column.FirstName + " = " + Parameter.FirstName +
						"," + Column.LastName + " = " + Parameter.LastName +
						"," + Column.Surname + " = " + Parameter.Surname +
						"," + Column.Shortcut + " = " + Parameter.Shortcut +
				" where( " + Column.Id + " = " + Parameter.Id + " ) ";
		}

		internal class Book
		{
			internal const string TableName = "[Books]";

			internal class Parameter
			{
				internal const string Title = "@title";
				internal const string PageCount = "@pageCount";
				internal const string Year = "@year";
				internal const string Authors = "@authors";
				internal const string Id = "@id";
			}

			internal class Column
			{
				internal const string Title = "Title";
				internal const string PageCount = "PageCount";
				internal const string Year = "Year";
				internal const string Authors = "Authors";
				internal const string Id = "Id";
			}

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

			internal const string SelectCommand = 
					" select b." + Column.Id +
							",c." + BookAuthor.Column.AuthorId +
							"," + Column.Title +
							"," + Column.PageCount +
							"," + Column.Year +
					" from " + Book.TableName + " b " +
						" inner join " + BookAuthor.TableName + " c " +
							" on b." + Column.Id + " = " + " c." + BookAuthor.Column.BookId + 
					" where ( b." + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string DeleteCommand =
					" delete " + 
					" from " + Book.TableName +
					" where ( " + Column.Id + " = " + Parameter.Id + " ) ";

			internal const string UpdateCommand = 
					" update " + Book.TableName +
					" set ( " +
							Column.Id + " = " + Parameter.Id +
								"," + Column.Title + " = " + Parameter.Title +
								"," + Column.PageCount + " = " + Parameter.PageCount +
								"," + Column.Year + " = " + Parameter.Year +
								"," + Column.Authors + " = " + Parameter.Authors + 
							" ) " + 
					"where ( " + Column.Id + " = " + Parameter.Id + " ) ";
		}

		internal class BookAuthor
		{
			internal const string TableName = "[BookAuthorConnection]";

			internal const string InsertCommand =
				" insert into " + BookAuthor.TableName +
					" ( " + Column.BookId +
						", " + Column.AuthorId +
						@")
					 values
							( " + Parameter.BookId +
						"," + Parameter.AuthorId +
						" ) ";

			internal const string SelectByBookCommand =
				" select " + Column.BookId +
						"," + Column.AuthorId +
				" from " + BookAuthor.TableName +
				" where ( " + Column.BookId + " = " + Parameter.BookId + " ) ";

			internal const string SelectByAuthorCommand =
				" select " + Column.BookId +
						"," + Column.AuthorId +
				" from " + BookAuthor.TableName +
				" where ( " + Column.AuthorId + " = " + Parameter.AuthorId + " ) ";

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

			internal class Parameter
			{
				internal const string BookId = "@bookId";
				internal const string AuthorId = "@authorId";
			}

			internal class Column
			{
				internal const string BookId = "BookId";
				internal const string AuthorId = "AuthorId";
			}
		}
	}
}