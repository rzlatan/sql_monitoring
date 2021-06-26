$ServerName=$args[0]

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
	####################################################################################
	# Total memory and target memory usage
	$TotalMemoryCounter = Get-Counter -Counter "\SQLServer:Memory Manager\Total Server Memory (KB)"
	$TotalMemory = $TotalMemoryCounter.CounterSamples.CookedValue

    $TargetMemoryCounter = Get-Counter -Counter "\SQLServer:Memory Manager\Target Server Memory (KB)"
    $TargetMemory = $TargetMemoryCounter.CounterSamples.CookedValue

    ####################################################################################
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadTotalAndTargetMemory"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		TotalMemory = $TotalMemory
        TargetMemory = $TargetMemory
	}

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

	
	####################################################################################
	# Buffer hit ratio
	$BufferCacheHitRatioCounter = Get-Counter -Counter "\SQLServer:Buffer Manager\Buffer Cache Hit Ratio"
	$BufferCacheHitRatio = $BufferCacheHitRatioCounter.CounterSamples.CookedValue

    ####################################################################################
    # Page Life Expectancy
    $PageLifeExpectancyCounter = Get-Counter -Counter "\SQLServer:Buffer Manager\Page life expectancy"
    $PageLifeExpectancy = $PageLifeExpectancyCounter.CounterSamples.CookedValue

    ####################################################################################
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadBufferHitRatioAndPageLifeExpectancy"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		BufferCacheHitRatio = $BufferCacheHitRatio
        PageLifeExpectancy = $PageLifeExpectancy
	}

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

    #####################################################################################
    # Top 5 memory clerks 
     $Top5MemoryClerks = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 5 [type] as [ClerkType], SUM(pages_kb) / 1024 AS [SizeMb] FROM sys.dm_os_memory_clerks GROUP by [type] ORDER BY [SizeMb] DESC"
	
    foreach ($Clerk in $Top5MemoryClerks)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadTop5MemoryClerks"
	    $Body = @{
		    Timestamp = $Timestamp
		    Server = $ServerName
		    MemoryClerk = $Clerk.ClerkType
            SizeMb = $Clerk.SizeMb
	    }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}