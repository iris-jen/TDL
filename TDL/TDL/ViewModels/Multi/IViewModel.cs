using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.ViewModels.Multi
{
    public interface IViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
