using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ObjectPool sýnýfý Singleton tasarým kalýbýný kullanarak bir nesne havuzu oluþturur
public class ObjectPool : Singleton<ObjectPool>
{
    // Havuzda kullanýlacak prefab nesnelerinin listesi
    public List<GameObject> PrefabsForPool;


    // Havuzda tutulan nesnelerin listesi
    
      List<GameObject> _pooledObjects = new List<GameObject>();

    // Belirtilen ada sahip bir nesneyi havuzdan alýr
    public GameObject GetObjectFromPool(string objectName)
    {
        // Havuzda istenilen adý taþýyan bir nesne olup olmadýðýný kontrol et
        var instance = _pooledObjects.FirstOrDefault(o => o.name == objectName);

        // Eðer nesne havuzda varsa, nesneyi havuzdan kaldýr, aktif hale getir ve döndür
        if (instance != null)
        {
            _pooledObjects.Remove(instance);
            instance.SetActive(true); // Nesneyi aktif hale getir
            return instance;
        }

        // Eðer havuzda istenen nesne yoksa, prefab listesinden nesne bul ve oluþtur
        var prefab = PrefabsForPool.FirstOrDefault(o => o.name == objectName);

        if (prefab != null)
        {
            // Yeni bir prefab örneði oluþtur, pozisyon ve dönüþü ayarla
            var newInstace = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);

            // Yeni örneði varsayýlan pozisyona ayarla ve adýný belirle
            newInstace.transform.localPosition = Vector3.zero;
            newInstace.name = objectName;

            return newInstace; // Yeni oluþturulan nesneyi döndür
        }

        // Eðer istenen ada sahip bir prefab yoksa, uyarý mesajý yazdýr
        Debug.LogWarning($"{objectName} adýnda bir prefab bulunamadý");

        return null; // Eðer uygun bir nesne bulunamazsa null döndür
    }

    // Belirtilen nesneyi tekrar havuza ekler
    public void PoolObject(GameObject gameObject)
    {
        gameObject.SetActive(false); // Nesneyi devre dýþý býrak
        _pooledObjects.Add(gameObject); // Nesneyi havuza ekle
    }
}
