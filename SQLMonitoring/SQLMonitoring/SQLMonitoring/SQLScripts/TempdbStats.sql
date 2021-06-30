DECLARE @startTime DATETIME
DECLARE @endTime DATETIME

SET @startTime = DATETIMEFROMPARTS (2021, 7, 1, 0, 0 ,0, 0)
SET @endTime = DATETIMEFROMPARTS(2021, 7, 2, 0, 0, 0, 0)

DECLARE @i DATETIME = @startTime
DECLARE @j INT = 0

WHILE (@i < @endTime)
BEGIN
	SELECT @i = DATEADD(mi, 1,@i)
	IF (ABS(CHECKSUM(NEWID())) % 2 = 0)
	BEGIN
		INSERT INTO dbo.GlobalTempdbStats(Date, ServerName, DataSizeMb, LogSizeMb, Type)
							VALUES  (@i, 'localhost', 160 + ABS(CHECKSUM(NEWID())) % 100 , 60 + ABS(CHECKSUM(NEWID())) % 60 , 1)
	END
	ELSE
	BEGIN
			INSERT INTO dbo.GlobalTempdbStats(Date, ServerName, DataSizeMb, LogSizeMb, Type)
							VALUES  (@i, 'localhost', 160 - ABS(CHECKSUM(NEWID())) % 100 , 60 - ABS(CHECKSUM(NEWID())) % 60 , 1)
	END
END