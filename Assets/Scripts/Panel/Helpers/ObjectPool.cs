using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ObjectPool s�n�f� Singleton tasar�m kal�b�n� kullanarak bir nesne havuzu olu�turur
public class ObjectPool : Singleton<ObjectPool>
{
    // Havuzda kullan�lacak prefab nesnelerinin listesi
    public List<GameObject> PrefabsForPool;


    // Havuzda tutulan nesnelerin listesi
    
    private  List<GameObject> _pooledObjects = new List<GameObject>();

    // Belirtilen ada sahip bir nesneyi havuzdan al�r
    public GameObject GetObjectFromPool(string objectName)
    {
        // Havuzda istenilen ad� ta��yan bir nesne olup olmad���n� kontrol et
        var instance = _pooledObjects.FirstOrDefault(o => o.name == objectName);

        // E�er nesne havuzda varsa, nesneyi havuzdan kald�r, aktif hale getir ve d�nd�r
        if (instance != null)
        {
            _pooledObjects.Remove(instance);
            instance.SetActive(true); // Nesneyi aktif hale getir
            return instance;
        }

        // E�er havuzda istenen nesne yoksa, prefab listesinden nesne bul ve olu�tur
        var prefab = PrefabsForPool.FirstOrDefault(o => o.name == objectName);

        if (prefab != null)
        {
            // Yeni bir prefab �rne�i olu�tur, pozisyon ve d�n��� ayarla
            var newInstace = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);

            // Yeni �rne�i varsay�lan pozisyona ayarla ve ad�n� belirle
            newInstace.transform.localPosition = Vector3.zero;
            newInstace.name = objectName;

            return newInstace; // Yeni olu�turulan nesneyi d�nd�r
        }

        // E�er istenen ada sahip bir prefab yoksa, uyar� mesaj� yazd�r
        Debug.LogWarning($"{objectName} ad�nda bir prefab bulunamad�");

        return null; // E�er uygun bir nesne bulunamazsa null d�nd�r
    }

    // Belirtilen nesneyi tekrar havuza ekler
    public void PoolObject(GameObject gameObject)
    {
        gameObject.SetActive(false); // Nesneyi devre d��� b�rak
        _pooledObjects.Add(gameObject); // Nesneyi havuza ekle
    }
}
