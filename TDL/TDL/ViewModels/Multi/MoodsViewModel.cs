using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.ViewModels.Groups;
using SelfMonitoringApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{

    public class MoodsViewModel : BaseViewModel
    {
        public delegate void DateSetHandler(object sender, EventArgs e);

        public event DateSetHandler DateSetEvent;
        protected virtual void OnDateSetEvent()
        {
            DateSetEvent?.Invoke(this, EventArgs.Empty);
        }

        private DateTime _startDate = DateTime.Now.AddDays(-7);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate == value)
                    return;

                _startDate = value;

                FilterMoods();
                OnPropertyChanged();
            }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate == value)
                    return;

                _endDate = value;
                FilterMoods();
                OnPropertyChanged();
            }
        }

        private List<MoodGroup> _filteredMoodGroups = new List<MoodGroup>();
        public List<MoodGroup> FilteredMoodGroups
        {
            get => _filteredMoodGroups;
            set
            {
                if (_filteredMoodGroups == value)
                    return;

                _filteredMoodGroups = value;
                OnPropertyChanged();
            }
        }

        public Command FilterByDateCommand { get; private set; }

        public Command GoMoodGraphCommand { get; private set; }

        private MoodViewModel LastMood { get; set; }

        internal void ToggleExpandedItem(MoodViewModel mood)
        {
            Parallel.ForEach(FilteredMoodGroups, m =>
            {
                if (m.Contains(mood))
                {
                    var index = m.IndexOf(mood);
                    mood.IsSelected = !mood.IsSelected;
                    m.Remove(mood);
                    m.Insert(index, mood);
                }

                if (m.Contains(LastMood) && LastMood != mood)
                {
                    var index = m.IndexOf(LastMood);
                    LastMood.IsSelected = false;
                    m.Remove(LastMood);
                    m.Insert(index, LastMood);
                }
            });
            LastMood = mood;
            OnPropertyChanged("FilteredMoodGroups");
        }

        public MoodsViewModel(Page p) : base (p)
        {
            SetCommands();
            StoreService.ItemStores.MoodStores.ItemsChanged += ItemsChanged;
        }

        private void ItemsChanged(object sender, EventArgs e)
        {
            FilterMoods();
        }

        private void SetCommands()
        {
            FilterByDateCommand = new Command(FilterMoods);
            GoMoodGraphCommand = new Command(GoMoodGraph);
            
        }

        private async void GoMoodGraph(object obj)
        {
            if (!DateValid(StartDate, EndDate))
                return;

             var moods = await GetFilteredMoods(StartDate, EndDate);

            await Navigation.PushAsync(new MoodChartsPage(moods));
        }

        public bool DateValid(DateTime startDate, DateTime endDate) => startDate.Date <= endDate.Date;

        public async Task<List<Mood>> GetFilteredMoods(DateTime startDate, DateTime endDate)
        {
            try
            {
                var list = await StoreService.ItemStores.MoodStores.GetItemsAsync();

                var outputList = new List<Mood>();

                foreach (Mood m in list)
                    if (m.RegisteredTime.Date >= startDate.Date && m.RegisteredTime.Date <= endDate.Date)
                        outputList.Add(m);

                return outputList.OrderBy(x => x.RegisteredTime).ToList();
                
            }
            catch(Exception ex)
            {
                DisplayException(ex);
                return null;
            }
        }

        public async void FilterMoods()
        {
            try
            {
                if (!DateValid(StartDate, EndDate))
                    return;

                OnDateSetEvent();

                var moodList = await GetFilteredMoods(StartDate, EndDate);

                if (moodList == null)
                    return;

                Dictionary<string, MoodGroup> groups = new Dictionary<string, MoodGroup>();

                foreach (Mood m in moodList)
                {
                    var date = m.RegisteredTime.Date.ToString();
                    if (!groups.ContainsKey(m.RegisteredTime.Date.ToString()))
                        groups.Add(date, new MoodGroup() { HeaderText = $"{m.RegisteredTime.DayOfWeek} - {date.Split(' ')[0]}" });

                    groups[date].Add(new MoodViewModel(m));
                }

                FilteredMoodGroups = groups.Values.ToList();
            }
            catch(Exception ex)
            {
                DisplayException(ex);
            }
        }

    }

}
