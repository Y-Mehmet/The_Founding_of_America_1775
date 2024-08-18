using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateCard : MonoBehaviour
{
    public TextMeshProUGUI StateNameText, CoinValueText, CurentLimitValueText;
    public Image stateFalgImage, resIconImage;
    public bool isExportCard = true;
    private void Start()
    {
      
    }
    private void OnEnable()
    {
        gameObject.SetActive(true);
        if(gameObject.transform.parent.TryGetComponent<StateProductPanelScript>(out StateProductPanelScript parent) )
        {
            if(parent.tradeType== TradeType.Import)
            {
                ShowImportPanelInfo();
            }
            else
            {
                ShowExportPanelInfo();
            }
        }

       

    }
    void ShowExportPanelInfo()
    {
        var exportTradeList = gameObject.transform.parent.GetComponent<StateProductPanelScript>().GetExportTradeList();
        int gameObjectindex = gameObject.transform.GetSiblingIndex()-1;
        if (gameObjectindex< exportTradeList.Count)
        {
            ResourceType curretResType = ResourceManager.Instance.curentResource;

           
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
                        CoinValueText.text = exportTrade.contractPrices[index].ToString();
                        CurentLimitValueText.text = exportTrade.limits[index].ToString();
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
            ResourceType curretResType = ResourceManager.Instance.curentResource;


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
                    CoinValueText.text = exportTrade.contractPrices[index].ToString();
                    CurentLimitValueText.text = exportTrade.limits[index].ToString();
                    break;
                }
                else
                    index++;
            }




        }
        else
        { gameObject.SetActive(false); }

    }

}
