﻿using System;
using TikTokClone.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TikTokClone
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "Shapes_Experimental" });

            InitializeComponent();

            MainPage = new MainView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
