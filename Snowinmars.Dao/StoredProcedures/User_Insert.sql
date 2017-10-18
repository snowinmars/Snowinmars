IF OBJECT_ID(N'[dbo].[User_Insert]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[User_Insert]'

	EXEC ('CREATE PROCEDURE [dbo].[User_Insert] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[User_Insert]'
GO

ALTER PROCEDURE [dbo].[User_Insert]
(
	@userId UNIQUEIDENTIFIER
	,@username nvarchar(20)
	,@passwordHash nvarchar(1000)
	,@roles int
	,@email nvarchar(50)
	,@salt nvarchar(50)
	,@languageCode int
)
AS
BEGIN

	insert into Users
	(
		Id
		,Salt
		,Roles
		,Email
		,Username
		,PasswordHash
		,LanguageCode
	)
	values
	(
		@userId
		,@salt
		,@roles
		,@email
		,@username
		,@passwordHash
		,@languageCode
	)

END
GO