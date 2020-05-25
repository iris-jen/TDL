using System;
using SelfMonitoringApp.LogModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using SelfMonitoringApp.Services;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SleepLoggerPage : ContentPage
    {
        private Sleep _sleep = new Sleep();

        public SleepLoggerPage()
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
            if (TimePickerSleep.IsSet(TimePicker.TimeProperty))
                _sleep.SleepStart = TimePickerSleep.Time;
            else
            {
                await DisplayAlert("Sleep time is not set", "Please set the time you went to sleep", "Ok");
                return;
            }

            if (TimePickerWake.IsSet(TimePicker.TimeProperty))
                _sleep.SleepEnd = TimePickerWake.Time;
            else
            {
                await DisplayAlert("Wake time is not set", "Please set the time you woke up", "Ok");
                return;
            }

            await StoreService.ItemStores.SleepStores.AddItemAsync(_sleep);
            await Navigation.PopAsync();
        }

    }
}