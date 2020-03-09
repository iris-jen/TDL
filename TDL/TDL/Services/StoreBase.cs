using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SelfMonitoringApp.Models;

namespace SelfMonitoringApp.Services
{
    public abstract class StoreBase<T>
    {
        private bool IsModel(T item) => item.GetType() == typeof(IModel);


        public List<T> Items = new List<T>();

        public async Task<bool> AddItemAsync(T item)
        {
            try
            {
                Items.Add(item);
                return await Task.FromResult(true);
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        public async Task<IModel>GetItemAsync(DateTime timeMark)
        {
            return await Task.FromResult((IModel)Items.FirstOrDefault(m => ((IModel)m).RegisteredTime == timeMark));
        }

        public async Task<bool> DeleteItemAsync(DateTime timeMark)
        {
            try
            {
                Type firstItem = Items.First().GetType();

                if (firstItem == typeof(IModel))
                {
                    var oldItem = Items.FirstOrDefault(x => ((IModel)x).RegisteredTime == timeMark);
                    return await Task.FromResult(Items.Remove(oldItem));
                }
                else
                    throw new NotImplementedException($"only items of type IModel are accounted for, first item of base collection was {firstItem}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateItemAsync(T item, DateTime timeMark)
        {
            if (IsModel(item))
            {
                var oldItem = Items.FirstOrDefault(x => timeMark == ((IModel)x).RegisteredTime);
                Items.Remove(oldItem);
                Items.Add(item);
                return await Task.FromResult(true);
            }
            else
                return await Task.FromResult(false);
        }

        public async Task<List<T>> GetItemsAsync()
        {
            return await Task.FromResult(Items.ToList<T>());
        }
    }
}
