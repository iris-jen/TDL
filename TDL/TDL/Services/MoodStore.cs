using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SelfMonitoringApp.Models;

namespace SelfMonitoringApp.Services
{
    public class MoodStore
    {
        public List<Mood> Moods { get; private set; }

        public MoodStore()
        {
            Moods = new List<Mood>();
        }


        //public string GetListObject()
        //{

        //}

        //public void RestoreFromList()
        //{

        //}

        public async Task<bool> AddItemAsync(Mood item)
        {
            Moods.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Mood item)
        {
            var oldItem = Moods.FirstOrDefault(arg => arg.RegisteredTime == item.RegisteredTime);
            Moods.Remove(oldItem);
            Moods.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string registeredTime)
        {
            var oldItem = Moods.FirstOrDefault(arg => arg.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime);
            Moods.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Mood> GetItemAsync(string registeredTime)
        {
            return await Task.FromResult(Moods.FirstOrDefault(s => s.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime));
        }

        public async Task<IEnumerable<Mood>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Moods);
        }

    }
}
