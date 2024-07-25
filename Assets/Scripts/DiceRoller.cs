using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public int maxForceValue = 10; // Max kuvvet de�erini tan�mla
    private Rigidbody rb;
    private bool hasStopped = false; // Zar durdu�unda true olacak
    public float delay = 2.0f; // Bekleme s�resi
    public float threshold = 0.8f; // E�ik de�eri, zar�n hangi y�z�n�n �stte oldu�unu belirlemek i�in kullan�l�r.

    private Vector3[] faceNormals = new Vector3[]
    {
        Vector3.up,    // 2
        Vector3.down,  // 5
        Vector3.forward, // 6
        Vector3.back,  // 1
        Vector3.left,  // 3
        Vector3.right  // 4
    };

    private int[] faceValues = new int[] { 2, 5, 6, 1, 3, 4 };

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        AddForceForDice();
        StartCoroutine(CheckDiceStopped()); // Zar durdu�unda y�z hesaplamas�n� ba�lat�r
    }

    public void AddForceForDice()
    {
        // Rastgele bir kuvvet vekt�r� olu�turun
        float x = Random.Range(-maxForceValue, maxForceValue);
        float y = Random.Range(0, 10);
        float z = Random.Range(-maxForceValue, maxForceValue);

        Vector3 forceDirection = new Vector3(x, y, z);
       // print("Uygulanan Kuvvet: " + forceDirection);

        // Kuvveti zara uygula
        rb.AddForce(forceDirection, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * maxForceValue, ForceMode.Impulse); // Rastgele bir tork da uygula
    }

    void DiceFaceNumberCalculation()
    {
        // Zar�n kendi uzay�ndaki yukar� y�n�
        Vector3 localUp = transform.TransformDirection(Vector3.up);

        // Y�n vekt�rleri aras�ndaki benzerli�i (dot product) hesaplay�n.
        float maxDot = -1f;
        int topFaceIndex = -1;

        for (int i = 0; i < faceNormals.Length; i++)
        {
            float dot = Vector3.Dot(localUp, faceNormals[i]);
            if (dot > threshold && dot > maxDot) // E�ik de�erine bak
            {
                maxDot = dot;
                topFaceIndex = i;
            }
        }

        if (topFaceIndex != -1)
        {
            int topFaceValue = faceValues[topFaceIndex];
            Debug.Log("�stteki y�z de�eri: " + topFaceValue + " - Obje: " + gameObject.name);
        }
        else
        {
            Debug.LogWarning("�stteki y�z de�eri hesaplanamad�! Obje: " + gameObject.name);
        }
    }

    private IEnumerator CheckDiceStopped()
    {
        yield return new WaitForSeconds(delay);
        // Zar�n durup durmad���n� kontrol eder
        while (!hasStopped)
        {
            if (rb.velocity.sqrMagnitude < 0.001f && rb.angularVelocity.sqrMagnitude < 0.001f)
            {
                hasStopped = true; // Zar durdu
                DiceFaceNumberCalculation(); // �st y�z� hesapla
            }

            yield return new WaitForSeconds(0.1f); // 100ms bekle ve tekrar kontrol et
        }
    }
}
