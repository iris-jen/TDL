using System;
using System.Linq;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealLoggerPage : ContentPage
    {

        private Meal meal { get; set; }

        public MealLoggerPage()
        {
            InitializeComponent();
            meal = new Meal();
            BindingContext = meal;

            PickerMealSize.ItemsSource = Helper.MealSizesDictionary.Values.ToList();
            PickerMealType.ItemsSource = Helper.MealsDictionary.Values.ToList();
        }

        
        private async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void ButtonSave_OnClicked(object sender, EventArgs e)
        {
            meal.RegisteredTime = DateTime.Now;
            await EvilStores.MealStores.AddItemAsync(meal);
            await EvilStores.SaveObject(ObjectNames.Meal);
            await Navigation.PopAsync();
        }
    }
}