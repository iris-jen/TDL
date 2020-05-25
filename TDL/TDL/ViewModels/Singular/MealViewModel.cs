using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Singular
{
    public class MealViewModel : BaseViewModel
    {
        #region Properties
        private readonly Meal _meal = new Meal();

        public string MealSize
        {
            get => _meal.MealSize;
            set
            {
                if (_meal.MealSize == value) return;

                _meal.MealSize = value;
                OnPropertyChanged();
            }
        }

        public string MealType
        {
            get => _meal.MealType;
            set
            {
                if (_meal.MealType == value) return;

                _meal.MealType = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _meal.Description;
            set
            {
                if (_meal.Description == value) return;

                _meal.Description = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> MealSizeList { get; set; }
        public ObservableCollection<string> MealTypeList { get; set; }
        #endregion

        public MealViewModel(Page p) : base (p)
        {
            SaveCommand = new Command(Save);
            MealSizeList = new ObservableCollection<string>(Helper.MealSizesDictionary.Values.ToList());
            MealTypeList = new ObservableCollection<string>(Helper.MealsDictionary.Values.ToList());
        }

        public MealViewModel(Meal meal) { _meal = meal; }

        private async void Save()
        {
            _meal.RegisteredTime = DateTime.Now;
            await StoreService.ItemStores.MealStores.AddItemAsync(_meal);
            await Navigation.PopAsync();
        }
    }
}
