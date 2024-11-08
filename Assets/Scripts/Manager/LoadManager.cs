using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadManager : MonoBehaviour
{
 //   public GameObject loadingScreen;  // Y�kleme ekran� paneli
    public Slider progressBar;        // �ste�e ba�l�: Y�kleme y�zdesini g�stermek i�in

    void Start()
    {
        StartCoroutine(LoadGameData());
    }

    private IEnumerator LoadGameData()
    {
        // Y�kleme ekran�n� aktif hale getirin
        // loadingScreen.SetActive(true);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);

        // Sim�le edilmi� bir veri y�kleme i�lemi (veritaban� ya da dosya okuma gibi)
        yield return SimulateDataLoading();

        // Veriler y�klendikten sonra y�kleme ekran�n� devre d��� b�rak�n
        // loadingScreen.SetActive(false);
       
    }

    private IEnumerator SimulateDataLoading()
    {
        // Ger�ek bir veri y�kleme i�lemini burada ger�ekle�tirebilirsiniz
        float fakeProgress = 0f;

        // Y�kleme ilerlemesini sim�le etmek i�in bir d�ng� olu�turun
        while (fakeProgress < 1f)
        {
            fakeProgress += 0.1f; // Y�kleme ilerlemesini artt�r�yoruz
            progressBar.value = fakeProgress; // �lerleme �ubu�unu g�ncelliyoruz
            yield return new WaitForSeconds(0.2f); // �ste�e ba�l� olarak bekleme s�resi
        }
    }

}
