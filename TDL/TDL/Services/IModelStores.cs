using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SelfMonitoringApp.Models;

namespace SelfMonitoringApp.Services
{
    public class SubstanceStore : StoreBase<Substance> { }
    public class SleepStore: StoreBase<Sleep> { }

    public class MoodStore : StoreBase<Mood> 
    { 
        public async Task AddRandomLogs()
        {
            List<string> Emotions = new List<string>()
            {
                "Lethargic",
                "Happy",
                "Sad",
                "Depressed",
                "Angry",
                "Excited",
                "Nervous",
                "Anxious"
            };

            for (int i = 1; i < 30; i++)
            {
                Random logs = new Random();
                for (int j = 0; j < logs.Next(5, 30); j++)
                {
                    Random rand = new Random();
                    DateTime dt = new DateTime
                        (
                            year: 2019,
                            month: 12,
                            day: i,
                            hour: rand.Next(23),
                            minute: rand.Next(59),
                            second: rand.Next(59)
                        );

                    Mood m = new Mood();
                    m.Description = $"AutoGen {j}";
                    m.RegisteredTime = dt;

                    var emotionIndex = rand.Next(Emotions.Count);

                    m.Emotion = Emotions[emotionIndex > 0 ? emotionIndex - 1:0];
                    m.OverallMood = rand.Next(9) + rand.NextDouble();
                    await AddItemAsync(m);
                }
            }
        }
    }
    public class MealStore : StoreBase<Meal> { }
}
