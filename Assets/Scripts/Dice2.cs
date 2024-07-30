using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice2 : MonoBehaviour
{
    // Hedef konumu belirleyin
    public Transform target;
    public float zOffset = -30;
    public bool isRaceRoll = true;
    // Hareket süresi
     float duration = 1f;
    Vector3 startPoint;
    // Sallama süresi
    public float shakeDuration = 1f;
    // Sallama gücü
    public float shakeStrength = 1f;
    // Sallama titreþim sayýsý
    public int vibrato = 10;
    // Rastgelelik miktarý
    public float randomness = 90;
    Vector3 firstScale;
    void Start()
    {
        firstScale= transform.localScale;
        InvokeRepeating("CallRaceRoll",0 , duration + 3);

        startPoint = transform.position;

    }

    void CallRaceRoll()
    {
        transform.localScale = firstScale;
        // Kontrol noktalarýný belirleyin
        Vector3 midPoint = new Vector3((startPoint.x + target.position.x) / 2, (startPoint.y + target.position.y) / 2, (startPoint.z + zOffset)); // Yükselme noktasý
        Vector3 endPoint = target.position;
        

        Vector3[] path = { startPoint, midPoint, endPoint };

        // GameObject'i belirli bir konuma belirli bir sürede eðrisel bir hareketle hareket ettirme
        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                // Hareket tamamlandýðýnda boyutu iki katýna çýkarma
                transform.DOShakeRotation(duration+1, shakeStrength, vibrato, randomness);
                transform.DOScale(firstScale.x*10, duration+1); // Burada 1f, ölçeklenmenin süresini belirtir
            });
    }
}
