IF OBJECT_ID(N'[dbo].[Author_Get]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[Author_Get]'

	EXEC ('CREATE PROCEDURE [dbo].[Author_Get] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Author_Get]'
GO

ALTER PROCEDURE [dbo].[Author_Get] (@authorId UNIQUEIDENTIFIER)
AS
BEGIN

	SELECT ba.BookId
		,a.AuthorId
		,a.Shortcut
		,a.GivenName
		,a.FamilyName
		,a.IsSynchronized
		,a.FullMiddleName
		,a.PseudonymGivenName
		,a.PseudonymFamilyName
		,a.PseudonymFullMiddleName
		,a.MustInformAboutWarnings
	FROM [Authors] a
		inner join [BookAuthorConnection] ba
			on a.AuthorId = ba.AuthorId
	WHERE (a.AuthorId = @authorId)

END
GO