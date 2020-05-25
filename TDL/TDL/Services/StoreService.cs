using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace SelfMonitoringApp.Services
{
    public static class StoreService
    {
        public const string FileName = "SDL_Logs.json";
        public const string FolderName = "SDL_Files";

        public static string AndroidFolderPath => 
            Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, FolderName);

        public static string AndroidFullPath =>
            Path.Combine(AndroidFolderPath, FileName);

        public static ItemStores ItemStores { get; set; } 

        /// <summary>
        /// Reloads all properties in the ItemStores object from the save file
        /// </summary>
        public static void LoadStores()
        {
            ItemStores = new ItemStores();

            if (File.Exists(AndroidFullPath))
            {
                var contents = File.ReadAllText(AndroidFullPath);

                if (string.IsNullOrEmpty(contents))
                    return;

                ItemStores = JsonConvert.DeserializeObject<ItemStores>(contents);
            }
        }

        /// <summary>
        /// Saves all properties in the ItemStores object to the save file
        /// </summary>
        public static void SaveStores()
        {
            var output = JsonConvert.SerializeObject(ItemStores, Formatting.Indented);

            if (!Directory.Exists(AndroidFolderPath))
                Directory.CreateDirectory(AndroidFolderPath);
            
            File.WriteAllText(AndroidFullPath, output);
        }
    }
}
