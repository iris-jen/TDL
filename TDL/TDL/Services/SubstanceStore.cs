using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SelfMonitoringApp.Models;

namespace SelfMonitoringApp.Services
{
    public class SubstanceStore
    {
        private readonly List<Substance> _substances;

        public SubstanceStore()
        {
          
            _substances = new List<Substance>();
        }

        public async Task<bool> AddItemAsync(Substance item)
        {
            _substances.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Substance item)
        {
            var oldItem = _substances.FirstOrDefault(arg => arg.RegisteredTime == item.RegisteredTime);
            _substances.Remove(oldItem);
            _substances.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string registeredTime)
        {
            var oldItem = _substances.FirstOrDefault(arg => arg.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime);
            _substances.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Substance> GetItemAsync(string registeredTime)
        {
            return await Task.FromResult(_substances.FirstOrDefault(s => s.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime));
        }

        public async Task<IEnumerable<Substance>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_substances);
        }

    }
}
