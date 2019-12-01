using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDataMainPage : ContentPage
    {
        public ViewDataMainPage()
        {
            InitializeComponent();
     

     
        }

        private async void ButtonGoToMood_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewDataSetup());
        }
    }
}