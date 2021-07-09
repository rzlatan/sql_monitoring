using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.SmartScalingML
{
    public class SmartScalingCpu
    {
        [LoadColumn(0)]
        public int Day;

        [LoadColumn(1)]
        public int Hour;

        [LoadColumn(2)]
        public int Minute;

        [LoadColumn(3)]
        public double CPU;
    }

    public class SmartScalingCpuPrediction
    {
        [ColumnName("CPU")]
        public double CPU;
    }

    public class SmartScalingMemory
    {
        [LoadColumn(0)]
        public int Day;

        [LoadColumn(1)]
        public int Hour;

        [LoadColumn(2)]
        public int Minute;

        [LoadColumn(3)]
        public double Memory;
    }

    public class SmartScalingMemoryPrediction
    {
        [ColumnName("Memory")]
        public double Memory;
    }

    public class SmartScalingNetwork
    {
        [LoadColumn(0)]
        public int Day;

        [LoadColumn(1)]
        public int Hour;

        [LoadColumn(2)]
        public int Minute;

        [LoadColumn(3)]
        public double Network;
    }

    public class SmartScalingNetworkPrediction
    {
        [ColumnName("Network")]
        public double Network;
    }
}