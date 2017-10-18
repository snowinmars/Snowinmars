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

	SELECT AuthorId
		,Shortcut
		,GivenName
		,FamilyName
		,IsSynchronized
		,FullMiddleName
		,PseudonymGivenName
		,PseudonymFamilyName
		,PseudonymFullMiddleName
		,MustInformAboutWarnings
	FROM [Authors]
	WHERE (AuthorId = @authorId)

END
GO