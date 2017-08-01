IF COL_LENGTH(N'Books',N'Status') IS NULL
 BEGIN
	PRINT 'Adding Status column to Books table...'

	alter table Books add [Status] int default 1;

	PRINT 'done'
 END
ELSE
 PRINT 'Status column already exists in Books table. No changes were applyed';