using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Base
{
    public abstract partial class BaseViewModel
    {

		public INavigation Navigation { get; set; }

		public Page Page { get; set; }

		public Command PopCommand { get; set; }

		public Command SaveCommand { get; set; }

		public BaseViewModel(Page p)
		{
			InitFromPage(p);
		}
		
		/// <summary>
		/// Use this blank constructor when adding view model to collections so your not passing navigation everywhere.
		/// </summary>
		public BaseViewModel()
		{
		}

		public void InitFromPage(Page p)
		{
			Page = p;
			Navigation = p.Navigation;
			PopCommand = new Command(() => { Navigation.PopAsync(); });
		}

		public void DisplayAlert(string message)
		{
			Page.DisplayAlert("Thomas Says:", message, "Ok.");
		}
		public void DisplayException(Exception ex)
		{
			Page.DisplayAlert("Exception", $"Exception: {ex}{Environment.NewLine} Trace: {Environment.StackTrace}", "Ok :(");
		}

	}
}
