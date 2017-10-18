IF OBJECT_ID(N'[dbo].[Author_Insert]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[Author_Insert]'

	EXEC ('CREATE PROCEDURE [dbo].[Author_Insert] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Author_Insert]'
GO

ALTER PROCEDURE [dbo].[Author_Insert] (
	@authorId UNIQUEIDENTIFIER
	,@shortcut nvarchar(100)
	,@givenName nvarchar(100)
	,@familyName nvarchar(100)
	,@isSynchronized bit
	,@fullMiddleName nvarchar(100)
	,@pseudonymGivenName nvarchar(100)
	,@pseudonymFamilyName nvarchar(100)
	,@pseudonymFullMiddleName nvarchar(100)
	,@mustInformAboutWarnings bit
	)
AS
BEGIN

	INSERT INTO [Authors] (
		AuthorId
		,Shortcut
		,GivenName
		,FamilyName
		,IsSynchronized
		,FullMiddleName
		,PseudonymGivenName
		,PseudonymFamilyName
		,PseudonymFullMiddleName
		,MustInformAboutWarnings
		)
	VALUES (
		@authorId
		,@shortcut
		,@givenName
		,@familyName
		,@isSynchronized
		,@fullMiddleName
		,@pseudonymGivenName
		,@pseudonymFamilyName
		,@pseudonymFullMiddleName
		,@mustInformAboutWarnings
		)

END
GO