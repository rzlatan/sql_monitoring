DECLARE @startTime DATETIME
DECLARE @endTime DATETIME

SET @startTime = DATETIMEFROMPARTS (2021, 7, 1, 0, 0 ,0, 0)
SET @endTime = DATETIMEFROMPARTS(2021, 7, 2, 0, 0, 0, 0)

DECLARE @i DATETIME = @startTime
DECLARE @j INT = 0

WHILE (@i < @endTime)
BEGIN
	SELECT @i = DATEADD(mi, 1,@i)	
	INSERT INTO dbo.GlobalSpinlockStats(Date, Server, Spinlock, Collisions, Backoffs)
							VALUES  (@i, 'localhost', 'X_PACKET_LIST' , 1500 + @j + ABS(CHECKSUM(NEWID())) % 100, 200 + @j + ABS(CHECKSUM(NEWID())) % 100),
							        (@i, 'localhost', 'SOS_SUSPEND_QUEUE', 1200 + @j + ABS(CHECKSUM(NEWID())) % 100, 100 + @j + ABS(CHECKSUM(NEWID())) % 100),
									(@i, 'localhost', 'BUF_FREE_LIST', 600 + @j + ABS(CHECKSUM(NEWID())) % 100, 100 + @j + ABS(CHECKSUM(NEWID())) % 100)
	SET @j = @j + 100
END