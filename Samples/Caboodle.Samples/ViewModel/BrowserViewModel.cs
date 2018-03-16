﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Caboodle;
using Xamarin.Forms;

namespace Caboodle.Samples.ViewModel
{
    public class BrowserViewModel : BaseViewModel
    {
        string browserStatus;

        public ICommand OpenUriCommand { get; }

        public string BrowserStatus
        {
            get => browserStatus;
            set => SetProperty(ref browserStatus, value);
        }

        public BrowserViewModel()
        {
            OpenUriCommand = new Command(async () =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                try
                {
                    await Browser.OpenAsync(uri, launchType);
                }
                catch (Exception e)
                {
                    BrowserStatus = $"Unable to open Uri {e.Message}";
                    Debug.WriteLine(browserStatus);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        string uri = "http://xamarin.com";

        public string Uri
        {
            get => uri;
            set => SetProperty(ref uri, value);
        }

        List<string> browserlaunchertypes = new List<string>
        {
            $"Uri Launcher",
            $"System Browser(CustomTabs, Safari)",
        };

        public List<string> BrowserLaunchTypes => browserlaunchertypes;

        BrowserLaunchingType launchType = BrowserLaunchingType.SystemBrowser;

        string browserType = $"System Browser(CustomTabs, Safari)";

        public string BrowserType
        {
            get => browserType;
            set
            {
                SetProperty(ref browserType, value);
                if (browserType == "Uri Launcher")
                {
                    launchType = BrowserLaunchingType.UriLauncher;
                }
                else
                {
                    launchType = BrowserLaunchingType.SystemBrowser;
                }
            }
        }
    }
}
