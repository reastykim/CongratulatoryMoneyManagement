﻿using System;

using CongratulatoryMoneyManagement.Services;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using CongratulatoryMoneyManagement.Views;

namespace CongratulatoryMoneyManagement
{
    public sealed partial class App : Application
    {
        private ActivationService ActivationService
        {
            get { return activationService.Value; }
        }
        private Lazy<ActivationService> activationService;

        public App()
        {
            // Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-us"; // for localizing test code
            InitializeComponent();

            EnteredBackground += App_EnteredBackground;

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(ViewModels.NavigationRootViewModel));
        }

        private async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            var deferral = e.GetDeferral();
            await Helpers.Singleton<SuspendAndResumeService>.Instance.SaveStateAsync();
            deferral.Complete();
        }

        protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }
    }
}
