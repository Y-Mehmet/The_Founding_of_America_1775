using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    public Button CleanBtn;
    

    private void OnEnable()
    {

        ShowMessage();
        CleanBtn.onClick.AddListener(OnCleanBtnClicked);
    }
    void ShowMessage()
    {
        int messageCount = MessageManager.messages.Count;
        foreach (Transform child in transform)
        {
            if (child.GetSiblingIndex() < messageCount)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        GetComponent<VerticalLayoutGroup>().enabled = false;
        GetComponent<VerticalLayoutGroup>().enabled = true;
    }
    void OnCleanBtnClicked()
    {
        MessageManager.messages.Clear();
        ShowMessage();
    }
}
