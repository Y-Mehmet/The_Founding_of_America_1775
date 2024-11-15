using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; // DoTween kütüphanesini kullanmak için gerekli

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
           
            isSpin = true;
            // Rastgele bir z ekseni açýsý belirle
            float randomAngle = Random.Range(0, 8);

            // Toplam dönüþ açýsý (720 derece + rastgele açý)
            float targetAngle = 1440f + randomAngle * 45f;

            SoundManager.instance.Play("wheelspin", 3.5f);
            // Döndürme iþlemini baþlat
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

                             case 4: SoundManager.instance.Play("Cash"); ResourceManager.Instance.AddResource(ResourceType.Gold, 7000); break;
                             case 5: SoundManager.instance.Play("GemCash"); ResourceManager.Instance.AddResource(ResourceType.Diamond, 100); break;
                             case 6: SoundManager.instance.Play("Cash"); ResourceManager.Instance.AddResource(ResourceType.Gold, 2500); break;

                             default: SoundManager.instance.Play("WheelNull"); break;

                         }
                         isSpin = false;
                     }); // Hýzlý baþlayýp yavaþlayan easing
         
        }
        else
            Debug.Log("is spin true");
    }
}
