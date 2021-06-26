# Manual steps required for configuring the script:
# Step 1: Enter the server name
$ServerName = Read-Host "Enter the server name"

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
    ##########################################################################################
    # Top 5 queries by CPU consumption
    $Top5QueriesCPU = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP(5) CONVERT(nvarchar(20), query_hash, 1) as query_hash, creation_time, last_execution_time, (total_worker_time+0.0)/1000 AS total_worker_time, (total_worker_time+0.0)/(execution_count*1000) AS [AvgCPUTime] , execution_count FROM sys.dm_exec_query_stats qs CROSS APPLY sys.dm_exec_sql_text(sql_handle) st WHERE total_worker_time > 0 ORDER BY total_worker_time DESC"
    
    foreach ($Query in $Top5QueriesCPU)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadTop5QueriesByCpuConsumption"
	    $Body = @{
	        Timestamp = $Timestamp
	        Server = $ServerName
	        QueryHash = $Query.query_hash
            LastExecTime = $Query.last_execution_time
            TotalWorkerTime = $Query.total_worker_time
            AvgCpuTime = $Query.AvgCPUTime
            ExecCount = $Query.execution_count
        }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

    ##########################################################################################
    # Top 5 queries by IO consumption
    $Top5QueriesIO = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 5 CONVERT(nvarchar(20), query_hash, 1) as query_hash, last_execution_time, total_logical_reads, total_logical_writes, execution_count , (total_logical_reads+total_logical_writes)/(execution_count+0.0) AS [avg_ios_per_execution]  FROM sys.dm_exec_query_stats qs CROSS APPLY sys.dm_exec_sql_text(sql_handle) st WHERE total_logical_reads+total_logical_writes > 0 AND sql_handle IS NOT NULL ORDER BY [avg_ios_per_execution] DESC"

    foreach ($Query in $Top5QueriesIO)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadTop5QueriesByIOConsumption"
	    $Body = @{
	        Timestamp = $Timestamp
	        Server = $ServerName
	        QueryHash = $Query.query_hash
            LastExecTime = $Query.last_execution_time
            TotalLogicalReads = $Query.total_logical_reads
            TotalLogicalWrites = $Query.total_logical_writes
            ExecCount = $Query.execution_count
            IOsPerExecution = $Query.avg_ios_per_execution
        }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}