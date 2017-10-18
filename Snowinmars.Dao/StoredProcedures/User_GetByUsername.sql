IF OBJECT_ID(N'[dbo].[User_GetByUsername]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[User_GetByUsername]'

	EXEC ('CREATE PROCEDURE [dbo].[User_GetByUsername] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[User_GetByUsername]'
GO

ALTER PROCEDURE [dbo].[User_GetByUsername]
(
	@username nvarchar(20)
)
AS
BEGIN

	select
		u.Id
		,u.Username
		,u.PasswordHash
		,u.Roles
		,u.Email
		,u.Salt
		,u.LanguageCode
	from Users u
	where u.Username = @username

END
GO