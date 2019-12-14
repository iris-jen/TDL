using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelfMonitoringApp.Models;


namespace SelfMonitoringApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDataSetup : ContentPage
    {
        public ViewDataSetup()
        {
            InitializeComponent();
        }

        public enum DataAgregationMethod
        {
            invalid = -1,
            PadToOneHour = 0,
        }

        private async void ButtonGoToMood_Clicked(object sender, EventArgs e)
        {
            try
            {
                var passinList = GetMoodList();

                var dataParseMethod = DataAgregationMethod.PadToOneHour;

                switch(dataParseMethod)
                {
                    case DataAgregationMethod.PadToOneHour:
                        DateTime mover = passinList.First().RegisteredTime;
                        double initialVal = passinList.First().OverallMood;

                        for (int i = 0; i < 24; i++)
                        {
                            DateTime t = mover;
                            
                            
                            foreach (var mood in passinList)
                            {


                            }
                        }

                        break;
                    default:
                        break;
                }
                
                await Navigation.PushAsync(new MoodChartsPage(passinList));


            }
            catch(Exception ex)
            {
                DisplayException(ex);
            }
        }

        public List<Mood> GetMoodList()
        {
            List<Mood> returnList = null;
            try
            {
                var startDate = DatePickerStartDate.Date;
                var endDate = DatePickerEndDate.Date;

                if (startDate > endDate || endDate < startDate)
                    DisplayAlert("Your start date cant be after your end date or vice versa");
                else if (startDate < endDate)
                {
                    var moodRet = EvilStores.MoodStores.GetItemsAsync().Result;


                    foreach (Mood m in moodRet)
                    {
                        if (m.RegisteredTime > startDate && m.RegisteredTime < endDate)
                            returnList.Add(m);
                    }
                    returnList.OrderByDescending(x => x.RegisteredTime);
                }
            }
            catch(Exception ex)
            {
                DisplayException(ex);
            }
            return returnList;
        }

        private async void ButtonUpdateEntrys_Clicked(object sender, EventArgs e)
        {
            try
            {
                var moodSelection = GetMoodList();
                ListViewMood.ItemsSource = moodSelection;
            }
            catch(Exception ex)
            {
                DisplayException(ex);
            }
        }

        #region Alarm methods
        public void DisplayAlert(string message)
        {
            DisplayAlert("Uh oh :(", message, "Ok.");
        }
        public void DisplayException(Exception ex)
        {
            DisplayAlert("Exception", $"Exception: {ex}{Environment.NewLine} Trace: {Environment.StackTrace}", "Ok :(");
        }
        #endregion
    }
}