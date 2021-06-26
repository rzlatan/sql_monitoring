# Manual steps required for configuring the script:
# Step 1: Enter the server name
$ServerName = Read-Host "Enter the server name"

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
    ##########################################################################################
    # Log stats
    $LogStats = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT DB_NAME() AS DbName, name AS FileName, type_desc, size/128.0 AS CurrentSizeMB, growth as Growth, size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS INT)/128.0 AS FreeSpaceMB FROM sys.database_files WHERE type_desc = 'LOG'"
    
    foreach ($LogStat in $LogStats)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
        $Url = "https://localhost:44347/Report/UploadLogFullProblem"
        $Body = @{
            Timestamp = $Timestamp
            Server = $ServerName
            DbName = $LogStat.DbName
            FileName = $LogStat.FileName
            CurrentSizeMB = $LogStat.CurrentSizeMB
            Growth = $LogStat.Growth
            FreeSpaceMB = $LogStat.FreeSpaceMB
        }

        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

    ##########################################################################################
    # Long Transactions
    $LongTransaction = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT TOP 1 [transaction_id] as TransactionId, CONVERT(varchar(50), [name], 1) as [Name], [transaction_begin_time] as [BeginTime], DATEDIFF(minute, [transaction_begin_time], GETDATE()) AS [DurationMin], [transaction_state] as [State] FROM sys.dm_tran_active_transactions ORDER BY DurationMin DESC"

    $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
    $Url = "https://localhost:44347/Report/UploadLongTransactionProblem"
    $Body = @{
        Timestamp = $Timestamp
        Server = $ServerName
        TransactionId = $LongTransaction.DbTransactionIdName
        Name = $LongTransaction.Name
        BeginTime = $LongTransaction.BeginTime
        Duration = $LongTransaction.Duration
        State = $LongTransaction.State
    }

    ########################################################################################
    # Backup missing in last 24h 
    $MissingBackups = Invoke-SqlCmd -ServerInstance $ServerName -Query @"
               SELECT 
               msdb.dbo.backupset.database_name, 
               MAX(msdb.dbo.backupset.backup_finish_date) AS last_db_backup_date, 
               DATEDIFF(hh, MAX(msdb.dbo.backupset.backup_finish_date), GETDATE()) AS [HoursSinceLastBackup] 
               FROM    msdb.dbo.backupset 
               WHERE     msdb.dbo.backupset.type = 'D'  
               GROUP BY msdb.dbo.backupset.database_name 
               HAVING  (MAX(msdb.dbo.backupset.backup_finish_date) < DATEADD(hh, - 24, GETDATE()))  
               UNION  
               --Databases without any backup history 
                SELECT      
                    master.dbo.sysdatabases.NAME AS database_name,  
                    NULL AS [Last Data Backup Date],  
                    9999 AS [HoursSinceLastBackup]  
                FROM 
                    master.dbo.sysdatabases LEFT JOIN msdb.dbo.backupset 
                    ON master.dbo.sysdatabases.name  = msdb.dbo.backupset.database_name 
                WHERE msdb.dbo.backupset.database_name IS NULL AND master.dbo.sysdatabases.name <> 'tempdb' 
                ORDER BY  
                msdb.dbo.backupset.database_name
"@

    foreach ($MissingBackup in $MissingBackups)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
        $Url = "https://localhost:44347/Report/UploadMissingBackupProblem"
        $Body = @{
            Timestamp = $Timestamp
            Server = $ServerName
            DatabaseName = $MissingBackup.database_name
            HoursSinceLastBackup = $MissingBackup.HoursSinceLastBackup
        }
        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

    ###########################################################################################
    # Missing indexes
    $MissingIndexes = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT database_id, equality_columns, inequality_columns FROM sys.dm_db_missing_index_details"

    foreach ($MissingIndex in $MissingIndexes)
    {
        $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
        $Url = "https://localhost:44347/Report/UploadMissingIndexes"
        $Body = @{
            Timestamp = $Timestamp
            Server = $ServerName
            DatabaseId = $MissingIndex.database_id
            EqualityColumns = $MissingIndex.equality_columns
            InequalityColumns = $MissingIndex.inequality_columns
        }
        
        Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
    }

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}