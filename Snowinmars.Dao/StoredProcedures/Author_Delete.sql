IF OBJECT_ID(N'[dbo].[Author_Delete]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[Author_Delete]'

	EXEC ('CREATE PROCEDURE [dbo].[Author_Delete] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Author_Delete]'
GO

ALTER PROCEDURE [dbo].[Author_Delete]
(
	@authorId UNIQUEIDENTIFIER
)
AS
BEGIN

	DELETE
	FROM [Authors]
	WHERE (AuthorId = @authorId)

END
GO