using System;

namespace SelfMonitoringApp.LogModels
{
    public class Substance : ILogModel
    {
        public DateTime RegisteredTime  { get; set; } = DateTime.MinValue;

        public string ConsumptionMethod { get; set; } = string.Empty;

        public string SubstanceName     { get; set; } = string.Empty;

        public string Unit              { get; set; } = string.Empty;

        public string Comment           { get; set; } = string.Empty;

        public double Amount            { get; set; }

    }
}
