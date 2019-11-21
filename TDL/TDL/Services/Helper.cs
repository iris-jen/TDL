using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SelfMonitoringApp.Services
{
    public static class Helper
    {
        public const string AddNew = "-Add New-";

        public static List<string> Emotions = new List<string>
        {
            "Devastated",
            "Depressed",
            "Sad",
            "Content",
            "Excited",
            "Angry",
            "Anxious",
            "Apathetic",
            "Strung Out",
            "Hyper",
            "Over whelmed",
            "Bored",
            "Jealous",
            "Nervous",
            AddNew
        };

        public static Dictionary<MealSizes, string> MealSizesDictionary = new Dictionary<MealSizes, string>()
        {
            {MealSizes.Under,   "Under Ate"},
            {MealSizes.Light,   "Smaller than usual" },
            {MealSizes.Regular, "The usual" },
            {MealSizes.Large,   "BIG CHONKR'" },
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
            "Injected",
            "Smoked",
            "Snorted",
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
                var emotions = JObject.FromObject(Emotions);
                var substances = JObject.FromObject(Substances);
                var deliveryMethods = JObject.FromObject(DeliveryMethod);
                var unitsOfMeasurement = JObject.FromObject(UnitsOfMeasurement);

                var suggestionsObject = new JObject()
                {
                    { "Emotions", emotions},
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

        public static async void PopAlarm(Exception e, [CallerMemberName] string membername ="")
        {
            
        }
        
    }
}