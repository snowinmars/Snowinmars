IF OBJECT_ID(N'[dbo].[Book_Delete]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[Book_Delete]'
    EXEC('CREATE PROCEDURE [dbo].[Book_Delete] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Book_Delete]'
GO

ALTER PROCEDURE [dbo].[Book_Delete]
(
	@bookId UNIQUEIDENTIFIER
)
AS
BEGIN

delete
from [Books]
where [BookId] = @bookId

END

GO