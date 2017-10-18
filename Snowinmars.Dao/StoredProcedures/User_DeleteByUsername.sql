IF OBJECT_ID(N'[dbo].[User_DeleteByUsername]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[User_DeleteByUsername]'

	EXEC ('CREATE PROCEDURE [dbo].[User_DeleteByUsername] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[User_DeleteByUsername]'
GO

ALTER PROCEDURE [dbo].[User_DeleteByUsername]
(
	@username nvarchar(20)
)
AS
BEGIN

	delete
	from Users
	where Username = @username

END
GO