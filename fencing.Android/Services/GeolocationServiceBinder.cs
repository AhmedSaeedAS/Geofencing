using System;
using Android.OS;

namespace fencing.Droid.Services
{
    public class GeolocationServiceBinder : Binder
    {
        public GeolocationServiceBinder(GeolocationService service)
        {
            Service = service;
        }

        public GeolocationService Service { get; }

        public bool IsBound { get; set; }
    }
}
