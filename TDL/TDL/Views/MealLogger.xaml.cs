using System;
using System.Linq;
using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Singular;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealLoggerPage : ContentPage
    {
        public MealLoggerPage()
        {
            InitializeComponent();
            BindingContext = new MealViewModel(this);
        }
    }
}