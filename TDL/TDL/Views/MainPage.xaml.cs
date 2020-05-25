using System;
using System.ComponentModel;
using SelfMonitoringApp.LogModels;
using Xamarin.Forms;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace SelfMonitoringApp.Views
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {   
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new NavigationViewModel(this);
        }


    }
}
