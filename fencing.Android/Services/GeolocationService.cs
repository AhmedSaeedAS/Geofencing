using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;

namespace fencing.Droid.Services
{
    [Service]
    public class GeolocationService : Service
    {
        IBinder binder;

        public GeofencingHelper GeofencingHelperInstance { get; private set; }

        public static string NotificationChannelId { get; set; } = "cloudmatic-geofencing-notification-channel-id";
        public static string NotificationChannelName { get; set; } = "CloudMatic Geofencing Meldungen";
        static bool _channelCreated;

        public override IBinder OnBind(Intent intent)
        {
            binder = new GeolocationServiceBinder(this);
            return binder;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var builder = new NotificationCompat.Builder(this, NotificationChannelId);

            var newIntent = new Intent(this, typeof(MainActivity));
            newIntent.PutExtra("tracking", true);
            newIntent.AddFlags(ActivityFlags.ClearTop);
            newIntent.AddFlags(ActivityFlags.SingleTop);

            var pendingIntent = PendingIntent.GetActivity(this, 0, newIntent, 0);
            var notification = builder.SetContentIntent(pendingIntent)
                                      .SetSmallIcon(Resource.Drawable.nw_300_push_notifications)
                                      .SetAutoCancel(true)
                                      .SetTicker("Geofencing ist aktiv")
                                      .SetContentTitle("Geofencing")
                                      .SetContentText("Geofencing ist aktiv")
                                      .Build();

            //notification.Flags = NotificationFlags.AutoCancel;

            NotificationManager notificationManager = GetSystemService(NotificationService) as NotificationManager;
            //if (Build.VERSION.SdkInt >= BuildVersionCodes.O || !_channelCreated)
            //{
            //    var channel = new NotificationChannel(NotificationChannelId, NotificationChannelName, NotificationImportance.None);
            //    channel.EnableLights(true);
            //    channel.EnableVibration(true);
            //    notificationManager.CreateNotificationChannel(channel);
            //    _channelCreated = true;
            //}

            StartForeground((int)NotificationFlags.ForegroundService, notification);


            GeofencingHelperInstance = GeofencingHelper.Current;

            return StartCommandResult.Sticky;
        }

        public void StartLocationUpdates()
        {
            GeofencingHelperInstance.StartGeofenceTracking();
        }

        public void StopLocationUpdates()
        {
            GeofencingHelperInstance.StopGeofenceTracking();
        }
    }
}