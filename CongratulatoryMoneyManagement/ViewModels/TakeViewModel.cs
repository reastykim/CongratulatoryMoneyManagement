using System;
using System.Linq;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media.Imaging;
using CongratulatoryMoneyManagement.EventHandlers;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using CongratulatoryMoneyManagement.Models;
using System.Collections.Generic;
using CongratulatoryMoneyManagement.Services.DataService;
using CongratulatoryMoneyManagement.Views;
using CongratulatoryMoneyManagement.Controls;

namespace CongratulatoryMoneyManagement.ViewModels
{
    public class TakeViewModel : ViewModelBase
    {
        #region Properties

        public IEnumerable<MoneyOption> MoneyOptions
        {
            get => moneyOptions;
            private set => Set(ref moneyOptions, value);
        }
        private IEnumerable<MoneyOption> moneyOptions;

        public MoneyOption SelectedMoneyOption
        {
            get => MoneyOptions?.FirstOrDefault(MO => MO.IsSelected);
            set
            {
                if (value == null)
                    return;

                foreach (var moneyOption in MoneyOptions)
                {
                    var isSelected = moneyOption == value;
                    moneyOption.IsSelected = isSelected;
                }
                RaisePropertyChanged("SelectedMoneyOption");
            }
        }

        public Uri PhotoUri
        {
            get { return photoUri; }
            set { Set(ref photoUri, value); }
        }
        private Uri photoUri;

        public string RecognizedText
        {
            get { return recognizedText; }
            set { Set(ref recognizedText, value); }
        }
        private string recognizedText;

        public double Sum
        {
            get { return sum; }
            set
            {
                if (Set(ref sum, value))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private double sum;
        
        public string GuestName
        {
            get { return guestName; }
            set
            {
                if (Set(ref guestName, value))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private string guestName;

        public ReturnPresentType SelectedReturnPresentType
        {
            get => selectedReturnPresentType;
            set => Set(ref selectedReturnPresentType, value);
        }
        private ReturnPresentType selectedReturnPresentType;

        public uint ReturnPresentQuantity
        {
            get => returnPresentQuantity;
            set => Set(ref returnPresentQuantity, value);
        }
        private uint returnPresentQuantity = 1;

        #endregion

        #region Fields

        private IDataService dataService;
        private WeakReference<ICameraController> cameraControllerReference;

        #endregion

        #region Constructor & Initialize

        public TakeViewModel(IDataService dataService)
        {
            this.dataService = dataService;

            Initialize();            
        }

        private async void Initialize()
        {
            MoneyOptions = await dataService.AllMoneyOptionsAsync();
        }

        #endregion

        #region Commands

        public RelayCommand<ICameraController> LoadedCommand
        {
            get => loadedCommand ?? (loadedCommand = new RelayCommand<ICameraController>(ExecuteLoaded));
        }
        private RelayCommand<ICameraController> loadedCommand;
        private void ExecuteLoaded(ICameraController cameraController)
        {
            cameraControllerReference = new WeakReference<ICameraController>(cameraController);
        }

        public RelayCommand UnloadedCommand
        {
            get => unloadedCommand ?? (unloadedCommand = new RelayCommand(ExecuteUnloaded));
        }
        private RelayCommand unloadedCommand;
        private void ExecuteUnloaded()
        {

        }

        #region Select Commands

        public RelayCommand<MoneyOption> SelectMoneyOptionCommand
        {
            get => selectMoneyOptionCommand ?? (selectMoneyOptionCommand = new RelayCommand<MoneyOption>(ExecuteSelectMoneyOption));
        }
        private RelayCommand<MoneyOption> selectMoneyOptionCommand;
        private void ExecuteSelectMoneyOption(MoneyOption moneyOption)
        {
            if (moneyOption == null)
                return;

            SelectedMoneyOption = moneyOption;
            Sum = SelectedMoneyOption.Sum;
        }

        public RelayCommand<ReturnPresentType> SelectReturnPresentTypeCommand
        {
            get => selectReturnPresentTypeCommand ?? (selectReturnPresentTypeCommand = new RelayCommand<ReturnPresentType>(ExecuteSelectReturnPresentType));
        }
        private RelayCommand<ReturnPresentType> selectReturnPresentTypeCommand;
        private void ExecuteSelectReturnPresentType(ReturnPresentType returnPresentType)
        {
            SelectedReturnPresentType = returnPresentType;
        }

        public RelayCommand<CameraControlEventArgs> OnCameraPhotoTakenCommand
        {
            get => onCameraPhotoTakenCommand ?? (onCameraPhotoTakenCommand = new RelayCommand<CameraControlEventArgs>(ExecuteOnCameraPhotoTaken));
        }
        private RelayCommand<CameraControlEventArgs> onCameraPhotoTakenCommand;
        private void ExecuteOnCameraPhotoTaken(CameraControlEventArgs args)
        {
            PhotoUri = args.Photo;
            RecognizedText = args.OcrResult.Text;
        }

        public RelayCommand OnCameraResettedCommand
        {
            get => onCameraResettedCommand ?? (onCameraResettedCommand = new RelayCommand(ExecuteOnCameraResetted));
        }
        private RelayCommand onCameraResettedCommand;
        private void ExecuteOnCameraResetted()
        {
            PhotoUri = null;
            RecognizedText = null;
        }

        #endregion

        #region AppBar Commands

        public RelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand(ExecuteSave, CanExecuteSave));
        }
        private RelayCommand saveCommand;
        private async void ExecuteSave()
        {
            var newItem = new CongratulatoryMoney();
            newItem.GuestName = GuestName;
            newItem.RecognizedText = RecognizedText;
            newItem.Sum = Sum;
            newItem.EnvelopeImageUri = PhotoUri?.AbsolutePath;
            newItem.ReturnPresent.Type = selectedReturnPresentType;
            newItem.ReturnPresent.Quantity = ReturnPresentQuantity;

            await dataService.SaveCongratulatoryMoneyAsync(newItem);
            ResetCommand.Execute(null);
        }
        private bool CanExecuteSave()
        {
            return Sum > 0;
        }

        public RelayCommand ResetCommand
        {
            get => resetCommand ?? (resetCommand = new RelayCommand(ExecuteReset));
        }
        private RelayCommand resetCommand;
        private void ExecuteReset()
        {
            SelectedMoneyOption = MoneyOptions?.FirstOrDefault(MO => MO.Sum == 0d);
            if (SelectedMoneyOption != null)
            {
                Sum = SelectedMoneyOption.Sum;
            }

            GuestName = String.Empty;
            SelectedReturnPresentType = ReturnPresentType.MealTickets;
            ReturnPresentQuantity = 1;

            ICameraController cameraController = null;
            if (cameraControllerReference.TryGetTarget(out cameraController))
            {
                cameraController.Reset();
            }
        }

        #endregion

        #endregion
    }
}
