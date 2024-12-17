using Unity.Notifications.Android; // Android bildirimleri için gerekli

using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bildirim yöneticisinin sahne deðiþimlerinde yok olmamasýný saðla
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
        // Android için bir bildirim kanalý oluþtur
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
            Title = "Commander, Your Free Spin Awaits!",
            Text = "The wheel is ready for you—claim your rewards now!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = System.DateTime.Now.AddSeconds(cooldownTime), // Cooldown süresine göre ayarla
        };

        AndroidNotificationCenter.SendNotification(notification, "spin_channel");
#endif


    }
}
