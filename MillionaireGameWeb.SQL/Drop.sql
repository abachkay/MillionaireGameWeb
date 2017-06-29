IF DB_ID('MillionaireGameWebDB') IS NULL
	PRINT 'Error. Database not exist';
GO
DROP DATABASE MillionaireGameWebDB;