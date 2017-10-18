IF OBJECT_ID(N'[dbo].[User_Update]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[User_Update]'

	EXEC ('CREATE PROCEDURE [dbo].[User_Update] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[User_Update]'
GO

ALTER PROCEDURE [dbo].[User_Update]
(
	@userId UNIQUEIDENTIFIER
	,@username nvarchar(20)
	,@roles int
	,@email nvarchar(50)
	,@languageCode int
)
AS
BEGIN
	UPDATE [Users]
	SET Username = @username
		,Roles = @roles
		,Email = @email
		,LanguageCode = @languageCode
	WHERE Id = @userId
END
GO

