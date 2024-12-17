using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro; // TextMeshPro i�in gerekli

public class SpinRoulette : MonoBehaviour
{
    public static SpinRoulette Instance { get; private set; }
    public static bool isSpin = false;

    public Button spinBtn;
    public TextMeshProUGUI spinBtnText; // Butonun alt�nda kalan s�reyi g�sterecek TextMeshPro
    int cooldownTime = 3600; // 1 saat = 3600 saniye
    private float remainingTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    private void Start()
    {
        CheckSpinAvailability();
    }

    private void Update()
    {
        UpdateSpinCooldown();
    }

    private void OnEnable()
    {
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
            // Spin i�lemini ger�ekle�tir
            MissionsManager.AddTotalSpin(1);
            isSpin = true;

            // Spin zaman� kaydet
            PlayerPrefs.SetString("LastSpinTime", System.DateTime.UtcNow.ToString());
            PlayerPrefs.Save();

            float randomAngle = Random.Range(0, 8);
            float targetAngle = 1440f + randomAngle * 45f;

            SoundManager.instance.Play("wheelspin", 3.5f);
            transform.DORotate(new Vector3(0, 0, targetAngle), 6f, RotateMode.FastBeyond360)
                     .SetEase(Ease.OutQuad).OnComplete(() =>
                     {
                         Debug.Log("random say� " + randomAngle);
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
        string lastSpinTimeStr = PlayerPrefs.GetString("LastSpinTime", "");
        if (!string.IsNullOrEmpty(lastSpinTimeStr))
        {
            System.DateTime lastSpinTime = System.DateTime.Parse(lastSpinTimeStr);
            System.TimeSpan timeSinceLastSpin = System.DateTime.UtcNow - lastSpinTime;
            remainingTime = Mathf.Max(0, cooldownTime - (float)timeSinceLastSpin.TotalSeconds);
        }
        else
        {
            remainingTime = 0;
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
            spinBtnText.text = $"{minutes:D2}:{seconds:D2}"; // Kalan s�reyi MM:SS format�nda g�ster
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
