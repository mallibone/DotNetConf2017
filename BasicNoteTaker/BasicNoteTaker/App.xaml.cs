using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicNoteTaker.Views;
using Xamarin.Forms;

namespace BasicNoteTaker
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

		    MainPage = new EditNotePage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
