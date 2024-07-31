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
     float duration ;
    Vector3 startPoint;
    // Sallama s�resi
    
    // Sallama g�c�
    public float shakeStrength = 1f;
    // Sallama titre�im say�s�
    public int vibrato = 10;
    // Rastgelelik miktar�
    public float randomness = 90;
    Vector3 firstScale;
    float shakingDuration = 1.0f;
    void Start()
    {
        float firstScaleValue = DiceSpawnner2.Instance.firstScale;
        firstScale = Vector3.one * firstScaleValue;
       
        transform.localScale= firstScale;
        startPoint = transform.position;
        duration = Attack.Instance.attackDuration;

    }
    private void Update()
    {
        if(transform.localScale.x<firstScale.x)
        {
            Debug.LogWarning(" k���l�uor "+firstScale);
        }
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
                    
                   //glb

                });
            transform.DOScale(DiceSpawnner2.Instance.firstScale * 10, duration + 1); // Burada 1f, �l�eklenmenin s�resini belirtir
        }
        else
            Debug.LogWarning("target is null");
    }
    public void DiceMoveForFight(GameObject targetDice , float waitingTime)
    {
        Debug.LogWarning("dice move for fight �al���t");
        StartCoroutine(StartDiceMoveForFight(targetDice, waitingTime));

    }
    public IEnumerator StartDiceMoveForFight(GameObject targetDice, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        Debug.LogWarning("name move �al��t� " + gameObject.name);


        transform.DOShakeRotation(shakingDuration);

        transform.DOMove(targetDice.transform.position, (shakingDuration/2)).OnComplete(() =>
        {
            
            targetDice.SetActive(false);
        });

    }
    public Transform GetTransform()
    {
        return transform;
    }
}
