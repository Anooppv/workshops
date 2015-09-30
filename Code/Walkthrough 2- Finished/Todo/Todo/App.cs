using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
    public class App : Application
    {
        public App()
        {
			SetupMobileService ();

            // The root page of your application
			MainPage = new NavigationPage (new TodoPage ()) {
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex ("2C97DE")
			};
        }

		private async void SetupMobileService ()
		{        
			var applicationURL = @"https://tododemoapplication.azure-mobile.net/";
			var applicationKey = @"xRelREGxnCOaYeXKUpqxFDRZKADbaV37";

			MobileService.Client = new MobileServiceClient (applicationURL, applicationKey);
			var items = await MobileService.Client.GetTable <TodoItem> ().ToListAsync ();

			if (items == null)
				System.Diagnostics.Debug.WriteLine ("NULL");
			else
				System.Diagnostics.Debug.WriteLine (items.Count);	
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
