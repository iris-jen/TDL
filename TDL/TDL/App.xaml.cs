using SelfMonitoringApp.Services;
using SelfMonitoringApp.Views;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;
using System;
namespace SelfMonitoringApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MoodStore>();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            try
            {
                ItemStores.InitStores();
            }
            catch (Exception e)
            {

            }
                        
        }

        
        protected override void OnSleep()
        {
            ItemStores.SaveObject(ObjectNames.All);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
