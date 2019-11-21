using System;

namespace SelfMonitoringApp.Models
{

    public class Meal : ModelBase
    {
        private string _mealSize;
        public string MealSize
        {
            get => _mealSize;
            set
            {
                if (_mealSize == value) return;

                _mealSize = value;
                OnPropertyChanged();
            }
        }


        private string _mealType;
        public string  MealType
        {
            get => _mealType;
            set
            {
                if (_mealType == value ) return;

                _mealType = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTime RegisteredTime { get; set; }
    }
}
