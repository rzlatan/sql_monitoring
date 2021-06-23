# Manual steps required for configuring the script:
# Step 1: Enter the server name
$ServerName = Read-Host "Enter the server name"

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
	####################################################################################
	# Processor usage counters
	$CPUTotalTimeCounter = Get-Counter -Counter "\Processor(_Total)\% Processor Time"
	$CPUTotalTime = $CPUTotalTimeCounter.CounterSamples.CookedValue

    $CPUUserTimeCounter = Get-Counter -Counter "\Processor(_Total)\% User Time"
    $CPUUserTime = $CPUUserTimeCounter.CounterSamples.CookedValue

    ####################################################################################
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadCpuTotalAndUserTime"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		TotalCpu = $CPUTotalTime
        UserCpu = $CPUUserTime
	}

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

	
    ####################################################################################
    # Query Execution and Compile time 
    #
    $QueryCpu = Invoke-SqlCmd -ServerInstance $ServerName -Query "select sum(total_worker_time) as QueryExecTime, sum(total_elapsed_time - total_worker_time) AS QueryCompileTime  from sys.dm_exec_query_stats"
    $QueryExecTime = $QueryCpu.QueryExecTime
    $QueryCompileTime = $QueryCpu.QueryCompileTime

    ####################################################################################
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadCompileAndExecTime"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		QueryExecTime = $QueryExecTime
        QueryCompileTime = $QueryCompileTime
	}

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

    #####################################################################################
    # Top 5 queries by CPU
    #
    $Top5CPUConsumingQueries = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 5 CONVERT(varchar(20), query_hash, 1) as query_hash, total_elapsed_time FROM sys.dm_exec_query_stats ORDER BY total_elapsed_time desc"
	
    foreach ($Query in $Top5CPUConsumingQueries)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadTop5CPUQueries"
	    $Body = @{
		    Timestamp = $Timestamp
		    Server = $ServerName
		    QueryHash = $Query.query_hash
            QueryTime = $Query.total_elapsed_time
	    }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

    #####################################################################################
    # CPU usage by workload group
    #
    $Top5CPUWorkloadGroups = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 5 [name], total_cpu_usage_ms FROM sys.dm_resource_governor_workload_groups ORDER BY total_cpu_usage_ms DESC"
	
    foreach ($WorkloadGroup in $Top5CPUWorkloadGroups)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadTop5CPUWorkloadGroups"
	    $Body = @{
		    Timestamp = $Timestamp
		    Server = $ServerName
		    WorkloadGroup = $WorkloadGroup.name
            WorkloadGroupCpuMs = $WorkloadGroup.total_cpu_usage_ms
	    }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}