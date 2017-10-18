IF OBJECT_ID(N'[dbo].[Book_GetUnsynchronized]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[Book_GetUnsynchronized]'
    EXEC('CREATE PROCEDURE [dbo].[Book_GetUnsynchronized] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Book_GetUnsynchronized]'
GO

ALTER PROCEDURE [dbo].[Book_GetUnsynchronized]
AS
BEGIN

select b.BookId
from [Books] b
where b.IsSynchronized = 0

END

GO