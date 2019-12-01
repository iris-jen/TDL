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




        private async void ButtonGoToMood_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Mood> passinList = new List<Mood>();

                var startDate = DatePickerStartDate.Date;
                var endDate = DatePickerEndDate.Date;
                var endDateSelected = CheckboxEndDate.IsChecked;


                if (startDate > endDate || endDate < startDate)
                    DisplayAlert("Your start date cant be after your end date or vice versa");
                else if (startDate < endDate)
                {
                    var moodRet = EvilStores.MoodStores.GetItemsAsync().Result;
                    

                    foreach (Mood m in moodRet)
                    {
                        if (m.RegisteredTime > startDate && m.RegisteredTime < endDate)
                        {
                            passinList.Add(m);
                        }
                    }
                    passinList.OrderByDescending(x => x.RegisteredTime);
                    await Navigation.PushAsync(new MoodChartsPage(passinList));
                }
            }
            catch(Exception ex)
            {
                DisplayException(ex);
            }
        }

        public void DisplayAlert(string message)
        {
            DisplayAlert("Uh oh :(",message, "Ok.");
        }
        public void DisplayException(Exception ex)
        {
            DisplayAlert("Exception", $"Exception: {ex}{Environment.NewLine} Trace: {Environment.StackTrace}", "Ok :(");
        }
    }


}