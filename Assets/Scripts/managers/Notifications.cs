using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class Notifications : MonoBehaviour
{
    public AndroidNotificationChannel defaultNotificationChannel;
    private int identifier;
    // Start is called before the first frame update
    void Start()
    {
        defaultNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Description = "For Generic notifications",
            Importance = Importance.Default,
        };
    AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Solve the Mystery!",
            Text = "The murder case remain unsolved , start right where you left off",
            SmallIcon = "Icon",
            LargeIcon = "icon_1",
            FireTime = System.DateTime.Now.AddMinutes(30),
    };
        identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");
}

}
