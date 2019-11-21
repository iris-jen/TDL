using SelfMonitoringApp.Services;
using SelfMonitoringApp.Views;
using Xamarin.Forms;

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
            // Handle when your app starts
            EvilStores.Load();
        }

        
        protected override void OnSleep()
        {
            EvilStores.SerializeAll();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
