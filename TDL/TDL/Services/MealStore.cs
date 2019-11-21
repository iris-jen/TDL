using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SelfMonitoringApp.Models;

namespace SelfMonitoringApp.Services
{
    public class MealStore
    {
        private readonly List<Meal> meals;

        public MealStore()
        {
            meals = new List<Meal>();
        }




        public async Task<bool> AddItemAsync(Meal item)
        {
            meals.Add(item);
            return await Task.FromResult(true);
            
        }

        public async Task<bool> UpdateItemAsync(Meal item)
        {
            var oldItem = meals.FirstOrDefault(arg => arg.RegisteredTime == item.RegisteredTime);
            meals.Remove(oldItem);
            meals.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string registeredTime)
        {
            var oldItem = meals.FirstOrDefault(arg => arg.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime);
            meals.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Meal> GetItemAsync(string registeredTime)
        {
            return await Task.FromResult(meals.FirstOrDefault(s => s.RegisteredTime.ToString(CultureInfo.InvariantCulture) == registeredTime));
        }

        public async Task<IEnumerable<Meal>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(meals);
        }

    }
}
