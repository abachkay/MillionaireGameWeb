IF DB_ID('MillionaireGameWebDB') IS NULL
	PRINT 'Error. Database not exist';
GO
INSERT INTO [dbo].[UserAnswerLogs] VALUES
(0,2,0,3,1),
(1,0,0,3,0),
(2,0,0,3,0),
(3,3,0,0,0),
(4,0,3,0,1),
(5,3,1,0,0),
(6,0,0,3,0),
(7,0,1,0,3),
(8,1,0,0,3),
(9,3,3,1,0),
(10,0,0,0,0),
(11,0,1,3,0),
(12,1,1,3,0),
(13,2,0,0,3),
(14,1,0,3,0);