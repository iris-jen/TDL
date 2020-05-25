using System;

namespace SelfMonitoringApp.LogModels
{

    public class Meal : ILogModel
    {
        public string MealSize { get; set; }

        public string  MealType { get; set; }

        public string Description { get; set; }

        public DateTime RegisteredTime { get; set; }

        public bool IsSelected { get; set; }
    }
}
