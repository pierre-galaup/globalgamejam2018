using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;
using UnityEngine.UI;
using Town;
using BusinessCore;
using System.ComponentModel;
using Game;

namespace Gui
{
    public class GuiManager : MonoBehaviour
    {
        [Header("Date")]
        [SerializeField]
        private Text textMonth;

        [SerializeField]
        private Text textYear;

        [Header("Infos")]
        [SerializeField]
        private Text _Money;

        [SerializeField]
        private Text _Happy;

        [SerializeField]
        private Text _Gains;

        [SerializeField]
        private Text _MarketShare;
        
        [Header("Abo")]
        [SerializeField]
        private Text textPeople;

        [SerializeField]
        private Text textSubs;

        [Header("Price")]
        [SerializeField]
        private Text _priceInternet;

        [SerializeField]
        private Text _priceMobile;

        [SerializeField]
        private Slider _sliderInternet;

        [SerializeField]
        private Slider _sliderMobile;

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

        [SerializeField]
        private BusinessManager _business;

        
        private void Start()
        {
            _menuInteract.SetActive(false);
            _AboPanel.SetActive(false);
            _MarkPanel.SetActive(false);
            _FinaPanel.SetActive(false);
        }

        private void Awake()
        {
            _business = GameManager.Instance.BusinessManager;
            _Money.text = "Argent = " + _business.Money + "$";
            _MarketShare.text = "MarketShare = 0 %";
            _Gains.text = "Income = 0 $";
            if (_business != null)
                _business.PropertyChanged += UIview;
            TownExpensionManager.OnNewPeople += ViewPeople;
            TimeManager.OnNewMonth += ViewMonth;

        }

        private void UIview(object sender, PropertyChangedEventArgs Event)
        {
            if (Event.PropertyName == "Money")
                _Money.text = "Argent = " + _business.Money + "$";
            if (Event.PropertyName == "MarketShare")
                _MarketShare.text = "MarketShare = " + (int)(_business.MarketShare * 100) + "%";
            if (Event.PropertyName == "Income")
            {
                double value = _business.Income - _business.MaintenanceCosts;
                if (value >= 0)
                    _Gains.text = "Income = " + value + "$";
                else
                    _Gains.text = "Income = " + value + "$";
            }
        }

        public void ChangePanel(GameObject panel)
        {
             _AboPanel.SetActive(false);
            _MarkPanel.SetActive(false);
            _FinaPanel.SetActive(false);

            panel.SetActive(true);
            if (panel == _AboPanel)
            {
                IEnumerable<CustomersManager> Networks = _business.Networks;
                foreach (CustomersManager Network in Networks)
                {
                    if (Network.NetworkType == InfrastructureType.CellularNetwork)
                        _sliderMobile.value = (float)Network.SubscriptionPrice;
                    else
                        _sliderInternet.value = (float)Network.SubscriptionPrice;

                }
            }
        }

        public void OpenMenu()
        {
            _menuInteract.SetActive(true);
            _AboPanel.SetActive(true);
            IEnumerable<CustomersManager> Networks = _business.Networks;
            foreach (CustomersManager Network in Networks)
            {
                if (Network.NetworkType == InfrastructureType.CellularNetwork)
                    _sliderMobile.value = (float)Network.SubscriptionPrice;
                else
                    _sliderInternet.value = (float)Network.SubscriptionPrice;

            }
        }

        public void CloseMenu()
        {
            _menuInteract.SetActive(false);
            _AboPanel.SetActive(false);
            _MarkPanel.SetActive(false);
            _FinaPanel.SetActive(false);
        }

        public void ChangePrice()
        {
            IEnumerable<CustomersManager> Networks = _business.Networks;
            foreach (CustomersManager Network in Networks)
            {
                if (Network.NetworkType == InfrastructureType.CellularNetwork)
                    Network.SubscriptionPrice = (double)_sliderMobile.value;
                else
                    Network.SubscriptionPrice = (double)_sliderInternet.value;

            }
        }

        public void CancelPrice()
        {
            IEnumerable<CustomersManager> Networks = _business.Networks;
            foreach (CustomersManager Network in Networks)
            {
                if (Network.NetworkType == InfrastructureType.CellularNetwork)
                    _sliderMobile.value = (float)Network.SubscriptionPrice;
                else
                    _sliderInternet.value = (float)Network.SubscriptionPrice;

            }
        }

        public void priceUpdateInternet()
        {
            _priceInternet.text = _sliderInternet.value + "$";
        }

        public void priceUpdateMobile()
        {
            _priceMobile.text = _sliderMobile.value + "$";
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
            _business.PropertyChanged -= UIview;
        }


    }
}
