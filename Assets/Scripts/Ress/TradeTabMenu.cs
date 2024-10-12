using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeTabMenu : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();

    private ShowPanelButton showPanelButton;

    private void OnEnable()
    {
        // UIManager ve ShowPanelButton'ý bir kez alýyoruz
        showPanelButton = UIManager.Instance.GetComponent<ShowPanelButton>();

        // Eðer ShowPanelButton componenti varsa ve buttons dizisinde yeterince buton varsa
        if (showPanelButton != null && buttons.Count >= 3)
        {
            buttons[0].onClick.AddListener(() => OnButtonClicked(PanelID.BuyPanel));
            buttons[1].onClick.AddListener(() => OnButtonClicked(PanelID.SellPanel));
            buttons[2].onClick.AddListener(() => OnButtonClicked(PanelID.SellAllPanel));
        }
    }

    private void OnButtonClicked(PanelID panelID)
    {
        showPanelButton.DoShowPanelWhitId(panelID);
    }
    private void OnDisable()
    {
        foreach (var button in buttons) { 
            button.onClick.RemoveAllListeners();
        }
    }

}
