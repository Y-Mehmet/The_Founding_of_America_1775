using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageCard : MonoBehaviour
{
    public TMP_Text messageText;

    private void OnEnable()
    {
        //Debug.LogError(MessageManager.messages.Count + " " + transform.GetSiblingIndex());
        messageText.text = MessageManager.messages[transform.GetSiblingIndex()];
       
    }
}
