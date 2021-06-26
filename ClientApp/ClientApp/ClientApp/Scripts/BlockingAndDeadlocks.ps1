$ServerName=$args[0]

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
    #####################################################################################
    # Top 5 long active transactions
    $Top5LongActiveTransactions = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT [transaction_id] as TransactionId, CONVERT(varchar(50), [name], 1) as [Name], [transaction_begin_time] as [BeginTime], DATEDIFF(minute, [transaction_begin_time], GETDATE()) AS [DurationMin], [transaction_state] as [State] FROM sys.dm_tran_active_transactions"
	
    foreach ($Transaction in $Top5LongActiveTransactions)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadTop5ActiveTransactions"
	    $Body = @{
		    Timestamp = $Timestamp
		    Server = $ServerName
		    TransactionId= $Transaction.TransactionId
            Name = $Transaction.Name
            BeginTime = $Transaction.BeginTime
            DurationMin = $Transaction.DurationMin
            State = $Transaction.State
	    }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

    #######################################################################################
    # Blocked processes
    $BlockedProcesses = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT [spid] as [ProcessId], [blocked] as [Blocked], [status] as [Status], [waittime] as [WaitTime], [waitresource] as [WaitResource], [dbid] as [DatabaseId] FROM sys.sysprocesses WHERE blocked > 0"

    foreach ($BlockedProcess in $BlockedProcesses)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/UploadBlockingSessions"
	    $Body = @{
		    Timestamp = $Timestamp
		    Server = $ServerName
		    ProcessId= $BlockedProcesses.ProcessId
            Blocked = $BlockedProcesses.Blocked
            Status = $BlockedProcesses.Status
            WaitTime = $BlockedProcesses.WaitTime
            WaitResource = $BlockedProcesses.WaitResource
            DatabaseId = $BlockedProcess.DatabaseId
	    }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

    ##########################################################################################
    # Deadlocks through time
    $DeadlocksThroughTime = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT cntr_value AS TotalDeadlocks FROM sys.dm_os_performance_counters WHERE counter_name = 'Number of Deadlocks/sec' AND instance_name = '_Total'"

    $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadTotalDeadlocks"
	$Body = @{
	    Timestamp = $Timestamp
	    Server = $ServerName
	    TotalDeadlocks = $DeadlocksThroughTime.TotalDeadlocks
    }

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}