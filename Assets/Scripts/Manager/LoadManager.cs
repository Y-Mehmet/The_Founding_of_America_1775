using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadManager : MonoBehaviour
{
 //   public GameObject loadingScreen;  // Yükleme ekraný paneli
    public Slider progressBar;        // Ýsteðe baðlý: Yükleme yüzdesini göstermek için

    void Start()
    {
        StartCoroutine(LoadGameData());
    }

    private IEnumerator LoadGameData()
    {
        // Yükleme ekranýný aktif hale getirin
        // loadingScreen.SetActive(true);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);

        // Simüle edilmiþ bir veri yükleme iþlemi (veritabaný ya da dosya okuma gibi)
        yield return SimulateDataLoading();

        // Veriler yüklendikten sonra yükleme ekranýný devre dýþý býrakýn
        // loadingScreen.SetActive(false);
       
    }

    private IEnumerator SimulateDataLoading()
    {
        // Gerçek bir veri yükleme iþlemini burada gerçekleþtirebilirsiniz
        float fakeProgress = 0f;

        // Yükleme ilerlemesini simüle etmek için bir döngü oluþturun
        while (fakeProgress < 1f)
        {
            fakeProgress += 0.1f; // Yükleme ilerlemesini arttýrýyoruz
            progressBar.value = fakeProgress; // Ýlerleme çubuðunu güncelliyoruz
            yield return new WaitForSeconds(0.2f); // Ýsteðe baðlý olarak bekleme süresi
        }
    }

}
