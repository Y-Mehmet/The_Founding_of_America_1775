using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ocupulation : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonCliced);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonCliced);
    }
    void OnButtonCliced()
    {
        RegionClickHandler.staticState.OccupyState();

    }
}
