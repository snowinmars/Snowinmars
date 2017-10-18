IF OBJECT_ID(N'[dbo].[BookAuthorConnection_GetAll]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[BookAuthorConnection_GetAll]'
    EXEC('CREATE PROCEDURE [dbo].[BookAuthorConnection_GetAll] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[BookAuthorConnection_GetAll]'
GO

ALTER PROCEDURE [dbo].[BookAuthorConnection_GetAll]
AS
BEGIN

select ba.BookId
,ba.AuthorId
from [dbo].[BookAuthorConnection] ba

END

GO