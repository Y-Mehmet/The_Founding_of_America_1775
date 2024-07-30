using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice2 : MonoBehaviour
{
    // Hedef konumu belirleyin
    public Transform target;
    public float zOffset = 30;
    public bool isRaceRoll = true;
    // Hareket süresi
    public float duration = 3f;
    Vector3 startPoint;
    // Sallama süresi
    public float shakeDuration = 1f;
    // Sallama gücü
    public float shakeStrength = 2f;
    // Sallama titreþim sayýsý
    public int vibrato = 20;
    // Rastgelelik miktarý
    public float randomness = 140f;
    void Start()
    {
        InvokeRepeating("CallRaceRoll",0 , duration + 3);

        startPoint = transform.position;
    }

    void CallRaceRoll()
    {
        transform.localScale = Vector3.one;
        // Kontrol noktalarýný belirleyin
        Vector3 midPoint = new Vector3((startPoint.x + target.position.x) / 2, (startPoint.y + target.position.y) / 2, (startPoint.z + zOffset)); // Yükselme noktasý
        Vector3 endPoint = target.position;
        

        Vector3[] path = { startPoint, midPoint, endPoint };

        // GameObject'i belirli bir konuma belirli bir sürede eðrisel bir hareketle hareket ettirme
        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                // Hareket tamamlandýðýnda boyutu iki katýna çýkarma
                transform.DOShakePosition(duration+1, shakeStrength, vibrato, randomness);
                transform.DOScale(10, duration+1); // Burada 1f, ölçeklenmenin süresini belirtir
            });
    }
}
