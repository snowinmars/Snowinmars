IF OBJECT_ID(N'[dbo].[User_Get]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[User_Get]'

	EXEC ('CREATE PROCEDURE [dbo].[User_Get] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[User_Get]'
GO

ALTER PROCEDURE [dbo].[User_Get]
(
	@id UNIQUEIDENTIFIER
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
	where u.Id = @id

END
GO