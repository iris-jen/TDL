using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SelfMonitoringApp.LogModels
{
    public class Sleep : ILogModel
    {
        public TimeSpan SleepStart     { get; set; } = TimeSpan.MinValue;

        public TimeSpan SleepEnd       { get; set; } = TimeSpan.MinValue;

        public DateTime RegisteredTime { get; set; } = DateTime.MinValue;

        public string DreamLog         { get; set; } = string.Empty;

        public double RestRating       { get; set; } 

        public bool SleepSet           { get; set; }

        public bool WakeSet            { get; set; }

        public bool RememberedDream    { get; set; }

        public bool VividDream         { get; set; }

        public bool Nightmare          { get; set; }
    }
}
