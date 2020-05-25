using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SelfMonitoringApp.LogModels;

namespace SelfMonitoringApp.Services
{
    public abstract class LogStoreBase<T>
    {
        private bool IsModel(T item) => item.GetType() == typeof(ILogModel);

        private List<T> _items = new List<T>();
        public List<T> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                    return;
                

                OnItemsChanged();
            }
        }

        public event EventHandler ItemsChanged;

        public delegate void ItemsChangedHandler(object sender, EventArgs e);

        protected virtual void OnItemsChanged()
        {
            EventHandler handler = ItemsChanged;
            handler?.Invoke(Items, EventArgs.Empty);
        }
            

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

        public Task<T>GetItemAsync(DateTime timeMark)
        {
            T output = default;
            Parallel.ForEach(Items, (item, i) =>
            {
                if (((ILogModel)item).RegisteredTime == timeMark)
                {
                    output = item;
                    i.Break();
                }
            }
            );

            return Task.FromResult(output);
        }

        public async Task<bool> DeleteItemAsync(DateTime timeMark)
        {
            try
            {
                //Type firstItem = Items.First().GetType();
                //var oldItem = Items.FirstOrDefault(x => ((ILogModel)x).RegisteredTime == timeMark);

                var requestedItem = await GetItemAsync(timeMark);
                bool removeOk = Items.Remove(requestedItem);

                if (removeOk)
                    OnItemsChanged();

                return await Task.FromResult(removeOk);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateItemAsync(T item, DateTime timeMark)
        {
            await DeleteItemAsync(timeMark);
            var addOkay = await AddItemAsync(item);
            if (addOkay)
                OnItemsChanged();
            
            return await Task.FromResult(addOkay);
        }

        public async Task<List<T>> GetItemsAsync()
        {
            return await Task.FromResult(Items.ToList<T>());
        }
    }
}
