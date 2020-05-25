using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Singular
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(Page p) : base(p)
        {
            AddMoodLogsCommand = new Command(
                execute: async ()=>
                {
                    await StoreService.ItemStores.MoodStores.AddRandomLogs();
                } );
        }

        public Command AddMoodLogsCommand { get; private set; }

    }
}
