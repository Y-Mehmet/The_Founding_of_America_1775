using Unity.Notifications.Android; // Android bildirimleri i�in gerekli

using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bildirim y�neticisinin sahne de�i�imlerinde yok olmamas�n� sa�la
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeNotificationChannel();
    }

    private void InitializeNotificationChannel()
    {
#if UNITY_ANDROID
        // Android i�in bir bildirim kanal� olu�tur
        var channel = new AndroidNotificationChannel()
        {
            Id = "spin_channel",
            Name = "Spin Notifications",
            Importance = Importance.Default,
            Description = "Free Spin!",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
#endif
    }

    public void ScheduleSpinNotification(int cooldownTime)
    {
        // Android Bildirimi Planlama
#if UNITY_ANDROID
        var notification = new AndroidNotification
        {
            Title = "Commander, Your National Lottery Revenue Awaits!",
            Text = "The treasury is full! Claim your share of the lottery income now!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = System.DateTime.Now.AddSeconds(cooldownTime), // Cooldown s�resine g�re ayarla
        };

        AndroidNotificationCenter.SendNotification(notification, "spin_channel");
#endif


    }
}
