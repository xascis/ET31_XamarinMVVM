﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ET31_XamarinMVVM
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new ET31_XamarinMVVM.MainPage();
            //MainPage = new ET31_XamarinMVVM.View.ListadoPersonal();
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
