using System;
using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelfMonitoringApp.Controls;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodLoggerPage : ContentPage
    {
        public MoodLoggerPage()
        {
            InitializeComponent();
            BindingContext = new MoodViewModel(this);
        }   

        public MoodLoggerPage(MoodViewModel model)
        {
            InitializeComponent();
            model.Page = this;
            BindingContext = model;
        }
    }
}