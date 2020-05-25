using SelfMonitoringApp.LogModels;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.Views;
using System.Threading.Tasks;
using System.Linq;

namespace SelfMonitoringApp.ViewModels
{
    public class MoodViewModel : BaseViewModel
    {
        #region Properties
        private readonly Mood _mood = new Mood();

        public Command DeleteLogCommand { get; private set; }
        public Command EditLogCommand { get; private set; }
        public Command ViewLogCommand { get; private set; }

        public double OverallMood
        {
            get => _mood.OverallMood;
            set
            {
                if (Math.Abs(_mood.OverallMood - value) < 0.01) 
                    return;

                _mood.OverallMood = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _mood.Description;
            set
            {
                if (_mood.Description != value) 
                    return;

                _mood.Description = value;
                OnPropertyChanged();
            }
        }

        public string Emotion
        {
            get => _mood.Emotion;
            set
            {
                if (_mood.Emotion == value) 
                    return;

                _mood.Emotion = value;
                OnPropertyChanged();
            }
        }

        public string ViewHeader
        {
            get
            {
                return $"{Helper.GetTimeString(_mood.RegisteredTime)} - Mood Rating: {OverallMood:0.0}, Emotion: {Emotion}";
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private bool Edited = false;
        public string MoodRating => OverallMood.ToString("0.0");
        public string Time => Helper.GetTimeString(_mood.RegisteredTime);
        #endregion

        #region Constructors
        public MoodViewModel(Page p) : base (p)
        {
            SetCommands();
        }

        public MoodViewModel(Mood mood)
        {
            _mood = mood;
            SetCommands();
        }
        #endregion

        private async Task DeleteLog()
        {
            await StoreService.ItemStores.MoodStores.DeleteItemAsync(_mood.RegisteredTime);
        }

        #region Commands


        public void SetCommands()
        {
            SaveCommand = new Command(Save);
            EditLogCommand = new Command(Edit);
            DeleteLogCommand = new Command(async () => { await DeleteLog(); });
        }

        private async void Edit()
        {
            Page currentPage = App.Current.MainPage;

            // Get whatever page is currently displayed.
            if (currentPage.Navigation != null)
                currentPage = currentPage.Navigation.NavigationStack.Last();

            base.InitFromPage(currentPage);
            Edited = true;

            await Navigation.PushAsync(new MoodLoggerPage(this));
        }
        #endregion

        /// <summary>
        /// Save command function.
        /// If mood has been edited, revise the item collection, otherwise 
        /// </summary>
        private async void Save()
        {
            if (Edited)
                await StoreService.ItemStores.MoodStores.UpdateItemAsync(_mood, _mood.RegisteredTime);
            else
            {
                _mood.RegisteredTime = DateTime.Now;
                await StoreService.ItemStores.MoodStores.AddItemAsync(_mood);
            }
            await Navigation.PopAsync();
        }
    }
}
