using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Plugin.Permissions;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace fencing.Droid
{
    [Activity(Label = "fencing", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.nw_300_push_notifications;
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            MessagingCenter.Subscribe<string, string>(string.Empty, "StartGeofencingService", StartGeofencingService);
            MessagingCenter.Subscribe<string, string>(string.Empty, "StopGeofencingService", StopGeofencingService);

            TryToGetPermissions();
        }

        public void StartGeofencingService(object sender, string whatEver)
        {
            Services.GeolocationHelper.StartLocationService();
        }

        public void StopGeofencingService(object sender, string whatEver)
        {
            Services.GeolocationHelper.StopLocationService();
        }

        #region RuntimePermissions 
        private void TryToGetPermissions()         {             if ((int)Build.VERSION.SdkInt >= 23)             {                 GetPermissions();                 return;             }           }          private const int RequestLocationId = 0;         private readonly string[] PermissionsGroupLocation =             {                             //TODO add more permissions                             Manifest.Permission.AccessCoarseLocation,                             Manifest.Permission.AccessFineLocation,                             Manifest.Permission.AccessNotificationPolicy,                             Manifest.Permission.BindNotificationListenerService              };          private void GetPermissions()         {             const string permission = Manifest.Permission.AccessFineLocation;              if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)             {
                //TODO change the message to show the permissions name
                Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();                 return;             }              if (ShouldShowRequestPermissionRationale(permission))             {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);                 alert.SetTitle("Permissions Needed");                 alert.SetMessage("The application need special permissions to continue");                 alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>                 {                     RequestPermissions(PermissionsGroupLocation, RequestLocationId);                 });                  alert.SetNegativeButton("Cancel", (senderAlert, args) =>                 {                     Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();                 });                  Dialog dialog = alert.Create();                 dialog.Show();                   return;             }              RequestPermissions(PermissionsGroupLocation, RequestLocationId);          }         public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)         {             switch (requestCode)             {                 case RequestLocationId:                     {                         if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)                         {                             Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();                          }                         else                         {
                            //Permission Denied :(
                            Toast.MakeText(this, "Special permissions denied", ToastLength.Short).Show();                          }                     }                     break;             }
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion
     } } 
