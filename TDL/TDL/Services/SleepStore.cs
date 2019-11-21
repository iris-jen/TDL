using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SelfMonitoringApp.Models;

namespace SelfMonitoringApp.Services
{
    public class SleepStore
    {
        private readonly List<Sleep> _sleeps;

        public SleepStore()
        {
            _sleeps = new List<Sleep>();
        }

        public async Task<bool> AddItemAsync(Sleep item)
        {
            _sleeps.Add(item);
            return await Task.FromResult(true);
            
        }

        public async Task<bool> UpdateItemAsync(Sleep item)
        {
            var oldItem = _sleeps.FirstOrDefault(arg => arg.RegisteredTime == item.RegisteredTime);
            _sleeps.Remove(oldItem);
            _sleeps.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string registeredTime)
        {
            var oldItem = _sleeps.FirstOrDefault(arg => arg.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime);
            _sleeps.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Sleep> GetItemAsync(string registeredTime)
        {
            return await Task.FromResult(_sleeps.FirstOrDefault(s => s.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime));
        }

        public async Task<IEnumerable<Sleep>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_sleeps);
        }

    }
}
