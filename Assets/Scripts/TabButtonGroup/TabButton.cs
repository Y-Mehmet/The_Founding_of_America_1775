using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MY
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerClickHandler
    {
        public TabGroup tabGroup;
        public Image background;
        public UnityEvent onTabSelected;
        public UnityEvent onTabDeselected;

        private void Start()
        {
            background = GetComponent<Image>();
            tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Bu kod parçasý, hem masaüstü hem de mobil cihazlarda çalýþýr.

            tabGroup.OnTabSelected(this);
        }


        public void Select()
        {
            if (onTabSelected != null)
            {
                Debug.LogWarning($" {name} selected");
                onTabSelected.Invoke();
            }

        }
        public void Deselect()
        {
            if (onTabDeselected != null)
            {
                Debug.LogWarning($" {name} deselected");
                onTabDeselected.Invoke();
            }
        }
    }





}

