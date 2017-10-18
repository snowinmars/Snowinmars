namespace Snowinmars.Dao
{
    internal static class LocalConst
    {
        internal class Author
        {
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

        internal class Book
        {
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
                internal const string MustInformAboutWarnings = "@mustInformAboutWarnings";
            }
        }

        internal class BookAuthor
        {
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