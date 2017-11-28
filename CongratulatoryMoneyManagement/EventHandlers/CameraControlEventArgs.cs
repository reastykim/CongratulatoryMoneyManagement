using System;
using Windows.Media.Ocr;

namespace CongratulatoryMoneyManagement.EventHandlers
{
    public class CameraControlEventArgs : EventArgs
    {
        public Uri Photo { get; private set; }
        public OcrResult OcrResult { get; private set; }

        public CameraControlEventArgs(Uri photo, OcrResult ocrResult)
        {
            Photo = photo;
            OcrResult = ocrResult;
        }
    }
}
