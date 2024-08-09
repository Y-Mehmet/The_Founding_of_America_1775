using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// PanelID enum tan�mlanm�� durumda
public class PanelManager : Singleton<PanelManager>
{
    // Panel �rneklerini saklayan liste
    public List<PanelInstanceModel> _listInstance = new List<PanelInstanceModel>();

    // Nesne havuzu referans�
    private ObjectPool _objectPool;

    private void Start()
    {
        // Nesne havuzu �rne�ini al
        _objectPool = ObjectPool.Instance;
    }

    // Paneli g�sterme fonksiyonu (enum kullan�m� ile g�ncellendi)
    public void ShowPanel(PanelID panelID, PanelShowBehavior behavior = PanelShowBehavior.SHOW_PREVISE)
    {
        // Enum'dan string'e d�n��t�rerek paneli al
        string panelIDString = panelID.ToString();

        // Panel havuzundan nesneyi al
        GameObject instancePanel = _objectPool.GetObjectFromPool(panelIDString);

        // E�er panel nesnesi bulunduysa
        if (instancePanel != null)
        {
            // �nceki paneli gizleme davran��� varsa ve aktif panel varsa
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
            Debug.LogWarning($" panel bulunamad� {panelID}");
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
        else
            Debug.LogWarning("hibir panel a��k de�il");
    }

    // T�m panelleri gizleme fonksiyonu
    public void HideAllPanel()
    {
        while (AnyPanelIsShowing()) // T�m panelleri gizleyene kadar devam et
        {
            var lastPanel = GetLastPanel();
            _listInstance.Remove(lastPanel);
            _objectPool.PoolObject(lastPanel.PanelInstance);
        }
    }

    // Liste i�indeki son paneli d�nd�r�r
    PanelInstanceModel GetLastPanel()
    {
        if (_listInstance.Count == 0)
        {
            return null;
        }
        return _listInstance[_listInstance.Count - 1];
    }

    // G�sterimde herhangi bir panel var m� kontrol eder
    public bool AnyPanelIsShowing()
    {
        return GetAmountPanelInList() > 0;
    }

    // G�sterimdeki panel say�s�n� d�nd�r�r
    public int GetAmountPanelInList()
    {
        return _listInstance.Count;
    }
}
