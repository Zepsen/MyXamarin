using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Xamarin.Forms;

using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Newtonsoft.Json;


namespace Randevy.Droid.Services.Notifications
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class PushMessagingService : FirebaseMessagingService
    {
        #region -- Overrides --

        public override void OnMessageReceived(RemoteMessage message)
        {
            SendNotification(message);
        }

        #endregion

        private async void SendNotification(RemoteMessage message)
        {
            var messageData = message.GetNotification();
            var intent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
            
            intent.AddFlags(ActivityFlags.ClearTop);

            var notificationBuilder = new Notification.Builder(this)
                //.SetSmallIcon(Resource.Drawable.Icon)
                .SetContentTitle(messageData.Title)
                .SetContentText(messageData.Body)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            if (messageData.Sound != null)
            {
                notificationBuilder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));
            }

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}