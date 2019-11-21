using System;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubstanceLoggerPage : ContentPage
    {

        private Substance Substance { get; set; }

        public SubstanceLoggerPage()
        {
            InitializeComponent();
            Substance = new Substance();
            BindingContext = Substance;
            PickerSubstance.ItemsSource = Helper.Substances;
            PickerUnit.ItemsSource = Helper.UnitsOfMeasurement;
            PickerUseMethod.ItemsSource = Helper.DeliveryMethod;
        }

        
        private async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void ButtonSave_OnClicked(object sender, EventArgs e)
        {
            Substance.RegisteredTime = DateTime.Now;
            await EvilStores.SubstanceStores.AddItemAsync(Substance);
            await EvilStores.SaveObject(ObjectNames.Substance);
            await Navigation.PopAsync();

        }
    }
}