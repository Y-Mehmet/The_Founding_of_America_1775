using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MessageManager : MonoBehaviour
{
  
    public static List<string> messages = new List<string>();
    public static int MaxMessageCount=10;
    public static int unreadMessageCount = 0;
    public static Action<int> OnAddMessage;
}
