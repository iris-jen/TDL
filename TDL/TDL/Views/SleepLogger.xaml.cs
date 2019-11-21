using System;
using SelfMonitoringApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SleepLoggerPage : ContentPage
    {
        private Sleep _sleep = new Sleep();

        public SleepLoggerPage(Sleep sleep)
        {
            InitializeComponent();
            BindingContext = _sleep;
        }

        
        private async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                
            }
        }

        private async void ButtonSave_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}