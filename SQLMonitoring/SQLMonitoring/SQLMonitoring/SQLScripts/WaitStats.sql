DECLARE @startTime DATETIME
DECLARE @endTime DATETIME

SET @startTime = DATETIMEFROMPARTS (2021, 7, 1, 0, 0 ,0, 0)
SET @endTime = DATETIMEFROMPARTS(2021, 7, 2, 0, 0, 0, 0)

DECLARE @i DATETIME = @startTime
DECLARE @j INT = 0

WHILE (@i < @endTime)
BEGIN
	SELECT @i = DATEADD(mi, 1,@i)	
	INSERT INTO dbo.GlobalWaitStats (Date, ServerName, WaitType, WaitTimeMs, MaxWaitTimeMs, SignalWaitTimeMs)
							VALUES  (@i, 'localhost', 'PAGELATCH_SH', 1000 + @j + ABS(CHECKSUM(NEWID())) % 100, 1500 + @j + ABS(CHECKSUM(NEWID())) % 100, 200 + + @j + ABS(CHECKSUM(NEWID())) % 100),
							        (@i, 'localhost', 'CXPACKET', 800 + @j + ABS(CHECKSUM(NEWID())) % 100, 1300 + @j + ABS(CHECKSUM(NEWID())) % 100, 100 + + @j + ABS(CHECKSUM(NEWID())) % 100),
									(@i, 'localhost', 'NETWORKIO', 900 + @j + ABS(CHECKSUM(NEWID())) % 100, 1400 + @j + ABS(CHECKSUM(NEWID())) % 100, 150 + + @j + ABS(CHECKSUM(NEWID())) % 100)
	SET @j = @j + 100
END