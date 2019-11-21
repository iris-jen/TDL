using System;

namespace SelfMonitoringApp.Models
{
    public class Schedule : ModelBase
    {
        public DateTime TargetTime { get; set; }
        public DateTime EndTargetTime { get; set; }
        public DateTime CompletionTime { get; set; }
    }
}
