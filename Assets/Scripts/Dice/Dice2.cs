using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice2 : MonoBehaviour
{
    public Vector3 target;
    public float zOffset = -30;
    public bool isRaceRoll = true;
    float duration;
    Vector3 startPoint;
    public float shakeStrength = 1f;
    public int vibrato = 10;
    public float randomness = 90;
    Vector3 firstScale;
    float shakingDuration = 0.50f;

    void Start()
    {
        float firstScaleValue = DiceSpawnner2.Instance.firstScale;
        firstScale = Vector3.one * firstScaleValue;

        transform.localScale = firstScale;
        startPoint = transform.position;
        duration = Attack.Instance.attackDuration;
    }

    private void Update()
    {
        if (transform.localScale.x < firstScale.x)
        {
            Debug.LogWarning(" küçülüuor " + firstScale);
        }
    }

    public void DiceRoll()
    {
        if (target != null)
        {
            transform.localScale = firstScale;
            Vector3 midPoint = new Vector3((startPoint.x + target.x) / 2, (startPoint.y + target.y) / 2, (startPoint.z + zOffset));
            Vector3 endPoint = target;

            Vector3[] path = { startPoint, midPoint, endPoint };

            transform.DOPath(path, duration, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .OnComplete(() => { });
            transform.DOScale(DiceSpawnner2.Instance.firstScale * 10, duration + 1);
        }
        else
            Debug.LogWarning("target is null");
    }

    public void DiceMoveForFight(GameObject targetDice, float waitingTime)
    {
       // Debug.LogWarning("dice move for fight çalýþtý");
        StartCoroutine(StartDiceMoveForFight(targetDice, waitingTime));
    }

    public IEnumerator StartDiceMoveForFight(GameObject targetDice, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
      //  Debug.LogWarning("name move çalýþtý " + gameObject.name);
        Vector3 targetPos = targetDice.transform.position;

        // Hedefe çarpýþma yönünde vektör hesapla
        Vector3 collisionDirection = (targetPos - transform.position).normalized;
        float collisionDistance = 102.0f; // Çarpýþma sonrasý hedef objenin hareket edeceði mesafe

        // Hedef objenin boyutunu hesapla
        Vector3 targetSize = targetDice.GetComponent<Renderer>().bounds.size;
        Vector3 targetOffset = collisionDirection * targetSize.magnitude / 2;

        // Yeni pozisyonu belirle
        Vector3 newTargetPosition = targetPos + collisionDirection * collisionDistance;

        transform.DOMove(targetPos - targetOffset, (shakingDuration / 2)).SetEase(Ease.InQuad).OnComplete(() =>
        {
            // Hedef objeyi çarpýþma yönünde hareket ettir
            targetDice.transform.DOMove(newTargetPosition, shakingDuration / 2);
            transform.DOMove((targetPos ), shakingDuration / 4).OnComplete(() => { SoundManager.instance.Play("DiceFight"); });
        });
    }

    public IEnumerator StartDiceShackForDrawWar(GameObject targetDice, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        // Ýki objenin tam ortasýný hesapla
        Vector3 midPoint = (transform.position + targetDice.transform.position) / 2;

        // Ýki objeyi ortada buluþtur
        transform.DOMove(midPoint, shakingDuration / 2).SetEase(Ease.Linear);
        targetDice.transform.DOMove(midPoint, shakingDuration / 2).SetEase(Ease.Linear).OnComplete(() =>
        {
          { SoundManager.instance.Play("DiceFight"); }
            // Çarpýþmadan sonra zýt yönlere savur
            Vector3 dir1 = (transform.position - targetDice.transform.position).normalized;
            Vector3 dir2 = (targetDice.transform.position - transform.position).normalized;
            float savrulmaMesafesi = 202.0f; // Savrulma mesafesi
            transform.DOMove(transform.position + dir1 * savrulmaMesafesi, shakingDuration / 2).SetEase(Ease.OutQuad);
            targetDice.transform.DOMove(targetDice.transform.position + dir2 * savrulmaMesafesi, shakingDuration / 2).SetEase(Ease.OutQuad);
        });
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
