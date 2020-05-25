using SelfMonitoringApp.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SelfMonitoringApp.LogModels
{
    public class Mood : ILogModel
    {
        public DateTime RegisteredTime { get; set; } = DateTime.MinValue;

        public string Description      { get; set; } = string.Empty;

        public string Emotion          { get; set; } = string.Empty;

        public double OverallMood      { get; set; }
    }
}
