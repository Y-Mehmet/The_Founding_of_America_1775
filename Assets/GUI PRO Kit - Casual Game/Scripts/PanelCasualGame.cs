using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LayerLab.CasualGame
{
    public class PanelCasualGame : MonoBehaviour
    {
        [SerializeField] private GameObject[] otherPanels;
        public Button prewive, postwive;
       public  int index = 0;
        public void OnEnable()
        {
            prewive.onClick.AddListener(Prewiev);
            postwive.onClick.AddListener (Postwiev);
           
        }

        public void OnDisable()
        {
            for (int i = 0; i < otherPanels.Length; i++) otherPanels[i].SetActive(false);
            prewive.onClick.RemoveListener(Prewiev);
            postwive.onClick.RemoveListener(Postwiev);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                Postwiev();
            if(Input.GetKeyDown(KeyCode.D))
                Prewiev();
        }
        private void Prewiev()
        {
            index++;
            if(index >= otherPanels.Length-1) { index = otherPanels.Length-1; }
            otherPanels[index].SetActive(true);
            
        }
        private  void Postwiev()
        {
            index--;
            if(index <= 0) { index = 0; }
            otherPanels[index+1].SetActive(false);
            
        }
    }
}
