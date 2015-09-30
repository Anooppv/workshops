using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Todo
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
			MainPage = new NavigationPage (new TodoPage ()) {
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex ("2C97DE")
			};
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
