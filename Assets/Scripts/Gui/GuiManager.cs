using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;
using UnityEngine.UI;
using Town;

namespace Gui
{
    public class GuiManager : MonoBehaviour
    {
        [SerializeField]
        private Text textMonth;

        [SerializeField]
        private Text textYear;

        [SerializeField]
        private Text textPeople;

        [Header("GUIinteraction")]
        [SerializeField]
        private GameObject _menuInteract;

        [Header("PanelInteraction")]
        [SerializeField]
        private GameObject _AboPanel;

        [SerializeField]
        private GameObject _MarkPanel;

        [SerializeField]
        private GameObject _FinaPanel;

        private void Start()
        {
            _menuInteract.SetActive(false);
            _AboPanel.SetActive(false);
            _MarkPanel.SetActive(false);
            _FinaPanel.SetActive(false);
        }

        private void Awake()
        {
            TownExpensionManager.OnNewPeople += ViewPeople;
            TimeManager.OnNewMonth += ViewMonth;
        }

        public void ChangePanel(GameObject panel)
        {
            _AboPanel.SetActive(false);
            _MarkPanel.SetActive(false);
            _FinaPanel.SetActive(false);

            panel.SetActive(true);
        }

        public void OpenMenu()
        {
            _menuInteract.SetActive(true);
            _AboPanel.SetActive(true);
        }

        public void CloseMenu()
        {
            _menuInteract.SetActive(false);
            _AboPanel.SetActive(false);
            _MarkPanel.SetActive(false);
            _FinaPanel.SetActive(false);
        }

        private void ViewPeople(int people)
        {
            textPeople.text = "Habitants : " + people;
        }

        private void ViewMonth(TimeManager.GameTime time)
        {
            textMonth.text = "Month : " + time.Months;
            textYear.text = "Year : " + time.Years;
        }

        private void OnDestroy()
        {
            TownExpensionManager.OnNewPeople -= ViewPeople;
            TimeManager.OnNewMonth -= ViewMonth;
        }
        
    
    }
}
