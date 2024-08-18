using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResCard : MonoBehaviour
{
    private void Start()
    {
       
    }
    private void OnEnable()
    {

        ShowPanelInfo();
    }
    void ShowPanelInfo()
    {
      
        State curentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if (curentState != null)
        {
            int counter = -1;
            foreach (Transform chid in transform)
            {

                
                   
                    if (counter == -1)
                    {
                        counter = 0;
                        continue;
                    }
                    foreach (Transform item in chid)
                    {
                      
                        int index = item.GetSiblingIndex();
                        switch (index)
                        {
                            case 0:
                                item.gameObject.GetComponent<Image>().sprite = ResSpriteSO.Instance.resIcon[counter];
                              

                                break;
                            case 1:
                                item.gameObject.GetComponent<TextMeshProUGUI>().text = curentState.resourceData.Keys.ElementAt(counter).ToString();
                                break;
                            case 3:
                                item.gameObject.GetComponent<TextMeshProUGUI>().text = curentState.resourceData.ElementAt(counter).Value.currentAmount.ToString();
                                break;
                            default:
                                break;

                        }
                       



                    }
                   
                if (counter < ResSpriteSO.Instance.resIcon.Count)
                {
                    counter++;
                }
                else
                    Debug.LogWarning("eriþilemez index hatasý");

            }


        }
        else
            Debug.Log("current state is null");
    }
}
