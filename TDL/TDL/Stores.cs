using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SelfMonitoringApp.Services;
using System.Runtime.CompilerServices;

namespace SelfMonitoringApp
{
    public enum ObjectNames
    {
        Mood,
        Sleep,
        Substance,
        Meal,
    }

    public enum MealSizes
    {
        Under,
        Light,
        Regular,
        Large,
        Over,
    }

    public enum Meals
    {
        Breakfast,
        Lunch,
        Dinner,
    }
    
    public static class EvilStores
    {
        private static string _root = string.Empty;
        private static string _moodFilePath = string.Empty;
        private static string _sleepFilePath = string.Empty;
        private static string _substanceFilePath = string.Empty;
        private static string _mealFilePath = string.Empty;

        public static MoodStore MoodStores;
        public static SleepStore SleepStores;
        public static SubstanceStore SubstanceStores;
        public static MealStore MealStores;
        
        private const string DataFolderName = "SelfMonitoring";

        public static async void InitStores()
        {
            SetPaths(Environment.OSVersion.Platform);
            Load();
        }
        
        public static async void Load()
        {
            if (!Directory.Exists(_root))
                Directory.CreateDirectory(_root);
            
            if (!File.Exists(_moodFilePath))
                await SaveObject(ObjectNames.Mood);
            if (!File.Exists(_sleepFilePath))
                await SaveObject(ObjectNames.Sleep);
            if (!File.Exists(_substanceFilePath))
                await SaveObject(ObjectNames.Substance);
            if (!File.Exists(_mealFilePath))
                await SaveObject(ObjectNames.Meal);
            
            var sleepJson     = File.ReadAllText(_sleepFilePath);
            var mealJson      = File.ReadAllText(_mealFilePath);
            var substanceJson = File.ReadAllText(_substanceFilePath);
            var moodJson      = File.ReadAllText(_moodFilePath);

            if (mealJson != "{}" && !string.IsNullOrEmpty(mealJson)) MealStores = JsonConvert.DeserializeObject<MealStore>(mealJson);
            else MealStores = new MealStore();

            if (moodJson != "{}" && !string.IsNullOrEmpty(moodJson)) MoodStores = JsonConvert.DeserializeObject<MoodStore>(moodJson);
            else MoodStores = new MoodStore();

            if (sleepJson != "{}" && !string.IsNullOrEmpty(sleepJson)) SleepStores = JsonConvert.DeserializeObject<SleepStore>(sleepJson);
            else SleepStores = new SleepStore();

            if (substanceJson != "{}" && !string.IsNullOrEmpty(substanceJson)) SubstanceStores = JsonConvert.DeserializeObject<SubstanceStore>(substanceJson);
            else SubstanceStores = new SubstanceStore();
        }
        
        private static void SetPaths(PlatformID platformId)
        {
            var windowsRoot    = Path.Combine("C", "SelfMonitoring");
            var unixRoot       = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DataFolderName);
            
            _root              = platformId == PlatformID.Win32NT ? windowsRoot : unixRoot;
            _moodFilePath      = Path.Combine(Helper.DataRootPath, "Mood.json");
            _sleepFilePath     = Path.Combine(Helper.DataRootPath, "Sleep.json");
            _substanceFilePath = Path.Combine(Helper.DataRootPath, "Substance.json");
            _mealFilePath      = Path.Combine(Helper.DataRootPath, "Meal.json");
        }
        
        public static async void SerializeAll()
        {
            foreach (var objectName in (ObjectNames[])Enum.GetValues(typeof(ObjectNames)))
                await SaveObject(objectName);
        }

        public static async Task SaveObject(ObjectNames objectNames)
        {

            string objectJson = string.Empty;
            string objectPath = string.Empty;

            switch (objectNames)
            {
                case ObjectNames.Meal:
                    objectJson = JsonConvert.SerializeObject(MealStores, Formatting.Indented);
                    objectPath = _mealFilePath;
                    break;
                case ObjectNames.Mood:
                    objectJson = JsonConvert.SerializeObject(MoodStores, Formatting.Indented);
                    objectPath = _moodFilePath;
                    break;
                case ObjectNames.Sleep:
                    objectJson = JsonConvert.SerializeObject(SleepStores, Formatting.Indented);
                    objectPath = _sleepFilePath;
                    break;
                case ObjectNames.Substance:
                    objectJson = JsonConvert.SerializeObject(SubstanceStores, Formatting.Indented);
                    objectPath = _substanceFilePath;
                    break;
            }

            using (var outputFile = new StreamWriter(objectPath,false))
            {
                await outputFile.WriteAsync(objectJson);
            }

        }
        
    }
}
