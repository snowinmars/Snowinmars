IF OBJECT_ID(N'[dbo].[User_GetAll]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[User_GetAll]'

	EXEC ('CREATE PROCEDURE [dbo].[User_GetAll] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[User_GetAll]'
GO

ALTER PROCEDURE [dbo].[User_GetAll]
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

END
GO