using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; // DoTween k�t�phanesini kullanmak i�in gerekli

public class SpinRoulette : MonoBehaviour
{
    public static SpinRoulette Instance {  get; private set; }
    public static bool  isSpin= false;
   
    public Button spinBtn;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
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
        
        if (!isSpin)
        {
            MissionsManager.AddTotalSpin(1);
            isSpin = true;
            // Rastgele bir z ekseni a��s� belirle
            float randomAngle = Random.Range(0, 8);

            // Toplam d�n�� a��s� (720 derece + rastgele a��)
            float targetAngle = 1440f + randomAngle * 45f;

            SoundManager.instance.Play("wheelspin", 3.5f);
            // D�nd�rme i�lemini ba�lat
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

                             case 4: SoundManager.instance.Play("Cash"); ResourceManager.Instance.AddResource(ResourceType.Gold, 7000); break;
                             case 5: SoundManager.instance.Play("GemCash"); ResourceManager.Instance.AddResource(ResourceType.Diamond, 100); break;
                             case 6: SoundManager.instance.Play("Cash"); ResourceManager.Instance.AddResource(ResourceType.Gold, 2500); break;

                             default: SoundManager.instance.Play("WheelNull"); break;

                         }
                         isSpin = false;
                     }); // H�zl� ba�lay�p yava�layan easing
         
        }
        else
            Debug.Log("is spin true");
    }
}
