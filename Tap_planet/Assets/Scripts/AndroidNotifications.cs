using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidNotifications : MonoBehaviour
{
    private static AndroidNotifications instance;

    void Awake()
    {
        if (PlayerPrefs.GetInt("VacationMode") == 0)
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            Destroy(gameObject);
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Raid alert",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        // a notification should look like this:
        // Tap Planet
        // Raid Alert!
        // Quick! You have 15 minutes to
        // enter and defend your planet!
    }

    void OnApplicationFocus(bool focus)
    {
        if (PlayerPrefs.GetInt("VacationMode") == 0)
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            Destroy(gameObject);
        }

        if (focus)
        {
            // removes displayed notifications
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
        }
        else
        {
            var notification = new AndroidNotification();   // create new notification
            notification.Title = "Raid Alert!";     // Header
            notification.Text = "Quick! You have 15 minutes to enter and defend your planet!";     // text

            int rnd = Random.Range(30, 35);
            PlayerPrefs.SetInt("timeBeforeRaid", rnd);
            notification.FireTime = System.DateTime.Now.AddSeconds(rnd);
            
            var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");     // sends notification and stores it 

            if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled) // checks if already scheduled 
            {
                AndroidNotificationCenter.CancelAllNotifications();     // delete already sceduled
                AndroidNotificationCenter.SendNotification(notification, "channel_id");     // schedule new notification
            }
        }
    }
}
