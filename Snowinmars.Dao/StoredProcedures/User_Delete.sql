IF OBJECT_ID(N'[dbo].[User_Delete]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[User_Delete]'

	EXEC ('CREATE PROCEDURE [dbo].[User_Delete] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[User_Delete]'
GO

ALTER PROCEDURE [dbo].[User_Delete]
(
	@id UNIQUEIDENTIFIER
)
AS
BEGIN

	delete
	from Users
	where Id = @id

END
GO