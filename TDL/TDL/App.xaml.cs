using SelfMonitoringApp.Services;
using SelfMonitoringApp.Views;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
using System;
using Plugin.Permissions;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace SelfMonitoringApp
{
    

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            try
            {

                Task.Run(GetPermision);
                StoreService.LoadStores();
            }
            catch (Exception e)
            {

            }
        }
        public static async Task<bool> GetPermision()
        {
            var lastPermision = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (lastPermision != PermissionStatus.Granted)
            {

                var permissionResponse = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                return permissionResponse[Permission.Storage] == PermissionStatus.Granted;
            }
            return true;
        }




        protected override void OnSleep()
        {
            StoreService.SaveStores();
        }


        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
