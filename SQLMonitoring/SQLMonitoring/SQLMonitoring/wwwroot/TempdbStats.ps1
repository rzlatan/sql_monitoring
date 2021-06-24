# Manual steps required for configuring the script:
# Step 1: Enter the server name
$ServerName = Read-Host "Enter the server name"

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
    ##########################################################################################
    # Tempdb Data and Log Size
    $TempdbDataSize = Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT SUM(size)/128 AS [DataSize] FROM tempdb.sys.database_files WHERE type_desc = 'ROWS'"
    $TempdbLogSize =  Invoke-SqlCmd -ServerInstance $ServerName -Query "SELECT SUM(size)/128 AS [LogSize] FROM tempdb.sys.database_files WHERE type_desc = 'LOG'"
    $Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/Report/UploadTempdbDataAndLogSize"
	$Body = @{
	    Timestamp = $Timestamp
	    Server = $ServerName
	    DataSize = $TempdbDataSize.DataSize
        LogSize = $TempdbLogSize.LogSize
    }

    Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body

	Write-Host "Sleeping for one minute"
	Start-Sleep -Seconds 60
}