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

            var cl = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 40);
            ButtonGoToFood.ImageSource = ImageSource.FromFile("KnifeFork.png");
            ButtonGoToFood.ContentLayout = cl;

            ButtonGoToSleep.ImageSource = ImageSource.FromFile("Bed.png");
            ButtonGoToSleep.ContentLayout = cl;

            

            var eye = ImageSource.FromFile("eye.png");
            ButtonViewFoodData.ImageSource = eye;
            ButtonViewMoodData.ImageSource = eye;
            ButtonViewSubstanceData.ImageSource = eye;
            ButtonViewSleepData.ImageSource = eye;

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
                await ItemStores.SleepStores.AddItemAsync(sleepHolder);
                await ItemStores.SaveObject(ObjectNames.Sleep);
                sleepHolder = new Sleep();
            }
        }

        async void ButtonViewMoodData_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewMoodDataSetup());
        }
    }
}
