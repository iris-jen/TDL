using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SelfMonitoringApp.Services;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace SelfMonitoringApp
{
    public enum ObjectNames
    {
        Mood,
        Sleep,
        Substance,
        Meal,
        All,
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
    
    public static class ItemStores
    {
        public const string MoodFileName = "Mood.json";
        public const string SleepFileName = "Sleep.json";
        public const string SubstanceFileName = "Substance.json";
        public const string MealFileName = "Meal.json";

        private static string _root = string.Empty;
        

        public static MoodStore MoodStores;
        public static SleepStore SleepStores;
        public static SubstanceStore SubstanceStores;
        public static MealStore MealStores;
        
        private const string DataFolderName = "SelfMonitoring";

        public static async void InitStores()
        {
            try
            {
                _root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"SDL");
 
                if (!Directory.Exists(_root))
                    Directory.CreateDirectory(_root);

                await LoadObject(ObjectNames.All);

                await MoodStores.AddRandomLogs();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
   
        

        public static async Task LoadObject(ObjectNames objectName)
        {
            string objectPath = string.Empty;
            bool Load = false;

            // Create new objects and determine the directory's 
            switch (objectName)
            {
                case ObjectNames.Meal:
                    MealStores = new MealStore();
                    objectPath = Path.Combine(_root, MealFileName);
                    Load = true;
                    break;
                case ObjectNames.Mood:
                    MoodStores = new MoodStore();
                    objectPath = Path.Combine(_root, MoodFileName);
                    Load = true;
                    break;
                case ObjectNames.Sleep:
                    SleepStores = new SleepStore();
                    objectPath = Path.Combine(_root, SleepFileName);
                    Load = true;
                    break;
                case ObjectNames.Substance:
                    SubstanceStores = new SubstanceStore();    
                    objectPath = Path.Combine(_root, SubstanceFileName);
                    Load = true;
                    break;
            }
            if (Load)
            {
                string json = string.Empty;
                if (File.Exists(objectPath))
                {
                    // Read the file from the determined path
                    using (var inputFile = new StreamReader(objectPath, false))
                    {
                        json = await inputFile.ReadToEndAsync();
                        inputFile.Close();
                    }

                    // De-serialize the file if its not empty or empty JSON
                    if (json != "{}" && !string.IsNullOrEmpty(json))
                    {
                        if(objectName == ObjectNames.Meal) 
                            MealStores = JsonConvert.DeserializeObject<MealStore>(json);
                        else if (objectName == ObjectNames.Mood) 
                            MoodStores = JsonConvert.DeserializeObject<MoodStore>(json);
                        else if (objectName == ObjectNames.Sleep) 
                            SleepStores = JsonConvert.DeserializeObject<SleepStore>(json);
                        else if (objectName == ObjectNames.Substance) 
                            SubstanceStores = JsonConvert.DeserializeObject<SubstanceStore>(json);
                    }
                }
            }
            if (objectName == ObjectNames.All)
            {
                // If all is passed in, looped through all names of the enum and call this function on each
                foreach (var name in (ObjectNames[])Enum.GetValues(typeof(ObjectNames)))
                {
                    if (name != ObjectNames.All)
                        await LoadObject(name);
                }
            }
        }

        public static async Task SaveObject(ObjectNames objectNames)
        {
            string objectJson = string.Empty;
            string objectPath = string.Empty;
            bool save = false;

            switch (objectNames)
            {
                case ObjectNames.Meal:
                    if (MealStores.Items.Count > 0)
                    {
                        objectJson = JsonConvert.SerializeObject(MealStores, Formatting.Indented);
                        objectPath = Path.Combine(_root, MealFileName);
                        save = true;
                    }
                    break;
                case ObjectNames.Mood:
                    if (MoodStores.Items.Count > 0)
                    {
                        objectJson = JsonConvert.SerializeObject(MoodStores, Formatting.Indented);
                        objectPath = Path.Combine(_root, MoodFileName);
                        save = true;
                    }
                    break;
                case ObjectNames.Sleep:
                    if (SleepStores.Items.Count > 0)
                    {
                        objectJson = JsonConvert.SerializeObject(SleepStores, Formatting.Indented);
                        objectPath = Path.Combine(_root, SleepFileName);
                        save = true;
                    }
                    break;
                case ObjectNames.Substance:
                    if (SubstanceStores.Items.Count > 0)
                    {
                        objectJson = JsonConvert.SerializeObject(SubstanceStores, Formatting.Indented);
                        objectPath = Path.Combine(_root, SubstanceFileName);
                        save = true;
                    }
                    break;
                case ObjectNames.All:
                    foreach (var objectName in (ObjectNames[])Enum.GetValues(typeof(ObjectNames)))
                    {
                        if (objectName != ObjectNames.All)
                            await SaveObject(objectName);
                    }
                    break;
            }
            if (save)
            {
                using (var outputFile = new StreamWriter(objectPath, false))
                {
                    await outputFile.WriteAsync(objectJson);
                    await outputFile.FlushAsync();
                }
            }
        }
    }
}
