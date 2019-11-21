using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SelfMonitoringApp.Services;

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

        public static string MoodFilePath = Path.Combine(Helper.DataRootPath, "Mood.json");
        public static string SleepFilePath = Path.Combine(Helper.DataRootPath, "Sleep.json");
        public static string SubstanceFilePath = Path.Combine(Helper.DataRootPath, "Substance.json");
        public static string MealFilePath = Path.Combine(Helper.DataRootPath, "Meal.json");

        public static MoodStore MoodStores = new MoodStore();
        public static SleepStore SleepStores = new SleepStore();
        public static SubstanceStore SubstanceStores = new SubstanceStore();
        public static MealStore MealStores = new MealStore();




        public static async void Load()
        {
            if (!Directory.Exists(Helper.DataRootPath))
                Directory.CreateDirectory(Helper.DataRootPath);

            if (!File.Exists(MoodFilePath))
                await SaveObject(ObjectNames.Mood);
            if (!File.Exists(SleepFilePath))
                await SaveObject(ObjectNames.Sleep);
            if (!File.Exists(SubstanceFilePath))
                await SaveObject(ObjectNames.Substance);
            if (!File.Exists(MealFilePath))
                await SaveObject(ObjectNames.Meal);

            var sleepJson = File.ReadAllText(SleepFilePath);
            var mealJson = File.ReadAllText(MealFilePath);
            var substanceJson = File.ReadAllText(SubstanceFilePath);
            var moodJson = File.ReadAllText(MoodFilePath);

            if (mealJson != "{}" && !string.IsNullOrEmpty(mealJson))
                MealStores = JsonConvert.DeserializeObject<MealStore>(mealJson);
            if (moodJson != "{}" && !string.IsNullOrEmpty(moodJson))
                MoodStores = JsonConvert.DeserializeObject<MoodStore>(moodJson);
            if (sleepJson != "{}" && !string.IsNullOrEmpty(sleepJson))
                SleepStores = JsonConvert.DeserializeObject<SleepStore>(sleepJson);
            if (substanceJson != "{}" && !string.IsNullOrEmpty(substanceJson))
                SubstanceStores = JsonConvert.DeserializeObject<SubstanceStore>(substanceJson);
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
                    objectPath = MealFilePath;
                    break;
                case ObjectNames.Mood:
                    objectJson = JsonConvert.SerializeObject(MoodStores, Formatting.Indented);
                    objectPath = MoodFilePath;
                    break;
                case ObjectNames.Sleep:
                    objectJson = JsonConvert.SerializeObject(SleepStores, Formatting.Indented);
                    objectPath = SleepFilePath;
                    break;
                case ObjectNames.Substance:
                    objectJson = JsonConvert.SerializeObject(SubstanceStores, Formatting.Indented);
                    objectPath = SubstanceFilePath;
                    break;
            }

            using (var outputFile = new StreamWriter(objectPath,false))
            {
                await outputFile.WriteAsync(objectJson);
            }

        }
        
    }
}
