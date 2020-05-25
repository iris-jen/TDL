using SelfMonitoringApp.LogModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace SelfMonitoringApp.Services
{
    public abstract class ILogStore<T> : List<ILogModel>
    {

    }
}
