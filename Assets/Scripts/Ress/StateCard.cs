using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StateCard : MonoBehaviour
{
    public TextMeshProUGUI StateNameText, CoinValueText, CurentLimitValueText;
    public Image stateFalgImage, resIconImage;
    public bool isExportCard = true;
    int thousand = 1000;
    
    private void OnEnable()
    {
        
        if(gameObject.transform.parent.TryGetComponent<StateProductPanelScript>(out StateProductPanelScript parent) )
        {
            if(parent.tradeType== TradeType.Import)
            {
                ShowImportPanelInfo();
            }
            else if(parent.tradeType == TradeType.Export)
            {
                ShowExportPanelInfo();
            }
        }
        gameObject.GetComponent<Button>().onClick.AddListener(SetCurrentTradeState);

    }
    private void OnDisable()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(SetCurrentTradeState);
    }
    void ShowExportPanelInfo()
    {
        var exportTradeList = gameObject.transform.parent.GetComponent<StateProductPanelScript>().GetExportTradeList();
        int gameObjectindex = gameObject.transform.GetSiblingIndex()-1;
       
        if (gameObjectindex< exportTradeList.Count)
        {
           
            ResourceType curretResType = ResourceManager.curentResource;

           
                Trade exportTrade = exportTradeList.ElementAt(gameObjectindex).Value;
                Transform state = exportTradeList.ElementAt(gameObjectindex).Key;

                StateNameText.text = state.name;
                int stateIndex = state.GetSiblingIndex();
                stateFalgImage.sprite = StateFlagSpritesSO.Instance.flagSpriteLists[stateIndex];

                int ResIconIndex = (int)(curretResType);
                resIconImage.sprite = ResSpriteSO.Instance.resIcon[ResIconIndex];
                int index = 0;
                foreach (var resType in exportTrade.resourceTypes)
                {

                    if (resType == curretResType)
                    {
                        CoinValueText.text = (exportTrade.contractPrices[index]*thousand).ToString();
                    if((state.gameObject.GetComponent<State>().resourceData[ResourceType.Gold].currentAmount)>0 )
                    {
                        float gold = state.gameObject.GetComponent<State>().resourceData[ResourceType.Gold].currentAmount;
                        float contPrice =  exportTrade.contractPrices[index];
                        int limit = (int) (gold / contPrice);
                        State tradeState = state.GetComponent<State>();
                        if ( tradeState!=null)
                        {
                            for(int i = 0;i< tradeState.importTrade.resourceTypes.Count;i++)
                            {
                                if (tradeState.importTrade.resourceTypes[i] == curretResType)
                                {
                                    if (limit> tradeState.importTrade.limit[i])
                                    {
                                        limit = (int)tradeState.importTrade.limit[i];
                                    }
                                }
                            }
                            
                        }
                            
                            CurentLimitValueText.text = limit.ToString() ;
                    }
                    else
                    {
                        Debug.LogWarning(" curent statetenin current amaountu null");
                    }
                       
                        break;
                    }
                    else
                        index++;
                }


            

        }
        else
        { gameObject.SetActive(false); }
       
    }
    void ShowImportPanelInfo()
    {
        var exportTradeList = gameObject.transform.parent.GetComponent<StateProductPanelScript>().GetImportTradeList();
        int gameObjectindex = gameObject.transform.GetSiblingIndex() - 1;
       
        if (gameObjectindex < exportTradeList.Count)
        {
           
            ResourceType curretResType = ResourceManager.curentResource;


            Trade exportTrade = exportTradeList.ElementAt(gameObjectindex).Value;
            Transform state = exportTradeList.ElementAt(gameObjectindex).Key;

            StateNameText.text = state.name;
            int stateIndex = state.GetSiblingIndex();
            stateFalgImage.sprite = StateFlagSpritesSO.Instance.flagSpriteLists[stateIndex];

            int ResIconIndex = (int)(curretResType);
            resIconImage.sprite = ResSpriteSO.Instance.resIcon[ResIconIndex];
            int index = 0;
            foreach (var resType in exportTrade.resourceTypes)
            {

                if (resType == curretResType)
                {
                    CoinValueText.text = (exportTrade.contractPrices[index] * thousand).ToString();
                    float limit = state.gameObject.GetComponent<State>().resourceData[resType].currentAmount;
                    
                    State tradeState = state.GetComponent<State>();
                    if (tradeState != null)
                    {
                        for (int i = 0; i < tradeState.exportTrade.resourceTypes.Count; i++)
                        {
                            if (tradeState.exportTrade.resourceTypes[i] == curretResType)
                            {
                                if (limit > tradeState.exportTrade.limit[i])
                                {
                                    limit = (int)tradeState.exportTrade.limit[i];
                                }
                            }
                        }

                    }
                    if (limit > 0)
                    {
                        CurentLimitValueText.text = limit.ToString();
                    }
                    else
                    {
                        CurentLimitValueText.text = "0";
                        Debug.LogWarning(" curent statetenin current amaountu null");
                    }

                }
                else
                    index++;
            }




        }
        else
        { gameObject.SetActive(false); }

    }
    void SetCurrentTradeState()
    {
        if (gameObject.transform.parent.TryGetComponent<StateProductPanelScript>(out StateProductPanelScript parent))
        {
            int gameObjectindex = gameObject.transform.GetSiblingIndex() - 1;
            if (parent.tradeType == TradeType.Export)
            {

                var exportTradeList = gameObject.transform.parent.GetComponent<StateProductPanelScript>().GetExportTradeList();
                Transform state = exportTradeList.ElementAt(gameObjectindex).Key;
                ResourceManager.Instance.SetCurrentTradeState(state.name);
                Debug.LogWarning(" yeni sate seçildi  state card" + state.name);

            }
            else
            {
                var importTradeList = gameObject.transform.parent.GetComponent<StateProductPanelScript>().GetImportTradeList();
                Transform state = importTradeList.ElementAt(gameObjectindex).Key;
                ResourceManager.Instance.SetCurrentTradeState(state.name);
                Debug.LogWarning(" yeni sate seçildi  state card" + state.name);
            }
            
        }
         
            
      

    }

}
