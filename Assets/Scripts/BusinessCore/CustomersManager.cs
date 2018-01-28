using System.ComponentModel;
using System.Runtime.CompilerServices;
using Game;
using JetBrains.Annotations;
using UnityEngine;

namespace BusinessCore
{
    public class CustomersManager : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField]
        private double _customerSatisfaction = 0.8;

        [SerializeField]
        private double _marketShare = 0.02;

        [SerializeField]
        private InfrastructureType _networkType;

        [SerializeField]
        private double _subscriptionPrice = 50;

        [SerializeField]
        private double _marketShareVariation = 1;

        [SerializeField]
        private double _customerSatisfactionVariation = 1;

        private int _subscribersNumber;

        /// <summary>
        /// Market share owned by the company. 0 -> 1 range
        /// </summary>
        public double MarketShare
        {
            get { return _marketShare; }
            private set
            {
                if (value < 0.001) // Minimal value for market share. Below this value, the formula will be broken
                    value = 0.001;
                if (value > 1)
                    value = 1;
                if (value.Equals(_marketShare)) return;
                _marketShare = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Number of subscribers
        /// </summary>
        public int SubscribersNumber
        {
            get { return _subscribersNumber; }
            private set
            {
                if (value == _subscribersNumber) return;
                _subscribersNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Customers satisfaction
        /// </summary>
        public double CustomerSatisfaction
        {
            get { return _customerSatisfaction; }
            private set
            {
                // Minimal value for customer satisfaction. Below this value, the formula will be broken
                if (value < 0.001)
                    value = 0.001;
                if (value > 1)
                    value = 1;
                if (value.Equals(_customerSatisfaction)) return;
                _customerSatisfaction = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Variation of the market share. 1 = no variation. 2 = 100% variation.
        /// </summary>
        public double MarketShareVariation
        {
            get { return _marketShareVariation; }
            set
            {
                if (value < 0.001)
                    value = 0.001;
                else if (value > 2)
                    value = 2;
                if (value.Equals(_marketShareVariation)) return;
                _marketShareVariation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Customers satisfaction variation. 1 = no variation. 2 = 100% variation.
        /// </summary>
        public double CustomerSatisfactionVariation
        {
            get { return _customerSatisfactionVariation; }
            set
            {
                if (value < 0.001)
                    value = 0.001;
                else if (value > 2)
                    value = 2;
                if (value.Equals(_customerSatisfactionVariation)) return;
                _customerSatisfactionVariation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Define the price of the subscription
        /// </summary>
        public double SubscriptionPrice
        {
            get { return _subscriptionPrice; }
            set
            {
                if (value.Equals(_subscriptionPrice)) return;
                _subscriptionPrice = value;
                OnPropertyChanged();
            }
        }

        public double MonthlyIncome { get; private set; }

        public InfrastructureType NetworkType
        {
            get { return _networkType; }
            set { _networkType = value; }
        }

        /// <summary>
        /// Recalculate market share value according to variation. Should be called each month in game.
        /// </summary>
        public void OnNewMonth()
        {
            this.CustomerSatisfactionVariationUpdate();
            this.CustomerSatisfaction *= this.CustomerSatisfactionVariation;
            this.MarketShareVariationUpdate();
            this.MarketShare *= this.MarketShareVariation;
            this.SubscribersNumber = (int)(GameManager.Instance.TownExpensionManager.GetPeoplesNumber() * this.MarketShare);
            this.MonthlyIncome = this.SubscribersNumber * this.SubscriptionPrice;
        }

        private void CustomerSatisfactionVariationUpdate()
        {
            var currentTime = GameManager.Instance.TimeManager.GetGameTime();
            if (currentTime.Years < 1985)
            {
                if (this.SubscriptionPrice < 30)
                    this.CustomerSatisfactionVariation *= 1.25;
                else if (this.SubscriptionPrice < 60)
                    this.CustomerSatisfactionVariation *= 1.05;
                else if (this.SubscriptionPrice < 120)
                    this.CustomerSatisfactionVariation *= 0.95;
                else
                    this.CustomerSatisfactionVariation *= 0.9;
            }
            else if (currentTime.Years < 1990)
            {
                if (this.SubscriptionPrice < 30)
                    this.CustomerSatisfactionVariation *= 1.1;
                else if (this.SubscriptionPrice < 60)
                    this.CustomerSatisfactionVariation *= 1.03;
                else if (this.SubscriptionPrice < 120)
                    this.CustomerSatisfactionVariation *= 0.9;
                else
                    this.CustomerSatisfactionVariation *= 0.8;
            }
            else
            {
                if (this.SubscriptionPrice < 5)
                    this.CustomerSatisfactionVariation *= 1.05;
                else if (this.SubscriptionPrice < 30)
                    this.CustomerSatisfactionVariation *= 1.01;
                else if (this.SubscriptionPrice < 40)
                    this.CustomerSatisfactionVariation *= 0.98;
                else if (this.SubscriptionPrice < 60)
                    this.CustomerSatisfactionVariation *= 0.93;
                else if (this.SubscriptionPrice < 120)
                    this.CustomerSatisfactionVariation *= 0.9;
                else
                    this.CustomerSatisfactionVariation *= 0.85;
            }
        }

        private void MarketShareVariationUpdate()
        {
            if (this.CustomerSatisfaction < 0.15)
                this.MarketShareVariation = 0.80;
            if (this.CustomerSatisfaction < 0.3)
                this.MarketShareVariation = 0.90;
            else if (this.CustomerSatisfaction < 0.40)
                this.MarketShareVariation = 0.92;
            else if (this.CustomerSatisfaction < 0.50)
                this.MarketShareVariation = 0.96;
            else if (this.CustomerSatisfaction < 0.60)
                this.MarketShareVariation = 1;
            else if (this.CustomerSatisfaction < 0.70)
                this.MarketShareVariation = 1.01;
            else if (this.CustomerSatisfaction < 0.80)
                this.MarketShareVariation = 1.03;
            else if (this.CustomerSatisfaction <= 1)
                this.MarketShareVariation = 1.05;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}