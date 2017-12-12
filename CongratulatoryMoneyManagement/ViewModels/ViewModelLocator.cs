using System;

using CongratulatoryMoneyManagement.Services;
using CongratulatoryMoneyManagement.Views;

using GalaSoft.MvvmLight.Ioc;

using Microsoft.Practices.ServiceLocation;
using CongratulatoryMoneyManagement.Services.DataService;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDataService, SqliteDataService>(true);

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            Register<NavigationRootViewModel, NavigationRootPage>();
            Register<TakeViewModel, TakePage>();
            Register<SpendViewModel, SpendPage>();
            Register<StatementViewModel, StatementPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public StatementViewModel StatementViewModel => ServiceLocator.Current.GetInstance<StatementViewModel>();

        public SpendViewModel SpendViewModel => ServiceLocator.Current.GetInstance<SpendViewModel>();

        public TakeViewModel TakeViewModel => ServiceLocator.Current.GetInstance<TakeViewModel>();

        public NavigationRootViewModel NavigationRootViewModel => ServiceLocator.Current.GetInstance<NavigationRootViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
