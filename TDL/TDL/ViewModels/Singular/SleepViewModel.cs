using SelfMonitoringApp.LogModels;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;


namespace SelfMonitoringApp.ViewModels
{
    public class SleepViewModel : BaseViewModel
    {
        private readonly Sleep _sleep = new Sleep();

        public TimeSpan SleepStart 
        {
            get => _sleep.SleepStart;
            set
            {
                if (_sleep.SleepStart == value)
                    return;

                _sleep.SleepStart = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan SleepEnd
        {
            get => _sleep.SleepEnd;
            set
            {
                if (_sleep.SleepEnd == value)
                    return;

                _sleep.SleepEnd = value;
                OnPropertyChanged();
            }
        }

        public bool RememberedDream
        {
            get => _sleep.RememberedDream;
            set
            {
                if (_sleep.RememberedDream == value)
                    return;

                _sleep.RememberedDream = value;
                OnPropertyChanged();
            }
        }
        public bool VividDream
        {
            get => _sleep.VividDream;
            set
            {
                if (_sleep.VividDream == value)
                    return;

                _sleep.VividDream = value;
                OnPropertyChanged();
            }
        }

        public bool Nightmare
        {
            get => _sleep.Nightmare;
            set
            {
                if (_sleep.Nightmare == value) 
                    return;

                _sleep.Nightmare = value;
                OnPropertyChanged();
            }
        }

        public string DreamLog
        {
            get => _sleep.DreamLog;
            set
            {
                if (_sleep.DreamLog == value) 
                    return;

                _sleep.DreamLog = value;
                OnPropertyChanged();
            }
        }

        public double RestRating
        {
            get => _sleep.RestRating;
            set
            {
                if (Math.Abs(_sleep.RestRating - value) < 0.01) 
                    return;

                _sleep.RestRating = value;
                OnPropertyChanged();
            }
        }
    }
}
