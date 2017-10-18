IF OBJECT_ID(N'[dbo].[BookAuthorConnection_Delete]', N'P') IS NULL
BEGIN
        PRINT 'Creating empty procedure [dbo].[BookAuthorConnection_Delete]'

        EXEC ('CREATE PROCEDURE [dbo].[BookAuthorConnection_Delete] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[BookAuthorConnection_Delete]'
GO

ALTER PROCEDURE [dbo].[BookAuthorConnection_Delete]
(
        @bookId UNIQUEIDENTIFIER
        ,@authorId UNIQUEIDENTIFIER
)
AS
BEGIN

delete
from [dbo].[BookAuthorConnection]
where
(
        [BookId] = @bookId and
        [AuthorId] = @authorId
)

END

GO