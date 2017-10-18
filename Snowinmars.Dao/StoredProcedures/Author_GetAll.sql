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

END
GO