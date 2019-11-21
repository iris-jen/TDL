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
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodChartsPage : ContentPage
    {

        

        public MoodChartsPage()
        {
            InitializeComponent();


            var items = EvilStores.MoodStores.GetItemsAsync().Result;
            List<Entry> entries = new List<Entry>();
            foreach (var item in items)
            {
                entries.Add(new Entry((float)item.OverallMood)
                {
                    Label = item.Emotion ?? "",
                    ValueLabel = item.OverallMood.ToString(CultureInfo.CurrentCulture)
                });
                    
            }
            MoodChart.Chart = new LineChart(){Entries = entries};
        }

        
        private async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void ButtonSave_OnClicked(object sender, EventArgs e)
        {
            
            
            await EvilStores.SaveObject(ObjectNames.Substance);
            await Navigation.PopAsync();

        }
    }
}