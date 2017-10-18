IF OBJECT_ID(N'[dbo].[BookAuthorConnection_Insert]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[BookAuthorConnection_Insert]'
    EXEC('CREATE PROCEDURE [dbo].[BookAuthorConnection_Insert] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[BookAuthorConnection_Insert]'
GO

ALTER PROCEDURE [dbo].[BookAuthorConnection_Insert]
(
	@bookId UNIQUEIDENTIFIER,
	@authorId UNIQUEIDENTIFIER
)
AS
BEGIN

INSERT INTO [BookAuthorConnection] (
	BookId
	,AuthorId
	)
VALUES (
	@bookId
	,@authorId
	)

END