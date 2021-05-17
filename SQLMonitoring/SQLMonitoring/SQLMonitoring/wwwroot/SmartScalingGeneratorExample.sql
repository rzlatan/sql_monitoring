declare @Id INT = 1
declare @Timestamp DATETIME = DATETIMEFROMPARTS ( 2021, 5, 2, 00, 00, 00, 00 )  
declare @Day int = 0
declare @Hour int = 0
declare @Minute int = 0
declare @Severname nvarchar(max) = 'localhost'
declare @Cpu float = 0
declare @Memory float = 0
declare @Network float = 0
declare @Disk float = 0

-- change
declare @daydate int = 2

while (@Id < 20160)
begin
	set @Minute = (@Minute + 1) % 60
	if (@Minute = 0)
	begin
		set @Hour = (@Hour + 1) % 24
		if (@Hour = 0)
		begin
			set @Day = (@Day + 1) % 7
			set @daydate = @daydate + 1
		end
	end

	print (CONCAT(@Day, '-', @Hour, '-', @Minute))
	
	if (@Day = 6 or @Day = 0)
		begin
			set @Cpu = 3 + RAND() 
			set @Memory = 7 + RAND()
			set @Network = 2 + Rand()
			set @Disk = 2 + Rand()
			print (CONCAT(@Cpu, '-', @Memory, '-', @Network, '-', @Disk))
		end
	else
		begin
			if (@Hour between 0 and 8)
				begin
					set @Cpu = 3 + RAND() * @Hour 
					set @Memory = 7 + RAND() * @Hour
					set @Network = 2 + Rand() * @Hour
					set @Disk = 2 + Rand() *  @Hour
				end
			else if (@Hour between 8 and 16)
				begin
					set @Cpu = 60 + RAND() *  @Hour
					set @Memory = 70 + RAND() *  @Hour
					set @Network = 40 + Rand() *  @Hour
					set @Disk = 2 + Rand() * @Hour
				end
			else if (@Hour between 16 and 17)
				begin
					set @Cpu = 70 + RAND() *  @Hour
					set @Memory = 30 + RAND() *  @Hour
					set @Network = 30 + Rand() *  @Hour
					set @Disk = 2 + Rand() * Rand()
				end
			else
				begin
					set @Cpu = 6 + RAND()  *  @Hour
					set @Memory = 10 + RAND() *  @Hour
					set @Network = 5 + Rand() *  @Hour
					set @Disk = 1 + Rand() *  @Hour
				end
		end

		set @Timestamp = DATETIMEFROMPARTS ( 2021, 5, @daydate, @Hour, @Minute, 00, 00 )  

		insert into dbo.SmartPredictionStats
		([Timestamp], [Day], [Hour], [Minute], [ServerName], [CpuUsage], [MemoryUsage], [NetworkUsage], [DiskLatency])
		values
		(@Timestamp, @Day, @Hour, @Minute, @Severname, @Cpu, @Memory, @Network, @Disk)
			
		set @Id = @Id + 1
end
