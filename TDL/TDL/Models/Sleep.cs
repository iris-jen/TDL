using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SelfMonitoringApp.Models
{
    public class Sleep : ModelBase
    {
        public DateTime SleepStart { get; private set; }
        public DateTime SleepEnd { get; private set; }

        private bool _rememberedDream;
        public bool RememberedDream
        {
            get => _rememberedDream;
            set
            {
                if (_rememberedDream == value) return;

                _rememberedDream = value;
                OnPropertyChanged();
            }
        }

        private bool _vividDream;
        public bool VividDream
        {
            get => _vividDream;
            set
            {
                if (_vividDream == value) return;

                _vividDream = value;
                OnPropertyChanged();
            }
        }

        private bool _nightmare;
        public bool Nightmare
        {
            get => _nightmare;
            set
            {
                if (_nightmare == value) return;

                _nightmare = value;
                OnPropertyChanged();
            }
        }

        private string _dreamLog;
        public string DreamLog
        {
            get => _dreamLog;
            set
            {
                if (_dreamLog == value) return;

                _dreamLog = value;
                OnPropertyChanged();
            }
        }


        private double _restRating;
        public double RestRating
        {
            get => _restRating;
            set
            {
                if (Math.Abs(_restRating - value) < 0.01) return;

                _restRating = value;
                OnPropertyChanged();
            }
        }

        public void OnSleep()
        {
            SleepStart = DateTime.Now;
            SleepSet = true;
        }

        public void OnWake()
        {
            SleepEnd = DateTime.Now;
            WakeSet = true;
        }

        public DateTime RegisteredTime { get; set; }
        public bool SleepSet { get; set; }
        public bool WakeSet { get; set; }
    }
}
