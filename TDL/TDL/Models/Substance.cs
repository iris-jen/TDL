using System;

namespace SelfMonitoringApp.Models
{
    public class Substance : ModelBase
    {
        private string _substanceName;
        public string SubstanceName
        {
            get => _substanceName;
            set
            {
                if (_substanceName == value) return;

                _substanceName = value;
                OnPropertyChanged();
            }
        }

        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                if (Math.Abs(_amount - value) < 0.01) return;

                _amount = value;
                OnPropertyChanged();
            }
        }

        private string _consumptionMethod;

        public string ConsumptionMethod
        {
            get => _consumptionMethod;
            set
            {
                if (_consumptionMethod == value) return;

                _consumptionMethod = value;
                OnPropertyChanged();
            }
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set
            {
                if (_unit == value) return;

                _unit = value;
                OnPropertyChanged();
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment == value) return;
                
                _comment = value;
                OnPropertyChanged();
            }
        }


        public DateTime RegisteredTime { get; set; }
    }
}
