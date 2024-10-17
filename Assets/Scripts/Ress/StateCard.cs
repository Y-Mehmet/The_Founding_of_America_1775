using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static TradeManager;
using static UnityEditor.Experimental.GraphView.GraphView;
public class StateCard : MonoBehaviour
{
    public TextMeshProUGUI StateNameText, CoinValueText, CurentLimitValueText;// tradelimit olmal�
    public Image stateFalgImage, resIconImage;
    public bool isExportCard = true;
    int thousand = 1000;
    
    private void OnEnable()
    {
       
       
        {
            if (transform.parent.GetComponent<StateProductPanelScript>().tradeType == TradeType.Import)
            {
                ShowImportPanelInfo();
            }
            else if (transform.parent.GetComponent<StateProductPanelScript>().tradeType == TradeType.Export)
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
        var exportTradeList =stateTransformAndTradeList;
        int gameObjectindex = gameObject.transform.GetSiblingIndex()-1;
       
        if (gameObjectindex< exportTradeList.Count)
        {
           
            ResourceType curretResType = ResourceManager.curentResource;

           
                Trade exportTrade = exportTradeList.ElementAt(gameObjectindex).Value;
                Transform tradeState = exportTradeList.ElementAt(gameObjectindex).Key;

                StateNameText.text = tradeState.name;
                int stateIndex = tradeState.GetSiblingIndex();
                stateFalgImage.sprite = StateFlagSpritesSO.Instance.flagSpriteLists[stateIndex];

                int ResIconIndex = (int)(curretResType);
                resIconImage.sprite = ResSpriteSO.Instance.resIcon[ResIconIndex];
            int tradeResIndex= (int) (curretResType)-1;
            float limit= exportTrade.limit[tradeResIndex];
            CurentLimitValueText.text = ((int)limit).ToString();
            CoinValueText.text = (exportTrade.contractPrices[(int)curretResType - 1] * thousand).ToString();







        }
        else
        { gameObject.SetActive(false); }
       
    }
    void ShowImportPanelInfo()
    {
        var importTradeList = stateTransformAndTradeList;
        int gameObjectindex = gameObject.transform.GetSiblingIndex() - 1;
       
        if (gameObjectindex < importTradeList.Count)
        {
           
            ResourceType curretResType = ResourceManager.curentResource;


            Trade importTrade = importTradeList.ElementAt(gameObjectindex).Value;
            State tradeState = importTradeList.ElementAt(gameObjectindex).Key.GetComponent<State>();

            StateNameText.text = tradeState.name;
            int stateIndex = tradeState.transform.GetSiblingIndex();
            stateFalgImage.sprite = StateFlagSpritesSO.Instance.flagSpriteLists[stateIndex];

            int ResIconIndex = (int)(curretResType);
            resIconImage.sprite = ResSpriteSO.Instance.resIcon[ResIconIndex];
            //int index = 0;
            foreach (var resType in importTrade.resourceTypes)
            {

                if (resType == curretResType)
                {
                    Debug.LogWarning($" curent res index {((int)resType - 1)}  ");
                    CoinValueText.text = (importTrade.contractPrices[(int)resType-1] * thousand).ToString();
                    float limit = importTrade.limit[(int)resType - 1];
                  

                  
                    if (limit > 0)
                    {
                        CurentLimitValueText.text = ((int)limit).ToString();
                    }
                    else
                    {
                        CurentLimitValueText.text = "0";
                        Debug.LogWarning(" curent statetenin current amaountu null");
                    }

                }
               // else
               // index++;
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

                var exportTradeList = stateTransformAndTradeList;
                Transform state = exportTradeList.ElementAt(gameObjectindex).Key;
                ResourceManager.Instance.SetCurrentTradeState(state.name);
                Debug.LogWarning(" yeni sate se�ildi  state card" + state.name);

            }
            else
            {
                var importTradeList = gameObject.transform.parent.GetComponent<StateProductPanelScript>().GetImportTradeList();
                Transform state = importTradeList.ElementAt(gameObjectindex).Key;
                ResourceManager.Instance.SetCurrentTradeState(state.name);
                Debug.LogWarning(" yeni sate se�ildi  state card" + state.name);
            }
            
        }
        UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
         
            
      

    }

}
