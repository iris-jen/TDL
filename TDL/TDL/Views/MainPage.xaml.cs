using System;
using System.ComponentModel;
using SelfMonitoringApp.Models;
using Xamarin.Forms;

namespace SelfMonitoringApp.Views
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private Sleep sleepHolder = new Sleep();

        public MainPage()
        {
            InitializeComponent();
        }
        
        async void Goto_Mood_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoodLoggerPage());
        }

        async void ButtonGoToFood_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MealLoggerPage());
        }

        async void ButtonGoToSubstance_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubstanceLoggerPage());
        }

        async void ButtonGoToSleep_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SleepLoggerPage(sleepHolder));

            if (sleepHolder.SleepSet && sleepHolder.WakeSet)
            {
                await EvilStores.SleepStores.AddItemAsync(sleepHolder);
                await EvilStores.SaveObject(ObjectNames.Sleep);
                sleepHolder = new Sleep();
            }
        }

        async void ButtonViewData_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoodChartsPage());
        }
    }
}
