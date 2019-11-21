﻿using System;
using SelfMonitoringApp.Models;
using SelfMonitoringApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodLoggerPage : ContentPage
    {

        private Mood mood { get; set; }

        public MoodLoggerPage()
        {
            InitializeComponent();
            mood = new Mood();
            BindingContext = mood;

            PickerEmotion.ItemsSource = Helper.Emotions;
        }

        
        private async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void ButtonSave_OnClicked(object sender, EventArgs e)
        {
            mood.RegisteredTime = DateTime.Now;
            await EvilStores.MoodStores.AddItemAsync(mood);
            await EvilStores.SaveObject(ObjectNames.Mood);
            await Navigation.PopAsync();

        }
    }
}