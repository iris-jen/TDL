using System;
using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubstanceLoggerPage : ContentPage
    {
        public SubstanceLoggerPage()
        {
            InitializeComponent();
            BindingContext = new SubstanceViewModel(this);
        }
    }
}