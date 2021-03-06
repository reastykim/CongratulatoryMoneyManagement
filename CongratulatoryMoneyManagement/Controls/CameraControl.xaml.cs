﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using CongratulatoryMoneyManagement.EventHandlers;
using CongratulatoryMoneyManagement.Helpers;

using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Media.Ocr;

namespace CongratulatoryMoneyManagement.Controls
{
    public interface ICameraController
    {
        void Capture();
        void Reset();
    }

    public sealed partial class CameraControl : ICameraController, INotifyPropertyChanged
    {
        public event EventHandler<CameraControlEventArgs> PhotoTaken;
        public event EventHandler Resetted;

        public static readonly DependencyProperty CanSwitchProperty =
            DependencyProperty.Register("CanSwitch", typeof(bool), typeof(CameraControl), new PropertyMetadata(false));

        public static readonly DependencyProperty PanelProperty =
            DependencyProperty.Register("Panel", typeof(Panel), typeof(CameraControl), new PropertyMetadata(Panel.Front, OnPanelChanged));
        
        public static readonly DependencyProperty IsIdleProperty =
            DependencyProperty.Register("IsIdle", typeof(bool), typeof(CameraControl), new PropertyMetadata(false));

        public static readonly DependencyProperty CameraButtonStyleProperty =
            DependencyProperty.Register("CameraButtonStyle", typeof(Style), typeof(CameraControl), new PropertyMetadata(null));

        public static readonly DependencyProperty SwitchCameraButtonStyleProperty =
            DependencyProperty.Register("SwitchCameraButtonStyle", typeof(Style), typeof(CameraControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ResetPhotoButtonStyleProperty =
            DependencyProperty.Register("ResetPhotoButtonStyle", typeof(Style), typeof(CameraControl), new PropertyMetadata(null));

        // Rotation metadata to apply to the preview stream and recorded videos (MF_MT_VIDEO_ROTATION)
        // Reference: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868174.aspx
        private readonly Guid _rotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");
        private readonly DisplayInformation _displayInformation = DisplayInformation.GetForCurrentView();
        private readonly SimpleOrientationSensor _orientationSensor = SimpleOrientationSensor.GetDefault();
        private MediaCapture _mediaCapture;
        private bool _isPreviewing;
        private bool _mirroringPreview;
        private SimpleOrientation _deviceOrientation = SimpleOrientation.NotRotated;
        private DisplayOrientations _displayOrientation = DisplayOrientations.Portrait;
        private DeviceInformationCollection _cameraDevices;

        public bool CanSwitch
        {
            get { return (bool)GetValue(CanSwitchProperty); }
            set { SetValue(CanSwitchProperty, value); }
        }

        public Panel Panel
        {
            get { return (Panel)GetValue(PanelProperty); }
            set { SetValue(PanelProperty, value); }
        }

        public bool IsIdle
        {
            get { return (bool)GetValue(IsIdleProperty); }
            private set { SetValue(IsIdleProperty, value); }
        }

        public BitmapSource Photo
        {
            get { return photo; }
            private set
            {
                if (photo != value)
                {
                    photo = value;
                    RaisePropertyChanged();
                }
            }
        }
        private BitmapSource photo;

        public Style CameraButtonStyle
        {
            get { return (Style)GetValue(CameraButtonStyleProperty); }
            set { SetValue(CameraButtonStyleProperty, value); }
        }

        public Style SwitchCameraButtonStyle
        {
            get { return (Style)GetValue(SwitchCameraButtonStyleProperty); }
            set { SetValue(SwitchCameraButtonStyleProperty, value); }
        }

        public Style ResetPhotoButtonStyle
        {
            get { return (Style)GetValue(ResetPhotoButtonStyleProperty); }
            set { SetValue(ResetPhotoButtonStyleProperty, value); }
        }

        public CameraControl()
        {
            InitializeComponent();

            CameraButtonStyle = Resources["CameraButtonStyle"] as Style;
            SwitchCameraButtonStyle = Resources["SwitchCameraButtonStyle"] as Style;
            ResetPhotoButtonStyle = Resources["ResetPhotoButtonStyle"] as Style;

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        public async Task InitializeCameraAsync()
        {
            try
            {
                if (_mediaCapture == null)
                {
                    _mediaCapture = new MediaCapture();
                    _mediaCapture.Failed += MediaCapture_Failed;

                    _cameraDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
                    if (_cameraDevices == null || !_cameraDevices.Any())
                    {
                        throw new NotSupportedException();
                    }

                    var device = _cameraDevices.FirstOrDefault(camera => camera.EnclosureLocation?.Panel == Panel);

                    var cameraId = device?.Id ?? _cameraDevices.First().Id;

                    await _mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings { VideoDeviceId = cameraId, StreamingCaptureMode = StreamingCaptureMode.Video });

                    if (Panel == Panel.Back)
                    {
                        // TODO : will be removed
                        //_mediaCapture.SetRecordRotation(VideoRotation.Clockwise90Degrees);
                        //_mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
                        _mirroringPreview = false;
                    }
                    else
                    {
                        _mirroringPreview = true;
                    }

                    IsIdle = true;
                    CanSwitch = _cameraDevices?.Count > 1;
                    RegisterOrientationEventHandlers();
                    await StartPreviewAsync();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("Camera_Exception_UnauthorizedAccess".GetLocalized(), ex);
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException("Camera_Exception_NotSupported".GetLocalized(), ex);
            }
        }

        public async Task CleanupCameraAsync()
        {
            if (IsIdle)
            {
                if (_isPreviewing)
                {
                    await StopPreviewAsync();
                }

                UnregisterOrientationEventHandlers();
                IsIdle = false;
            }

            if (_mediaCapture != null)
            {
                _mediaCapture.Failed -= MediaCapture_Failed;
                _mediaCapture.Dispose();
                _mediaCapture = null;
            }
        }

        public async Task<BitmapSource> TakePhoto()
        {
            if (IsIdle != true)
            {
                return null;
            }

            IsIdle = false;

            using (var stream = new InMemoryRandomAccessStream())
            {
                await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), stream);

                var photoOrientation = _displayInformation
                    .ToSimpleOrientation(_deviceOrientation, _mirroringPreview)
                    .ToPhotoOrientation(_mirroringPreview);
                
                var photoUri = await ReencodeAndSavePhotoAsync(stream.CloneStream(), photoOrientation);
                var recognizeResult = await RecognizeAsync(stream.CloneStream());

                PhotoTaken?.Invoke(this, new CameraControlEventArgs(photoUri, recognizeResult.Item2));
                IsIdle = true;
                
                return recognizeResult.Item1;
            }
        }

        public void SwitchPanel()
        {
            Panel = (Panel == Panel.Front) ? Panel.Back : Panel.Front;
        }

        public async void Capture()
        {
            Photo = await TakePhoto();
        }

        public void Reset()
        {
            Photo = null;
            Resetted?.Invoke(this, EventArgs.Empty);
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await InitializeCameraAsync();
            }
            catch (Exception ex)
            {
                errorMessage.Text = ex.Message;
            }
        }

        private async void OnUnloaded(object sender, RoutedEventArgs e)
        {
            await CleanupCameraAsync();
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            Capture();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchPanel();
        }

        private async void CleanAndInitialize()
        {
            await Task.Run(async () => await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await CleanupCameraAsync();
                await InitializeCameraAsync();
            }));
        }

        private void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            Task.Run(async () => await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await CleanupCameraAsync();
            }));
        }

        private async Task StartPreviewAsync()
        {
            PreviewControl.Source = _mediaCapture;
            PreviewControl.FlowDirection = _mirroringPreview ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            if (_mediaCapture != null)
            {
                await _mediaCapture.StartPreviewAsync();
                await SetPreviewRotationAsync();
                _isPreviewing = true;
            }
        }

        private async Task SetPreviewRotationAsync()
        {
            _displayOrientation = _displayInformation.CurrentOrientation;
            int rotationDegrees = _displayOrientation.ToDegrees();

            if (_mirroringPreview)
            {
                rotationDegrees = (360 - rotationDegrees) % 360;
            }

            if (_mediaCapture != null)
            {
                var props = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview);
                props.Properties.Add(_rotationKey, rotationDegrees);
                await _mediaCapture.SetEncodingPropertiesAsync(MediaStreamType.VideoPreview, props, null);
            }
        }

        private async Task StopPreviewAsync()
        {
            _isPreviewing = false;
            await _mediaCapture.StopPreviewAsync();
            PreviewControl.Source = null;
        }

        private async Task<Uri> ReencodeAndSavePhotoAsync(IRandomAccessStream stream, PhotoOrientation photoOrientation)
        {
            using (var inputStream = stream)
            {
                var decoder = await BitmapDecoder.CreateAsync(inputStream);

                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync($"{DateTime.Now.ToString("HHmmss")}.jpeg", CreationCollisionOption.GenerateUniqueName);

                using (var outputStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

                    //var properties = new BitmapPropertySet { { "System.Photo.Orientation", new BitmapTypedValue(photoOrientation, PropertyType.UInt16) } };

                   // await encoder.BitmapProperties.SetPropertiesAsync(properties);
                    await encoder.FlushAsync();
                }

                return new Uri(file.Path);
            }
        }

        private async Task<Tuple<BitmapSource, OcrResult>> RecognizeAsync(IRandomAccessStream stream)
        {
            using (var inputStream = stream)
            {
                var decoder = await BitmapDecoder.CreateAsync(inputStream);
                var bitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                var imgSource = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);
                bitmap.CopyToBuffer(imgSource.PixelBuffer);

                var ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
                var ocrResult = await ocrEngine.RecognizeAsync(bitmap);

                return new Tuple<BitmapSource, OcrResult>(imgSource, ocrResult);
            }
        }

        private void RegisterOrientationEventHandlers()
        {
            if (_orientationSensor != null)
            {
                _orientationSensor.OrientationChanged += OrientationSensor_OrientationChanged;
                _deviceOrientation = _orientationSensor.GetCurrentOrientation();
            }

            _displayInformation.OrientationChanged += DisplayInformation_OrientationChanged;
            _displayOrientation = _displayInformation.CurrentOrientation;
        }

        private void UnregisterOrientationEventHandlers()
        {
            if (_orientationSensor != null)
            {
                _orientationSensor.OrientationChanged -= OrientationSensor_OrientationChanged;
            }

            _displayInformation.OrientationChanged -= DisplayInformation_OrientationChanged;
        }

        private void OrientationSensor_OrientationChanged(SimpleOrientationSensor sender, SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            if (args.Orientation != SimpleOrientation.Faceup && args.Orientation != SimpleOrientation.Facedown)
            {
                _deviceOrientation = args.Orientation;
            }
        }

        private async void DisplayInformation_OrientationChanged(DisplayInformation sender, object args)
        {
            _displayOrientation = sender.CurrentOrientation;
            await SetPreviewRotationAsync();
        }

        private static void OnPanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (CameraControl)d;

            if (ctrl.IsIdle)
            {
                ctrl.CleanAndInitialize();
            }
        }

        #region Implements INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
