IF OBJECT_ID(N'[dbo].[Author_GetAll]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[Author_GetAll]'

	EXEC ('CREATE PROCEDURE [dbo].[Author_GetAll] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Author_GetAll]'
GO

ALTER PROCEDURE [dbo].[Author_GetAll]
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
		LEFT OUTER join [BookAuthorConnection] ba
			on a.AuthorId = ba.AuthorId
END
GO