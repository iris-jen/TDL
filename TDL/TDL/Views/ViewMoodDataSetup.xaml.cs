using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;


namespace SelfMonitoringApp.Views
{
    public class MoodGroup : List<TextCell>
    {
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string Subtitle { get; set; }
        public MoodGroup(string title, string shortName)
        {
            Title = title;
            ShortName = shortName;
        }

        public static IList<MoodGroup> All { private set; get; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMoodDataSetup : ContentPage
    {
        public enum MoodListType
        {
            DateSeperated,
            NonSepeerated,
        }
        
        public ViewMoodDataSetup()
        {
            InitializeComponent();
        }

        public bool DateValid()
        {
            if (DatePickerStartDate.Date > DatePickerEndDate.Date || DatePickerEndDate.Date < DatePickerStartDate.Date)
                DisplayAlert("Your start date cant be after your end date or vice versa");
            else if (DatePickerStartDate.Date < DatePickerEndDate.Date || DatePickerStartDate.Date == DatePickerEndDate.Date)
            {
                return true;
            }
            return false;
        }

        public void LayoutList(List<Mood> moodList, MoodListType type = MoodListType.DateSeperated)
        {

            switch (type)
            {
                case MoodListType.DateSeperated:
                    Dictionary<string, MoodGroup> moodGroups = new Dictionary<string, MoodGroup>();
                    // Loop through all entry's that were passed in
                    foreach (Mood m in moodList)
                    {
                        // Use the registered date as the key for the groups
                        string date = m.RegisteredTime.Date.ToString();
                        
                        // Create group for current entry's date if missing
                        if (!moodGroups.ContainsKey(date))
                            moodGroups.Add(date, new MoodGroup(date, m.RegisteredTime.DayOfWeek.ToString()));
                        
                        moodGroups[date].Add(MakeMoodCell(m));
                    }
                    MoodListView.ItemsSource = moodGroups.Values.ToList();
                    break;

                case MoodListType.NonSepeerated:
                    List<TextCell> textCellList = new List<TextCell>();
                    foreach(Mood m in moodList)
                    {
                        textCellList.Add(MakeMoodCell(m));
                    }
                    MoodListView.ItemsSource = textCellList;
                    break;
            }
        }


        private async void ButtonGoToMood_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Mood> passinList = new List<Mood>();

                var startDate = DatePickerStartDate.Date;
                var endDate = DatePickerEndDate.Date;
                var endDateSelected = CheckboxEndDate.IsChecked;
                

                if (DateValid())
                {
                    var moodRet = ItemStores.MoodStores.GetItemsAsync().Result;
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

        public TextCell MakeMoodCell(Mood m)
        {
            return new TextCell()
            {
                Text = Helper.GetDateTimeString(m.RegisteredTime, false) + "Mood Rating: ".PadLeft(15) + m.OverallMood,
                Detail = $"Emotion: {m.Emotion}. Description added? {(string.IsNullOrEmpty(m.Description) ? "No" : "Yes")}",
                DetailColor = Color.FromHex(Helper.SecondaryTextColor),
                TextColor = Color.FromHex(Helper.PrimaryTextColor),                 
            };
        }

        private async void ButtonPopulateList_Clicked(object sender, EventArgs e)
        {
            var list = await ItemStores.MoodStores.GetItemsAsync();

            if (DateValid())
            {
                var startDate = DatePickerStartDate.Date;
                var endDate = DatePickerEndDate.Date;
                var endDateSelected = CheckboxEndDate.IsChecked;

                var listOutput = new List<Mood>();
                foreach(Mood m in list)
                {
                    if (m.RegisteredTime.Date >= startDate && m.RegisteredTime.Date <= endDate)
                        listOutput.Add(m);
                }

                LayoutList(listOutput);
            }

        }

        private void MoodListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        public void DisplayAlert(string message)
        {
            DisplayAlert("Uh oh :(", message, "Ok.");
        }
        public void DisplayException(Exception ex)
        {
            DisplayAlert("Exception", $"Exception: {ex}{Environment.NewLine} Trace: {Environment.StackTrace}", "Ok :(");
        }
    }


}