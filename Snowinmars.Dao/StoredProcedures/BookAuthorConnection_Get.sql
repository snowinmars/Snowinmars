IF OBJECT_ID(N'[dbo].[BookAuthorConnection_Get]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[BookAuthorConnection_Get]'
    EXEC('CREATE PROCEDURE [dbo].[BookAuthorConnection_Get] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[BookAuthorConnection_Get]'
GO

ALTER PROCEDURE [dbo].[BookAuthorConnection_Get]
(
	@bookId UNIQUEIDENTIFIER
)
AS
BEGIN

select ba.BookId
	,ba.AuthorId
from [dbo].[BookAuthorConnection] ba
where ba.BookId = @bookId

END

GO