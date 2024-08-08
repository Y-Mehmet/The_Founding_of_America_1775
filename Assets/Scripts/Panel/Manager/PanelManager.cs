using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
  
  public List<PanelInstanceModel> _listInstance = new List<PanelInstanceModel>();

  private ObjectPool _objectPool;
    
    private void Start()
    {
        _objectPool = ObjectPool.Instance;
    }

    public void ShowPanel(string panelID, PanelShowBehavior behavior= PanelShowBehavior.SHOW_PREVISE)
    {
        GameObject instacePanel= _objectPool.GetObjectFromPool(panelID);
        if(instacePanel != null)
        {
            if(behavior == PanelShowBehavior.HIDE_PREVISE && GetAmountPanelInList()>0)
            {
                var lastPanel = GetLastPanel();
                if(lastPanel != null)
                {
                    lastPanel.PanelInstance.SetActive(false);
                }
            }
           
            _listInstance.Add(new PanelInstanceModel
            {
                PanelID = panelID,
                PanelInstance = instacePanel
            }) ;
        }
        else
        {
            Debug.LogWarning($" panel bulunamadý {panelID}");
        }
    }
    public  void HideLastPanel()
    {
        if(AnyPanelIsShowing())
        {
            var lastPanel = GetLastPanel();
            _listInstance.Remove(lastPanel);
            _objectPool.PoolObject(lastPanel.PanelInstance);

            if (GetAmountPanelInList() > 0)
            {
                lastPanel = GetLastPanel();
                if(lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                {
                    lastPanel.PanelInstance.SetActive(true);
                }
            }
        }
      

    }
    public void HideAllPanel()
    {
        if (AnyPanelIsShowing())
        {
            var lastPanel = GetLastPanel();
            _listInstance.Remove(lastPanel);
            _objectPool.PoolObject(lastPanel.PanelInstance);

            if (GetAmountPanelInList() > 0)
            {
                HideAllPanel();
            }
        }
    }

    PanelInstanceModel GetLastPanel()
    {
        if (_listInstance.Count == 0)
        {
            return null;
        }
        return _listInstance[_listInstance.Count - 1];
    }
    public bool AnyPanelIsShowing()
    {
        return GetAmountPanelInList() > 0;
    }
    public int GetAmountPanelInList()
    {
        return _listInstance.Count;
    }
}
