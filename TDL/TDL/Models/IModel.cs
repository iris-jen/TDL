using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.Models
{
    public interface IModel
    {
        DateTime RegisteredTime { get; set; }
    }
}
