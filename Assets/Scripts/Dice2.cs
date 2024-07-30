using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice2 : MonoBehaviour
{
    // Hedef konumu belirleyin
    public Vector3 target;
    public float zOffset = -30;
    public bool isRaceRoll = true;
    // Hareket s�resi
     float duration = 1f;
    Vector3 startPoint;
    // Sallama s�resi
    public float shakeDuration = 1f;
    // Sallama g�c�
    public float shakeStrength = 1f;
    // Sallama titre�im say�s�
    public int vibrato = 10;
    // Rastgelelik miktar�
    public float randomness = 90;
    Vector3 firstScale;
    void Start()
    {
        firstScale= new Vector3(0.035f, 0.035f, 0.035f);
       

        startPoint = transform.position;

    }

    public void DiceRoll()
    {
        if (target != null)
        {
            transform.localScale = firstScale;
            // Kontrol noktalar�n� belirleyin
            Vector3 midPoint = new Vector3((startPoint.x + target.x) / 2, (startPoint.y + target.y) / 2, (startPoint.z + zOffset)); // Y�kselme noktas�
            Vector3 endPoint = target;


            Vector3[] path = { startPoint, midPoint, endPoint };

            // GameObject'i belirli bir konuma belirli bir s�rede e�risel bir hareketle hareket ettirme
            transform.DOPath(path, duration, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // Hareket tamamland���nda boyutu iki kat�na ��karma
                    //  transform.DOShakeRotation(duration+1, shakeStrength, vibrato, randomness);
                    
                });
            transform.DOScale(firstScale.x * 10, duration + 1); // Burada 1f, �l�eklenmenin s�resini belirtir
        }
        else
            Debug.LogWarning("target is null");
    }
}
