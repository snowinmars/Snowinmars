IF OBJECT_ID(N'[dbo].[Book_Insert]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[Book_Insert]'
    EXEC('CREATE PROCEDURE [dbo].[Book_Insert] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Book_Insert]'
GO

ALTER PROCEDURE [dbo].[Book_Insert]
(
	@bookId UNIQUEIDENTIFIER
	,@year INT
	,@title NVARCHAR (200)
	,@owner nvarchar(200)
	,@status int
	,@pageCount INT
	,@bookshelf nvarchar(50)
	,@liveLibUrl varchar(200)
	,@libRusEcUrl varchar(200)
	,@flibustaUrl varchar(200)
	,@isSynchronized bit
	,@additionalInfo nvarchar(1000)
)
AS
BEGIN

INSERT INTO [Books] (
	[BookId]
	,[Year]
	,[Title]
	,[Owner]
	,[Status]
	,[PageCount]
	,[Bookshelf]
	,[LiveLibUrl]
	,[LibRusEcUrl]
	,[FlibustaUrl]
	,[IsSynchronized]
	,[AdditionalInfo]
	)
VALUES (
	@bookId
	,@year
	,@title
	,@owner
	,@status
	,@pageCount
	,@bookshelf
	,@liveLibUrl
	,@libRusEcUrl
	,@flibustaUrl
	,@isSynchronized
	,@additionalInfo
	)

END

GO