using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DoTween kütüphanesini kullanmak için

public class DiceRollerDO : MonoBehaviour
{
    public int maxForceValue = 1;  // Maksimum kuvvet deðeri
    private Rigidbody rb;
    public float rollDuration = 2.0f;  // Zarýn dönme süresi
    public float moveDistance = 0.5f;  // Zarýn öteleme mesafesi (Düþük deðer)
    public Vector3 rotationAxis = Vector3.up; // Dönüþ ekseni
    public bool IsPlayerDice = false;
    public int DiceValue;

    // Zarýn yüz deðerleri ve karþýlýk gelen dönüþ rotasyonlarý
    private Dictionary<int, Vector3> faceRotations = new Dictionary<int, Vector3>()
    {
        {2, new Vector3(0, 0, 0)},      // 1 numaralý yüz yukarý bakýyor
        {6, new Vector3(90, 0, 0)},     // 2 numaralý yüz yukarý bakýyor
        {4, new Vector3(0, 0, 90)},     // 3 numaralý yüz yukarý bakýyor
        {3, new Vector3(0, 0, -90)},    // 4 numaralý yüz yukarý bakýyor
        {1, new Vector3(-90, 0, 0)},    // 5 numaralý yüz yukarý bakýyor
        {5, new Vector3(180, 0, 0)}     // 6 numaralý yüz yukarý bakýyor
    };

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
       
    }

   

    public void RollDice()
    {
        int randomFace = Random.Range(1, 7);  // 1 ile 6 arasýnda rastgele bir sayý
        DiceValue= randomFace;
        Vector3 targetRotation = faceRotations[randomFace];  // Hedef rotasyon

        // DoTween ile zarý döndür
        transform.DORotate(targetRotation, rollDuration)
            .SetEase(Ease.OutBounce); // Yavaþlayarak durma efekti

        float finalMoveDistance = CalculateMoveDistance(); // Hareket mesafesini hesapla

        if (IsPlayerDice)
        {
            // DoTween ile zarý ötele
            transform.DOMoveX(transform.position.x + finalMoveDistance, rollDuration)
                .SetEase(Ease.InOutSine); // Daha doðal bir hareket efekti
        }
        else
        {
            // DoTween ile zarý ötele
            transform.DOMoveX(transform.position.x - finalMoveDistance, rollDuration)
                .SetEase(Ease.InOutSine);
        }

        Debug.Log("Üstteki yüz deðeri: " + randomFace);
    }

    private float CalculateMoveDistance()
    {
        // Burada, hareket mesafesini azaltmak için bir mantýk kullanabilirsiniz
        // Örneðin, zarýn yüz deðerine veya diðer faktörlere göre mesafeyi ayarlayabilirsiniz

        float minDistance = 0.2f; // Minimum hareket mesafesi
        float maxDistance = moveDistance; // Tanýmlanmýþ maksimum mesafe

        // Rastgele bir faktör ekleyerek mesafeyi ayarlayýn
        float randomFactor = Random.Range(0.5f, 1.0f); // 0.5 ile 1.0 arasýnda bir çarpan

        return Mathf.Lerp(minDistance, maxDistance, randomFactor); // Mesafeyi ayarla
    }
}
