# Manual steps required for configuring the script:
# Step 1: Enter the server name
$ServerName = Read-Host "Enter the server name"

# Infinite loop for collecting statistics
# and uploading them to the server for analysis
while ($true)
{
	####################################################################################
	# Processor usage counters
	$ProcessorCounterSample = Get-Counter -Counter "\Processor(_Total)\% Processor Time"
	$ProcessorUsage = $ProcessorCounterSample.CounterSamples.CookedValue
	
	####################################################################################
	# Memory usage counters
	$TotalMemory = (Get-CimInstance Win32_PhysicalMemory | Measure-Object -Property capacity -Sum).sum /1mb
	$FreeMemoryCounter = Get-CIMInstance Win32_OperatingSystem | Select FreePhysicalMemory
	$AvailableMemoryMb = $FreeMemoryCounter.FreePhysicalMemory / 1024
	$MemoryUsage = 100.0 * $AvailableMemoryMb / $TotalMemory
	
	####################################################################################
	# Network usage counters
	$NetworkInterfaces = Get-WmiObject -class Win32_PerfFormattedData_Tcpip_NetworkInterface | select BytesTotalPersec, CurrentBandwidth, PacketsPersec|where {$_.PacketsPersec -gt 0}

    foreach ($Interface in $NetworkInterfaces) {
      $BitsPerSec = $Interface.BytesTotalPersec * 8
      $TotalBits = $Interface.CurrentBandwidth

      # Exclude Nulls (any WMI failures)
      if ($TotalBits -gt 0) {
         $Result = (($BitsPerSec / $TotalBits) * 100)
         $TotalBandwidth = $TotalBandwidth + $Result
         $Count++
      }
    }
	
	$NetworkUsage = $TotalBandwidth / $Count
	
	####################################################################################
	# Disk latencies
	$DiskLatencyCounterSample = Get-Counter -Counter "\PhysicalDisk(_Total)\% Disk Time"
	$DiskLatencies = $DiskLatencyCounterSample.CounterSamples.CookedValue
	
	#Upload data on the server 
	$Timestamp = Get-Date -Format "dd/MM/yyyy HH:mm"
	$Url = "https://localhost:44347/SmartScaling/UploadStats"
	$Body = @{
		Timestamp = $Timestamp
		Server = $ServerName
		Cpu = $ProcessorUsage
		Memory = $MemoryUsage
		Network = $NetworkUsage
		Disk = $DiskLatencies
	}
		
	Write-Host "Current stats: $Body" 
	Invoke-RestMethod -Method "Post" -Uri $Url -Body $Body
	
	Write-Host "Sleeping for 30 seconds"
	Start-Sleep -Seconds 30
}