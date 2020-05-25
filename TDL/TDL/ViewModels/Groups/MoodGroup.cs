using SelfMonitoringApp.LogModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Groups
{
    public class MoodGroup : ObservableCollection<MoodViewModel>
    {
        public string HeaderText { get; set; }

        public ObservableCollection<MoodViewModel> Moods => this;
    }
}
