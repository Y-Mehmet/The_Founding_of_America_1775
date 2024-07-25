using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public int maxForceValue = 10; // Max kuvvet deðerini tanýmla
    private Rigidbody rb;
    private bool hasStopped = false; // Zar durduðunda true olacak
    public float delay = 2.0f; // Bekleme süresi
    public float threshold = 0.8f; // Eþik deðeri, zarýn hangi yüzünün üstte olduðunu belirlemek için kullanýlýr.

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
        StartCoroutine(CheckDiceStopped()); // Zar durduðunda yüz hesaplamasýný baþlatýr
    }

    public void AddForceForDice()
    {
        // Rastgele bir kuvvet vektörü oluþturun
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
        // Zarýn kendi uzayýndaki yukarý yönü
        Vector3 localUp = transform.TransformDirection(Vector3.up);

        // Yön vektörleri arasýndaki benzerliði (dot product) hesaplayýn.
        float maxDot = -1f;
        int topFaceIndex = -1;

        for (int i = 0; i < faceNormals.Length; i++)
        {
            float dot = Vector3.Dot(localUp, faceNormals[i]);
            if (dot > threshold && dot > maxDot) // Eþik deðerine bak
            {
                maxDot = dot;
                topFaceIndex = i;
            }
        }

        if (topFaceIndex != -1)
        {
            int topFaceValue = faceValues[topFaceIndex];
            Debug.Log("Üstteki yüz deðeri: " + topFaceValue + " - Obje: " + gameObject.name);
        }
        else
        {
            Debug.LogWarning("Üstteki yüz deðeri hesaplanamadý! Obje: " + gameObject.name);
        }
    }

    private IEnumerator CheckDiceStopped()
    {
        yield return new WaitForSeconds(delay);
        // Zarýn durup durmadýðýný kontrol eder
        while (!hasStopped)
        {
            if (rb.velocity.sqrMagnitude < 0.001f && rb.angularVelocity.sqrMagnitude < 0.001f)
            {
                hasStopped = true; // Zar durdu
                DiceFaceNumberCalculation(); // Üst yüzü hesapla
            }

            yield return new WaitForSeconds(0.1f); // 100ms bekle ve tekrar kontrol et
        }
    }
}
