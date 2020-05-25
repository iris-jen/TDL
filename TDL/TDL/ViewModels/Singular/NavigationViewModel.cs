using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SelfMonitoringApp.Views;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace SelfMonitoringApp.ViewModels
{
    public class NavigationViewModel : BaseViewModel
    {
        public Command GoMoodLoggerCommand      => new Command(()=>
                Navigation.PushAsync(new MoodLoggerPage()));
        public Command GoViewMoodDataCommand    => new Command(() =>
                Navigation.PushAsync(new ViewMoodDataSetup()));


        public Command GoSubstanceLoggerCommand => new Command(() =>
                Navigation.PushAsync(new SubstanceLoggerPage()));
        public Command GoViewSubstanceDataCommand;


        public Command GoMealLoggerCommand      => new Command(() => 
                Navigation.PushAsync(new SubstanceLoggerPage()));


        public Command GoSleepLoggerCommand     => new Command(() => 
                Navigation.PushAsync(new SleepLoggerPage()));

        public Command GoSettingsCommand => new Command(() => 
                Navigation.PushAsync(new SettingsPage()));
        

        public NavigationViewModel(Page page) : base(page)
        {
            GetPermision();
        }

        public void GetPermision()
        {
            var lastPermision = CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage).Result;

            if (lastPermision != PermissionStatus.Granted)
            {
                DisplayAlert("i need it");

                var permissionResponse = CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage).Result;
            }
        }
    }
}
