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
    // Hareket s�resi
    public float duration = 3f;
    Vector3 startPoint;
    // Sallama s�resi
    public float shakeDuration = 1f;
    // Sallama g�c�
    public float shakeStrength = 2f;
    // Sallama titre�im say�s�
    public int vibrato = 20;
    // Rastgelelik miktar�
    public float randomness = 140f;
    void Start()
    {
        InvokeRepeating("CallRaceRoll",0 , duration + 3);

        startPoint = transform.position;
    }

    void CallRaceRoll()
    {
        transform.localScale = Vector3.one;
        // Kontrol noktalar�n� belirleyin
        Vector3 midPoint = new Vector3((startPoint.x + target.position.x) / 2, (startPoint.y + target.position.y) / 2, (startPoint.z + zOffset)); // Y�kselme noktas�
        Vector3 endPoint = target.position;
        

        Vector3[] path = { startPoint, midPoint, endPoint };

        // GameObject'i belirli bir konuma belirli bir s�rede e�risel bir hareketle hareket ettirme
        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                // Hareket tamamland���nda boyutu iki kat�na ��karma
                transform.DOShakePosition(duration+1, shakeStrength, vibrato, randomness);
                transform.DOScale(10, duration+1); // Burada 1f, �l�eklenmenin s�resini belirtir
            });
    }
}
