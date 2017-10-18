ALTER TABLE [dbo].[Books] ALTER COLUMN [AdditionalInfo] nvarchar(1000) NULL
IF COL_LENGTH('[dbo].[Books]', '[AuthorShortcuts]') IS NOT NULL
	begin
		print 'Deleting [AuthorShortcuts] column from [dbo].[Books] table'
		ALTER TABLE [dbo].[Books] DROP COLUMN [AuthorShortcuts]
	end
ELSE
	begin
		print 'Skipping deleting [AuthorShortcuts] column from [dbo].[Books] table'
	end