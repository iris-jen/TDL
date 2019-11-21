using System;

namespace SelfMonitoringApp.Models
{
    public class Mood : ModelBase
    {
        private double _overallMood;
        public double OverallMood
        {
            get => _overallMood;
            set
            {
                if (Math.Abs(_overallMood - value) < 0.01) return;

                _overallMood = value;
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

        private string _emotion;
        public string Emotion
        {
            get => _emotion;
            set
            {
                if (_emotion == value) return;
                
                _emotion = value;
                OnPropertyChanged();
            }
        }

        public DateTime RegisteredTime { get; set; }

        public Mood()
        {
            OverallMood = 0;
            Description = "";
            Emotion = "";
        }
    }
}
