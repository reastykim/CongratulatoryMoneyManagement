using CongratulatoryMoneyManagement.Helpers;
using CongratulatoryMoneyManagement.Models;
using CongratulatoryMoneyManagement.Services;
using CongratulatoryMoneyManagement.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class NavigationRootViewModel : ViewModelBase
    {
        #region Properties


        #endregion

        #region Fields

        private NavigationServiceEx navigationService;

        #endregion

        #region Constructors & Initialize

        public NavigationRootViewModel(NavigationServiceEx navigationService)
        {
            this.navigationService = navigationService;
            Initialize();
        }
        private void Initialize()
        {

        }

        #endregion

        #region Commands

        public RelayCommand LoadedCommand
        {
            get => loadedCommand ?? (loadedCommand = new RelayCommand(ExecuteLoaded));
        }
        private RelayCommand loadedCommand;
        private void ExecuteLoaded()
        {

        }

        public RelayCommand<NavigationViewSelectionChangedEventArgs> NavViewSelectionChangedCommand
        {
            get => navViewSelectionChangedCommand ?? (navViewSelectionChangedCommand = new RelayCommand<NavigationViewSelectionChangedEventArgs>(ExecuteNavViewSelectionChanged));
        }
        private RelayCommand<NavigationViewSelectionChangedEventArgs> navViewSelectionChangedCommand;
        private async void ExecuteNavViewSelectionChanged(NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                await navigationService.NavigateAsync(typeof(SettingsPage));
                return;
            }

            var selectedItem = args.SelectedItem as NavigationViewItem;
            switch (selectedItem.Tag as string)
            {
                case "Take":
                    await navigationService.NavigateAsync(typeof(TakePage));
                    break;
                case "Spend":
                    await navigationService.NavigateAsync(typeof(SpendPage));
                    break;
                case "Statement":
                    await navigationService.NavigateAsync(typeof(StatementPage));
                    break;
            }
        }

        #endregion
    }
}
