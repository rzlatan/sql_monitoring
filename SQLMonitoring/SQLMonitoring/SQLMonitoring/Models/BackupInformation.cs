using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Model
{
    public enum BackupInformationType
    {
        LastBackups,
        LastRestorablePoint,
        DatabasesWithoutBackup
    }

    public class BackupInformation
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string ServerName { get; set; }

        public BackupInformationType Type { get; set; }

        public string? DatabaseName { get; set; }

        public DateTime? BackupStartTime { get; set; }

        public DateTime? BackupEndTime { get; set; }

        public long? BackupDuration { get; set; }

        public string? BackupType { get; set; }

        public long? BackupSize { get; set; }

        public string? BackupLocation { get; set; }

        public DateTime? LastRestorablePoint { get; set; }

        public long? HoursSinceLastBackup { get; set; }
    }
}
