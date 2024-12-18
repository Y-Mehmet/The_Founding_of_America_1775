using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro; // TextMeshPro için gerekli

public class SpinRoulette : MonoBehaviour
{
    public static SpinRoulette Instance { get; private set; }
    public static bool isSpin = false;

    public Button spinBtn;
    public TextMeshProUGUI spinBtnText; // Butonun altýnda kalan süreyi gösterecek TextMeshPro
   public static int cooldownTime = 3600; // 1 saat = 3600 saniye
    private float remainingTime;
    public static string LastSpinDate;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

 
    private void Update()
    {
        UpdateSpinCooldown();
    }

    private void OnEnable()
    {
        CheckSpinAvailability();
        spinBtn.onClick.AddListener(SpinWheel);
    }

    private void OnDisable()
    {
        spinBtn.onClick.RemoveListener(SpinWheel);
    }

    void SpinWheel()
    {
        if (!isSpin && remainingTime <= 0)
        {
            MissionsManager.AddTotalSpin(1);
            isSpin = true;

            // Spin zamaný kaydet
           
            LastSpinDate = System.DateTime.UtcNow.ToString();
            // Spin cooldown bittikten sonra bildirim planla
            NotificationManager.Instance.ScheduleSpinNotification(cooldownTime);
            // Spin zamaný kaydet
        

            float randomAngle = Random.Range(0, 8);
            float targetAngle = 1440f + randomAngle * 45f;

            SoundManager.instance.Play("wheelspin", 3.5f);
            transform.DORotate(new Vector3(0, 0, targetAngle), 6f, RotateMode.FastBeyond360)
                     .SetEase(Ease.OutQuad).OnComplete(() =>
                     {
                         Debug.Log("random sayý " + randomAngle);
                         switch (randomAngle)
                         {
                             case 0:
                                 SoundManager.instance.Play("Cash");
                                 ResourceManager.Instance.AddResource(ResourceType.Gold, 1000);
                                 break;
                             case 1:
                                 SoundManager.instance.Play("GemCash");
                                 ResourceManager.Instance.AddResource(ResourceType.Diamond, 10);
                                 break;
                             case 2:
                                 SoundManager.instance.Play("Cash");
                                 ResourceManager.Instance.AddResource(ResourceType.Gold, 5500);
                                 break;
                             case 4:
                                 SoundManager.instance.Play("Cash");
                                 ResourceManager.Instance.AddResource(ResourceType.Gold, 7000);
                                 break;
                             case 5:
                                 SoundManager.instance.Play("GemCash");
                                 ResourceManager.Instance.AddResource(ResourceType.Diamond, 100);
                                 break;
                             case 6:
                                 SoundManager.instance.Play("Cash");
                                 ResourceManager.Instance.AddResource(ResourceType.Gold, 2500);
                                 break;
                             default:
                                 SoundManager.instance.Play("WheelNull");
                                 break;
                         }
                         isSpin = false;
                         CheckSpinAvailability();
                     });
        }
        else
        {
            Debug.Log("Spin cooldown devam ediyor!");
        }
    }

    void CheckSpinAvailability()
    {
        if (!string.IsNullOrEmpty(LastSpinDate))
        {
            Debug.LogError("last spind isnt null");
            System.DateTime lastSpinTime = System.DateTime.Parse(LastSpinDate);
            System.TimeSpan timeSinceLastSpin = System.DateTime.UtcNow - lastSpinTime;
            remainingTime = Mathf.Max(0, cooldownTime - (float)timeSinceLastSpin.TotalSeconds);
        }
        else
        {
            Debug.LogError("last spind is null  ");
            remainingTime = cooldownTime;
        }

        UpdateSpinButtonState();
    }


    void UpdateSpinCooldown()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            spinBtnText.text = $"{minutes:D2}:{seconds:D2}"; // Kalan süreyi MM:SS formatýnda göster
        }
        else
        {
            spinBtnText.text = "Spin!";
            remainingTime = 0;
        }

        UpdateSpinButtonState();
    }

    void UpdateSpinButtonState()
    {
        spinBtn.interactable = remainingTime <= 0;
    }
}
