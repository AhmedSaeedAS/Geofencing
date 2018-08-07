using System;
using Android.Content;
using Android.OS;

namespace fencing.Droid.Services
{
    public class GeolocationServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public GeolocationServiceConnection(GeolocationServiceBinder binder)
        {
            if (binder != null)
            {
                Binder = binder;
            }
        }

        public GeolocationServiceBinder Binder { get; set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var serviceBinder = service as GeolocationServiceBinder;

            if (serviceBinder == null)
                return;


            Binder = serviceBinder;
            Binder.IsBound = true;

            // raise the service bound event
            ServiceConnected?.Invoke(this, new ServiceConnectedEventArgs { Binder = service });

            // begin updating the location in the Service
            serviceBinder.Service.StartLocationUpdates();
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Binder.IsBound = false;
        }

        public event EventHandler<ServiceConnectedEventArgs> ServiceConnected;
    }

    public class ServiceConnectedEventArgs : EventArgs
    {
        public IBinder Binder { get; set; }
    }
}