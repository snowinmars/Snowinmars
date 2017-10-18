IF OBJECT_ID(N'[dbo].[Book_Update]', N'P') IS NULL
BEGIN
	PRINT 'Creating empty procedure [dbo].[Book_Update]'

	EXEC ('CREATE PROCEDURE [dbo].[Book_Update] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Book_Update]'
GO

ALTER PROCEDURE [dbo].[Book_Update]
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


	UPDATE [Books]
	SET [Title] = @title
		,[Year] = @year
		,[Owner] = @owner
		,[Status] = @status
		,[PageCount] = @pageCount
		,[Bookshelf] = @bookshelf
		,[LiveLibUrl] = @liveLibUrl
		,[LibRusEcUrl] = @libRusEcUrl
		,[FlibustaUrl] = @flibustaUrl
		,[IsSynchronized] = @isSynchronized
		,[AdditionalInfo] = @additionalInfo
	WHERE (BookId = @bookId)


END

GO