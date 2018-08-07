using System;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geofencing;
using Plugin.Geolocator;
using Plugin.LocalNotifications;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace fencing
{
    public partial class App : Application
    {

        const string FENCE_ID = "My Office5";
        GeofenceRegion monitorLocation;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            startGeofencing();
        }

        private async Task startGeofencing()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationAlways))
                    {
                        Current.MainPage.DisplayAlert("Need location", "Gonna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationAlways);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.LocationAlways))
                        status = results[Permission.LocationAlways];
                }

                if (status == PermissionStatus.Granted)
                {
                    await setGeoFence();
                }
            }
            if (Device.RuntimePlatform == Device.Android)
            {
               await setGeoFence();
            }
        }

        public async Task setGeoFence()
        {
            CrossGeofences.Current.RegionStatusChanged += ShowAlerts;
            var monitoredRegions = CrossGeofences.Current.MonitoredRegions;
            if (monitoredRegions.Any(reg => reg.Identifier.Equals(FENCE_ID)))
            {
                CrossGeofences.Current.StopAllMonitoring();
            }

            var locator = CrossGeolocator.Current;
            try
            {
                var Center = new Position(24.8635707, 67.0753158);
                monitorLocation = new GeofenceRegion(FENCE_ID, Center, Distance.FromMeters(100));
            }

            catch (Exception ex)
            {
                var abc = ex;
            }

            if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
            {
                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //Current.MainPage.DisplayAlert("FENCE CREATED", string.Format("{0} {1}", 24.8635707, 67.0753158), "Cancel");
                        CrossGeofences.Current.StartMonitoring(monitorLocation);
                        GeofencingHelper.Current.StartGeofenceTracking();
                    });

                }
                catch (Exception ex)
                {
                    var exc = ex;
                }
            }

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes;
        }

        public void ShowAlerts(object sender, GeofenceStatusChangedEventArgs args) {
            //Current.MainPage.DisplayAlert("REGION ENTERED", args.Status.ToString(), "Cancel");
            CrossLocalNotifications.Current.Show("Office Location", args.Status.ToString());
        }
    }
}
//πSystem.ArgumentException: No AppLinks implementation was found, if in Android make sure you installed the Xamarin.Forms.AppLinks