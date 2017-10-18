IF OBJECT_ID(N'[dbo].[Book_GetAllWishlist]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[Book_GetAllWishlist]'
    EXEC('CREATE PROCEDURE [dbo].[Book_GetAllWishlist] AS SET NOCOUNT ON;')
END
GO

PRINT 'Altering procedure [dbo].[Book_GetAllWishlist]'
GO

ALTER PROCEDURE [dbo].[Book_GetAllWishlist]
(
	@owner nvarchar(200)
)
AS
BEGIN

SELECT b.[BookId]					AS BookId
	,b.[Year] 						AS Year
	,b.[Title] 						AS Title
	,b.[Owner] 						AS Owner
	,b.[Status] 					AS Status
	,b.[PageCount] 					AS PageCount
	,b.[Bookshelf] 					AS Bookshelf
	,b.[LiveLibUrl] 				AS LiveLibUrl
	,b.[LibRusEcUrl] 				AS LibRusEcUrl
	,b.[FlibustaUrl]				AS FlibustaUrl
	,b.[IsSynchronized] 			AS IsSynchronized
	,b.[AdditionalInfo] 			AS AdditionalInfo
	,b.[MustInformAboutWarnings] 	AS MustInformAboutWarnings
FROM [dbo].[Books] b
where b.[Status] = 0 and
	b.[Owner] = @owner



SELECT ba.[BookId]					AS BookId
	,a.[AuthorId] 					AS AuthorId
	,a.[FamilyName] 				AS FamilyName
	,a.[FullMiddleName] 			AS FullMiddleName
	,a.[GivenName] 					AS GivenName
	,a.[PseudonymFamilyName] 		AS PseudonymFamilyName
	,a.[PseudonymFullMiddleName] 	AS PseudonymFullMiddleName
	,a.[PseudonymGivenName] 		AS PseudonymGivenName
	,a.[Shortcut] 					AS Shortcut
	,a.[IsSynchronized] 			AS IsSynchronized
FROM [dbo].[Authors] a
	INNER JOIN [dbo].[BookAuthorConnection] ba
		ON a.[AuthorId] = ba.[AuthorId]
	inner join [dbo].[Books] b
		on ba.[BookId] = b.[BookId]
where b.[Status] = 0 and
	b.[Owner] = @owner


END

GO