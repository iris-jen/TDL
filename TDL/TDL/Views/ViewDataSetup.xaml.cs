using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Microcharts;

namespace SelfMonitoringApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMoodDataSetup : ContentPage
    {

        public MoodsViewModel moodViewModel;
             
        public ViewMoodDataSetup()
        {
            InitializeComponent();
            moodViewModel = new MoodsViewModel(this);
            moodViewModel.DateSetEvent += Vm_DateSetEvent;
            BindingContext = moodViewModel;
        }

        private async void Vm_DateSetEvent(object sender, EventArgs e)
        {
            var startDate = DatePickerStartDate.Date;
            var endDate = DatePickerEndDate.Date;
            var moods = await moodViewModel.GetFilteredMoods(startDate, endDate);
            GenerateChart(moods);
        }


        private SKColor GetColorFromRating(double mood)
        {

            int max = 10;
            int min = 0;
            int mid = max / 2;

            double idealTol = 1;
            double okayTol = 2.5;
            double badTol = 3;

            double deltaFromMiddle = Math.Abs(mood - mid);

            if (deltaFromMiddle >= badTol)
                return SKColors.Red;
            else if (deltaFromMiddle <= okayTol && deltaFromMiddle >= idealTol)
                return SKColors.Orange;
            else
                return SKColors.Green;


        }

        public void GenerateChart(List<Mood> moodList)
        {
            try
            {
                List<Entry> entries = new List<Entry>();

                foreach (Mood m in moodList)
                {
                    entries.Add(new Entry((float)m.OverallMood)
                    {
                        ValueLabel = m.OverallMood.ToString("0.0"),
                        Color = GetColorFromRating(m.OverallMood),
                        Label = m.Emotion,
                        TextColor = SKColors.Black,

                    });
                }

                MoodChart.Chart = new LineChart() { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Black, PointMode = PointMode.Square, PointSize = 30 };
                MoodChart.Chart.MaxValue = 10;
                MoodChart.Chart.MinValue = 0;

            }
            catch (Exception ex)
            {
                DisplayException(ex.ToString());
            }
        }




        public void DisplayException(string message)
        {
            DisplayAlert("Exception", message + "...Trace: " + Environment.StackTrace, "Ok :(");
        }


        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var viewModel = BindingContext as MoodsViewModel;
            var mood = e.Item as MoodViewModel;
            viewModel.ToggleExpandedItem(mood);
        }


    }


}