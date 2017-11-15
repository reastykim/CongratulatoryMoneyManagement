using System;

namespace CongratulatoryMoneyManagement.EventHandlers
{
    public class CameraControlEventArgs : EventArgs
    {
        public Uri Photo { get; set; }

        public CameraControlEventArgs(Uri photo)
        {
            Photo = photo;
        }
    }
}
