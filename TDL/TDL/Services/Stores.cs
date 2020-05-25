using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfMonitoringApp.Services;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace SelfMonitoringApp
{
    public enum ObjectNames
    {
        Unkown = -1,
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
    
    public class ItemStores
    {
        public MoodStore MoodStores { get; set;}
        public SleepStore SleepStores { get; set; }
        public SubstanceStore SubstanceStores { get; set; }
        public MealStore MealStores { get; set; }

        public ItemStores()
        {
            MoodStores = new MoodStore();
            SleepStores = new SleepStore();
            SubstanceStores = new SubstanceStore();
            MealStores = new MealStore();
        }
    }
}
