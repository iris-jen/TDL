using System;
using System.Collections.Generic;
using System.Globalization;
using Microcharts;
using SelfMonitoringApp.Models;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodChartsPage : ContentPage
    {

        
        public MoodChartsPage(List<Mood> moodList)
        {
            try
            {
                InitializeComponent();
                GenerateChart(moodList);                
            }
            catch(Exception ex)
            {
                DisplayException(ex.ToString());
            }
        }

        private SKColor GetColorFromRating(double mood)
        {
            
            int max = 10;
            int min = 0;
            int mid = max/2;

            double idealTol = 1;
            double okayTol =2.5;
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
                
                foreach(Mood m in moodList)
                {
                    entries.Add(new Entry((float)m.OverallMood)
                    {
                        ValueLabel = m.OverallMood.ToString(),
                        Color = GetColorFromRating(m.OverallMood),
                        Label = m.Emotion,
                        TextColor = SKColors.Black,
                    }) ;
                    
                }
                

                MoodChart.Chart = new LineChart() { Entries = entries, LabelTextSize = 8, BackgroundColor = SKColors.Black };
            }
            catch (Exception ex)
            {
                DisplayException(ex.ToString());
            }
        }




        
        private async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                DisplayException(ex.ToString());
            }
        }

        private async void ButtonSave_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await ItemStores.SaveObject(ObjectNames.Substance);
                await Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                DisplayException(ex.ToString());
            }
        }


        public void DisplayException(string message)
        {
            DisplayAlert("Exception", message + "...Trace: " + Environment.StackTrace,"Ok :(");
        }
    }
}