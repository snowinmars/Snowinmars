IF OBJECT_ID(N'[dbo].[Author_Update]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[Author_Update]'

	EXEC ('CREATE PROCEDURE [dbo].[Author_Update] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Author_Update]'
GO

ALTER PROCEDURE [dbo].[Author_Update] (
	@authorId UNIQUEIDENTIFIER
	,@shortcut NVARCHAR(100)
	,@givenName NVARCHAR(100)
	,@familyName NVARCHAR(100)
	,@isSynchronized BIT
	,@fullMiddleName NVARCHAR(100)
	,@pseudonymGivenName NVARCHAR(100)
	,@pseudonymFamilyName NVARCHAR(100)
	,@pseudonymFullMiddleName NVARCHAR(100)
	,@mustInformAboutWarnings BIT
	)
AS
BEGIN

	UPDATE [Authors]
	SET GivenName = @givenName
		,Shortcut = @shortcut
		,FamilyName = @familyName
		,IsSynchronized = @isSynchronized
		,FullMiddleName = @fullMiddleName
		,PseudonymGivenName = @pseudonymGivenName
		,PseudonymFamilyName = @pseudonymFamilyName
		,PseudonymFullMiddleName = @pseudonymFullMiddleName
		,MustInformAboutWarnings = @mustInformAboutWarnings
	WHERE (AuthorId = @authorId)

END
GO