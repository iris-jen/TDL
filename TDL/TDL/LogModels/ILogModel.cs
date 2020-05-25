using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.LogModels
{
    public interface ILogModel
    {
        DateTime RegisteredTime { get; set; }
    }
}
