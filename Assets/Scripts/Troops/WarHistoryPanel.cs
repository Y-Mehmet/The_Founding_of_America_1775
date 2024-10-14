using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
public class WarHistoryPanel : MonoBehaviour
{
    private void OnEnable()
    {
        ChilGameObjcetActiveted();



    }
    private void OnDisable()
    {

    }
    private void ChilGameObjcetActiveted()
    {

        for (int index = 0; index < transform.childCount; index++)
        {

            if (index < WarHistory.maxWarHistoryCount && index < WarHistory.generalIndexAndWarList.Count)
            {
                GameObject childGameObject = transform.GetChild(index).gameObject;
                childGameObject.SetActive(true);


                childGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = WarHistory.generalIndexAndWarList.Skip(index).FirstOrDefault().warDate;
                int allyStateIndex = WarHistory.generalIndexAndWarList.Skip(index).FirstOrDefault().allyState.HierarchicalIndex;
                int enemyStateIndx = WarHistory.generalIndexAndWarList.Skip(index).FirstOrDefault().enemyState.HierarchicalIndex;
                childGameObject.transform.GetChild(1).GetComponent<Image>().sprite = StateFlagSpritesSO.Instance.flagSpriteLists[allyStateIndex];
                childGameObject.transform.GetChild(2).GetComponent<Image>().sprite = StateFlagSpritesSO.Instance.flagSpriteLists[enemyStateIndx];
                string generalName = WarHistory.generalIndexAndWarList.Skip(index).FirstOrDefault().generalIndex;
                childGameObject.transform.GetChild(3).GetComponent<TMP_Text>().text = generalName;
                childGameObject.transform.GetChild(4).GetComponent<TMP_Text>().text = WarHistory.generalIndexAndWarList.Skip(index).FirstOrDefault().warReslut.ToString();

            }
            else
                break;

        }
    }
   
}
