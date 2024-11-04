using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// PanelID enum tanýmlanmýþ durumda
public class PanelManager : Singleton<PanelManager>
{
    // Panel örneklerini saklayan liste
    public List<PanelInstanceModel> _listInstance = new List<PanelInstanceModel>();

    // Nesne havuzu referansý
    private ObjectPool _objectPool;

    private void Start()
    {
        // Nesne havuzu örneðini al
        _objectPool = ObjectPool.Instance;
    }

    // Paneli gösterme fonksiyonu (enum kullanýmý ile güncellendi)
    public void ShowPanel(PanelID panelID, PanelShowBehavior behavior = PanelShowBehavior.SHOW_PREVISE)
    {
        if(panelID!=PanelID.HelpPanel)
        InfoManager.AddQueque( panelID);
        // Enum'dan string'e dönüþtürerek paneli al
        string panelIDString = panelID.ToString();

        // Panel havuzundan nesneyi al
        GameObject instancePanel = _objectPool.GetObjectFromPool(panelIDString);

        // Eðer panel nesnesi bulunduysa
        if (instancePanel != null)
        {
            // Önceki paneli gizleme davranýþý varsa ve aktif panel varsa
            if (behavior == PanelShowBehavior.HIDE_PREVISE && GetAmountPanelInList() > 0)
            {
                var lastPanel = GetLastPanel();
                if (lastPanel != null)
                {
                    lastPanel.PanelInstance.SetActive(false);
                }
            }

            // Yeni paneli listeye ekle
            _listInstance.Add(new PanelInstanceModel
            {
                PanelID = panelID, // Enum string olarak kaydedilir
                PanelInstance = instancePanel
            });
        }
        else
        {
            Debug.LogWarning($" panel bulunamadý {panelID}");
        }
    }

    // Son paneli gizleme fonksiyonu
    public void HideLastPanel()
    {
        if (AnyPanelIsShowing())
        {
            var lastPanel = GetLastPanel();
          
            _listInstance.Remove(lastPanel);
            _objectPool.PoolObject(lastPanel.PanelInstance);

            if (GetAmountPanelInList() > 0)
            {
                lastPanel = GetLastPanel();
               
                if (lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                {
                    lastPanel.PanelInstance.SetActive(true);
                }
            }
        }
        //else
           // Debug.LogWarning("hibir panel açýk deðil");
    }

    // Tüm panelleri gizleme fonksiyonu
    public void HideAllPanel()
    {
        while (AnyPanelIsShowing()) // Tüm panelleri gizleyene kadar devam et
        {
            var lastPanel = GetLastPanel();
            _listInstance.Remove(lastPanel);
            _objectPool.PoolObject(lastPanel.PanelInstance);
        }
    }

    // Liste içindeki son paneli döndürür
    PanelInstanceModel GetLastPanel()
    {
        if (_listInstance.Count == 0)
        {
            return null;
        }
        return _listInstance[_listInstance.Count - 1];
    }

    // Gösterimde herhangi bir panel var mý kontrol eder
    public bool AnyPanelIsShowing()
    {
        return GetAmountPanelInList() > 0;
    }

    // Gösterimdeki panel sayýsýný döndürür
    public int GetAmountPanelInList()
    {
        return _listInstance.Count;
    }
}
