﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokClone.ContentViews;
using TikTokClone.Models;
using TikTokClone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TikTokClone.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainView : ContentPage
    {
        private readonly Lazy<TabItemHomeView> _homeView;
        private readonly Lazy<TabItemDiscoverView> _discoverView;
        private readonly Lazy<TabItemAddView> _addView;
        private readonly Lazy<TabItemInboxView> _inboxView;
        private readonly Lazy<TabItemMeView> _meView;
        private Color _tabBarTransparent = Color.Transparent;
        private Color _tabBarWhite = Color.White;
        
        public MainView()
        {
            InitializeComponent();

            _homeView = new Lazy<TabItemHomeView>();
            _discoverView = new Lazy<TabItemDiscoverView>();
            _addView = new Lazy<TabItemAddView>();
            _inboxView = new Lazy<TabItemInboxView>();
            _meView = new Lazy<TabItemMeView>();

            SetHomeContentTab();
        }

        private async void OnTabTapped(object sender, EventArgs args)
        {
            await OnTabTappedAsync(sender, args);
        }

        private async Task OnTabTappedAsync(object sender, EventArgs args)
        {
            if (args is TappedEventArgs tappedEventArgs && tappedEventArgs.Parameter is string tabItem)
            {
                if (tabItem == "home")
                {
                    await SetHomeContentTabAndPlayVideoAsync();
                    return;
                }

                if (tabItem == "discover")
                {
                    SetDiscoverContentTab();
                    return;
                }

                if (tabItem == "add")
                {
                    SetAddContentTab();
                    return;
                }

                if (tabItem == "inbox")
                {
                    SetInboxContentTab();
                    return;
                }

                if (tabItem == "me")
                {
                    SetMeContentTab();
                    return;
                }
            }
        }

        private async Task SetHomeContentTabAndPlayVideoAsync()
        {
            SetHomeContentTab();
            await PlayVideoAsync();
        }

        private void SetHomeContentTab()
        {
            TabItemContentView.Content = _homeView.Value;
            TabBar.BackgroundColor = _tabBarTransparent;
            HideStatusBar();
        }

        private async Task PlayVideoAsync()
        {
            var carouselViewElement = _homeView.Value.FindByName<CarouselView>("CarouselViewVideos");
            carouselViewElement.IsScrollAnimated = false;
            await Task.Delay(100);
            carouselViewElement.IsScrollAnimated = true;
            _homeView.Value.PlayVideoInOfBounds();
        }

        private void SetDiscoverContentTab()
        {
            TabItemContentView.Content = _discoverView.Value;
            TabBar.BackgroundColor = _tabBarWhite;
            _homeView.Value.StopVideoOutOfBounds();
            ShowStatusBar();
        }

        private void SetAddContentTab()
        {
            TabItemContentView.Content = _addView.Value;
            TabBar.BackgroundColor = _tabBarWhite;
            _homeView.Value.StopVideoOutOfBounds();
            ShowStatusBar();
        }

        private void SetInboxContentTab()
        {
            TabItemContentView.Content = _inboxView.Value;
            TabBar.BackgroundColor = _tabBarWhite;
            _homeView.Value.StopVideoOutOfBounds();
            ShowStatusBar();
        }

        private void SetMeContentTab()
        {
            TabItemContentView.Content = _meView.Value;
            TabBar.BackgroundColor = _tabBarWhite;
            _homeView.Value.StopVideoOutOfBounds();
            ShowStatusBar();
        }

        private void HideStatusBar() => On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True);
        private void ShowStatusBar() => On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.False);
    }
}
