using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using NLog.Common;

namespace SelfMonitoringApp.Services
{

    public static class Helper
    {

        
        public const string PrimaryTextColor = "212121";
        public const string SecondaryTextColor = "757575";
        public const string DividerColor = "BDBDBD";
        public const string AccentColor = "CDDC39";


        public static string GetDateTimeString(DateTime dt, bool _24hour)
        {
            var date = $"{dt.Year.ToString("00")}/{dt.Month.ToString("00")}/{dt.Day.ToString("00")}";
            var pm = dt.Hour >= 12;
            var hour = _24hour ? dt.Hour: pm ? dt.Hour : dt.Hour-12;

            return $"{date} {hour}:{dt.Minute}:{dt.Second} {(_24hour ? string.Empty: pm ? "pm":"am")}";
        }

        public static string GetTimeString(DateTime dt)
        {
            var pm = dt.Hour >= 12;
            var hour = pm ? dt.Hour - 12 : dt.Hour;
            
            return $"{(hour == 0 ? 12 :hour):00}:{dt.Minute:00} {(pm ? "PM" : "AM")}";
        }


        public const string AddNew = "-Add New-";

   

        public static Dictionary<MealSizes, string> MealSizesDictionary = new Dictionary<MealSizes, string>()
        {
            {MealSizes.Under,   "Under Ate"},
            {MealSizes.Light,   "Smaller than usual" },
            {MealSizes.Regular, "The usual" },
            {MealSizes.Large,   "Larger than usual'" },
            {MealSizes.Over,    "Over Ate"}
        };

        public static Dictionary<Meals, string> MealsDictionary = new Dictionary<Meals, string>()
        {
            {Meals.Breakfast, "Breakfast"},
            {Meals.Lunch, "Lunch"},
            {Meals.Dinner, "Dinner"},
        };

        public static List<string> Substances = new List<string>
        {
             "Cannabis",
             "Spirits",
             "Beer",
             "Wine",
             "Mixed Drink",
        };

        public static List<string> UnitsOfMeasurement = new List<string>
        {
            "ml",
            "drink",
            "mg",
            "g",
        };

        public static List<string> DeliveryMethod = new List<string>
        {
            "Smoked",
            "Vaporized",
            "Drank Quickly",
            "Drank Slowly",
        };



        public static string DataRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DataFolderName);
        public static string SuggestionsFilePath = Path.Combine(DataRootPath, "Suggestions.json");
        public const string DataFolderName = "SelfMonitoring";

        public static void SaveSuggestions()
        {
            try
            {
                var substances = JObject.FromObject(Substances);
                var deliveryMethods = JObject.FromObject(DeliveryMethod);
                var unitsOfMeasurement = JObject.FromObject(UnitsOfMeasurement);

                var suggestionsObject = new JObject()
                {
                    { "Substances", substances },
                    { "DeliveryMethods", deliveryMethods},
                    { "UnitsOfMeasurement", unitsOfMeasurement}
                };

                File.WriteAllText(SuggestionsFilePath, suggestionsObject.ToString(Formatting.Indented));

            }
            catch (Exception e)
            {

            }
        }

        public static void LoadSuggestions()
        {

        }


        public static void AddNewSuggestion()
        {

        }

        public static void ClearSuggestions()
        {

        }        
    }
}