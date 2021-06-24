# Manual steps required for configuring the script:
# Step 1: Enter the server name
$ServerName = Read-Host "Enter the server name"

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
	####################################################################################
	# IOPS
	$ReadIOPSCounter = Get-Counter -Counter "\PhysicalDisk(_total)\Disk Reads/Sec"
	$ReadIOPS = $ReadIOPSCounter.CounterSamples.CookedValue

    $WriteIOPSCounter = Get-Counter -Counter "\PhysicalDisk(_total)\Disk Writes/Sec"
	$WriteIOPS = $WriteIOPSCounter.CounterSamples.CookedValue

    $TotalIOPS = $ReadIOPS + $WriteIOPS

    ####################################################################################
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadIOPS"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		ReadIOPS = $ReadIOPS
        WriteIOPS = $WriteIOPS
        TotalIOPS = $TotalIOPS
	}

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

	
	####################################################################################
	# Throughtput
	$ReadMBpsCounter = Get-Counter -Counter "\PhysicalDisk(_total)\Disk Read Bytes/Sec"
	$ReadMBps = $ReadMBpsCounter.CounterSamples.CookedValue / 1024 / 1024

    $WriteMBpsCounter = Get-Counter -Counter "\PhysicalDisk(_total)\Disk Write Bytes/Sec"
	$WriteMBps = $WriteMBpsCounter.CounterSamples.CookedValue / 1024 / 1024

    $TotalMBps = $ReadMBps + $WriteMBps

    ####################################################################################
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadThroughput"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		ReadMBps = $ReadMBps
        WriteMBps = $WriteMBps
        TotalMBps = $TotalMBps
	}

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

    ####################################################################################
    # Latency
	$ReadLatencyCounter = Get-Counter -Counter "\PhysicalDisk(_total)\Avg. Disk Sec/Read"
	$ReadLatency = $ReadLatencyCounter.CounterSamples.CookedValue

    $WriteLatencyCounter = Get-Counter -Counter "\PhysicalDisk(_total)\Avg. Disk Sec/Write"
	$WriteLatency = $WriteLatencyCounter.CounterSamples.CookedValue

    ####################################################################################
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadLatency"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		WriteLatency = $WriteLatency
        ReadLatency = $ReadLatency
	}

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

    #####################################################################################
    # Top 5 plans by IOs
     $Top5IOPlans = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 5 plan_id, avg_logical_io_reads + avg_logical_io_writes AS avg_logical_ios, avg_logical_io_reads, avg_logical_io_writes FROM sys.query_store_runtime_stats ORDER BY avg_logical_io_reads + avg_logical_io_writes DESC"
	
    foreach ($Plan in $Top5IOPlans)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadTop5IOPlans"
	    $Body = @{
		    Timestamp = $Timestamp
		    Server = $ServerName
		    PlanId= $Plan.plan_id
            LogicalIOs = $Plan.avg_logical_ios
            LogicalReads = $Plan.avg_logical_io_reads
            LogicalWrites = $Plan.avg_logical_io_writes
	    }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}