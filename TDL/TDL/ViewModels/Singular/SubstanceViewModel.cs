using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    class SubstanceViewModel: BaseViewModel
    {
        #region Properties
        private readonly Substance _substance = new Substance();

        public string SubstanceName
        {
            get => _substance.SubstanceName;
            set
            {
                if (_substance.SubstanceName == value) return;

                _substance.SubstanceName = value;
                OnPropertyChanged();
            }
        }

        public double Amount
        {
            get => _substance.Amount;
            set
            {
                if (Math.Abs(_substance.Amount - value) < 0.01) 
                    return;

                _substance.Amount = value;
                OnPropertyChanged();
            }
        }

        public string ConsumptionMethod
        {
            get => _substance.ConsumptionMethod;
            set
            {
                if (_substance.ConsumptionMethod == value) 
                    return;

                _substance.ConsumptionMethod = value;
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get => _substance.Unit;
            set
            {
                if (_substance.Unit == value) 
                    return;

                _substance.Unit = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get => _substance.Comment;
            set
            {
                if (_substance.Comment == value) 
                    return;

                _substance.Comment = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> SubstancesList { get; set; }
        public ObservableCollection<string> UnitList{ get; set; }
        public ObservableCollection<string> DeliveryMethodList { get; set; }
        #endregion

        public SubstanceViewModel(Page page) : base (page)
        {
            SaveCommand = new Command(Save);
            SubstancesList     = new ObservableCollection<string>(Helper.Substances);
            UnitList           = new ObservableCollection<string>(Helper.UnitsOfMeasurement);
            DeliveryMethodList = new ObservableCollection<string>(Helper.DeliveryMethod);
        }
        
        public SubstanceViewModel(Substance substance) { _substance = substance; }

        private async void Save()
        {
            _substance.RegisteredTime = DateTime.Now;
            await StoreService.ItemStores.SubstanceStores.AddItemAsync(_substance);
            await Navigation.PopAsync();
        }
    }
}
