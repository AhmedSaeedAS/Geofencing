using System.Linq;
using Plugin.Geofencing;
using Xamarin.Forms;
using Plugin.LocalNotifications;

namespace fencing
{
    public class GeofencingHelper
    {
        bool isMonitoring = false;

        static GeofencingHelper _instance = null;
        public static GeofencingHelper Current => _instance = _instance ?? new GeofencingHelper();

        GeofencingHelper()
        { }

        void Current_RegionStatusChanged(object sender, GeofenceStatusChangedEventArgs e)
        {
            var geofencePlaceId = e.Region.Identifier;
            var entered = e.Status == GeofenceStatus.Entered;
            // Do whatever you want with the status

            CrossLocalNotifications.Current.Show("Office Location", e.Status.ToString());
            //((App)App.Current).ShowAlerts(sender, e);
        }

        public void StartGeofenceTracking()
        {
            //if (isMonitoring || !CrossGeofences.Current.MonitoredRegions.Any())
                //return;

            CrossGeofences.Current.RegionStatusChanged += Current_RegionStatusChanged;
            MessagingCenter.Send("", "StartGeofencingService", "");

            isMonitoring = true;
        }

        public void StopGeofenceTracking()
        {
            CrossGeofences.Current.RegionStatusChanged -= Current_RegionStatusChanged;
            MessagingCenter.Send("", "StopGeofencingService", "");
            isMonitoring = false;
        }
    }
}