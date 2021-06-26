$ServerName=$args[0]

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
	####################################################################################
	# Server-Level wait stats
    $Waits = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 5 wait_type, wait_time_ms, max_wait_time_ms, signal_wait_time_ms  FROM sys.dm_os_wait_stats ORDER BY wait_time_ms DESC"

    foreach ($Wait in $Waits)
    {
        # Uploading global wait stats
        #
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/GlobalWaitStats"
	    $Body = @{
		    Timestamp = $Timestamp
            Server = $ServerName
		    WaitType = $Wait.wait_type
		    WaitTimeMs = $Wait.wait_time_ms
		    MaxWaitTimeMs = $Wait.max_wait_time_ms
		    SignalWaitTimeMs = $Wait.signal_wait_time_ms
	    }
        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }


    #####################################################################################
    # Server-Level spinlock-stats
    $Spinlocks = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 5 name, collisions, backoffs FROM sys.dm_os_spinlock_stats ORDER BY collisions DESC"
    foreach ($Spinlock in $Spinlocks)
    {
        # Uploading global wait stats
        #
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	    $Url = "https://localhost:44347/Report/GlobalSpinlockStats"
	    $Body = @{
		    Timestamp = $Timestamp
            Server = $ServerName
		    Spinlock = $Spinlock.name
		    Collisions = $Spinlock.collisions
		    Backoffs = $Spinlock.backoffs
	    }
        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}