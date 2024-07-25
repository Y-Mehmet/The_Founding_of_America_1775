using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DoTween k�t�phanesini kullanmak i�in

public class DiceRollerDO : MonoBehaviour
{
    public int maxForceValue = 1;  // Maksimum kuvvet de�eri
    private Rigidbody rb;
    public float rollDuration = 2.0f;  // Zar�n d�nme s�resi
    public float moveDistance = 0.5f;  // Zar�n �teleme mesafesi (D���k de�er)
    public Vector3 rotationAxis = Vector3.up; // D�n�� ekseni
    public bool IsPlayerDice = false;
    public int DiceValue;

    // Zar�n y�z de�erleri ve kar��l�k gelen d�n�� rotasyonlar�
    private Dictionary<int, Vector3> faceRotations = new Dictionary<int, Vector3>()
    {
        {2, new Vector3(0, 0, 0)},      // 1 numaral� y�z yukar� bak�yor
        {6, new Vector3(90, 0, 0)},     // 2 numaral� y�z yukar� bak�yor
        {4, new Vector3(0, 0, 90)},     // 3 numaral� y�z yukar� bak�yor
        {3, new Vector3(0, 0, -90)},    // 4 numaral� y�z yukar� bak�yor
        {1, new Vector3(-90, 0, 0)},    // 5 numaral� y�z yukar� bak�yor
        {5, new Vector3(180, 0, 0)}     // 6 numaral� y�z yukar� bak�yor
    };

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
       
    }

   

    public void RollDice()
    {
        int randomFace = Random.Range(1, 7);  // 1 ile 6 aras�nda rastgele bir say�
        DiceValue= randomFace;
        Vector3 targetRotation = faceRotations[randomFace];  // Hedef rotasyon

        // DoTween ile zar� d�nd�r
        transform.DORotate(targetRotation, rollDuration)
            .SetEase(Ease.OutBounce); // Yava�layarak durma efekti

        float finalMoveDistance = CalculateMoveDistance(); // Hareket mesafesini hesapla

        if (IsPlayerDice)
        {
            // DoTween ile zar� �tele
            transform.DOMoveX(transform.position.x + finalMoveDistance, rollDuration)
                .SetEase(Ease.InOutSine); // Daha do�al bir hareket efekti
        }
        else
        {
            // DoTween ile zar� �tele
            transform.DOMoveX(transform.position.x - finalMoveDistance, rollDuration)
                .SetEase(Ease.InOutSine);
        }

        Debug.Log("�stteki y�z de�eri: " + randomFace);
    }

    private float CalculateMoveDistance()
    {
        // Burada, hareket mesafesini azaltmak i�in bir mant�k kullanabilirsiniz
        // �rne�in, zar�n y�z de�erine veya di�er fakt�rlere g�re mesafeyi ayarlayabilirsiniz

        float minDistance = 0.2f; // Minimum hareket mesafesi
        float maxDistance = moveDistance; // Tan�mlanm�� maksimum mesafe

        // Rastgele bir fakt�r ekleyerek mesafeyi ayarlay�n
        float randomFactor = Random.Range(0.5f, 1.0f); // 0.5 ile 1.0 aras�nda bir �arpan

        return Mathf.Lerp(minDistance, maxDistance, randomFactor); // Mesafeyi ayarla
    }
}
