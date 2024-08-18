using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private GraphicRaycaster raycaster;

    private void Start()
    {
        eventSystem = EventSystem.current;

        // Canvas �zerinde GraphicRaycaster bile�enini al
        raycaster = GetComponent<GraphicRaycaster>();

        // Null kontrol� ekleyin
        if (eventSystem == null)
        {
            Debug.LogError("EventSystem bile�eni bulunamad�!");
        }

        if (raycaster == null)
        {
            Debug.LogError("GraphicRaycaster bile�eni bulunamad�!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector2 touchPosition;

            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }
            else
            {
                touchPosition = Input.mousePosition;
            }

            // Null kontrolleri ekleyin
            if (eventSystem == null || raycaster == null)
            {
                return; // Bile�enlerden biri null ise i�lemi yapmay�n
            }

            PointerEventData pointerEventData = new PointerEventData(eventSystem)
            {
                position = touchPosition
            };

            var results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            // Aktif Canvas'daki panel d���nda bir yere dokunuldu�unu kontrol et
            bool isTouchOnPanel = false;
            foreach (var result in results)
            {
                // Panelin �zerinde bir raycast sonucu olup olmad���n� kontrol et
                if (result.gameObject.GetComponent<RectTransform>() != null)
                {
                    isTouchOnPanel = true;
                    break;
                }
            }

            // E�er dokunulan yer herhangi bir panel de�ilse, aktif paneli kapat
            if (!isTouchOnPanel)
            {
                PanelManager.Instance.HideLastPanel();
            }
        }
    }
}
