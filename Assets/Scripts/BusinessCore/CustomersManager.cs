using Game;
using UnityEngine;

namespace BusinessCore
{
    public class CustomersManager : MonoBehaviour
    {
        [SerializeField]
        private double _customerSatisfaction = 0;

        [SerializeField]
        private double _marketShare = 0;

        [SerializeField]
        private InfrastructureType _networkType;

        [SerializeField]
        private double _subscriptionPrice = 50;

        /// <summary>
        /// Market share owned by the company. 0 -> 1 range
        /// </summary>
        public double MarketShare
        {
            get { return _marketShare; }
            private set
            {
                if (value < 0.01) // Minimal value for market share. Below this value, the formula will be broken
                    value = 0.01;
                _marketShare = value;
            }
        }

        /// <summary>
        /// Number of subscribers
        /// </summary>
        public int SubscribersNumber { get; private set; }

        /// <summary>
        /// Customers satisfaction
        /// </summary>
        public double CustomerSatisfaction
        {
            get { return _customerSatisfaction; }
            private set
            {
                // Minimal value for customer satisfaction. Below this value, the formula will be broken
                if (value <= 0.01)
                    value = 0.01;
                _customerSatisfaction = value;
            }
        }

        /// <summary>
        /// Variation of the market share. 1 = no variation. 2 = 100% variation.
        /// </summary>
        public double MarketShareVariation { get; set; }

        /// <summary>
        /// Customers satisfaction variation. 1 = no variation. 2 = 100% variation.
        /// </summary>
        public double CustomerSatisfactionVariation { get; set; }

        /// <summary>
        /// Define the price of the subscription
        /// </summary>
        public double SubscriptionPrice
        {
            get { return _subscriptionPrice; }
            set { _subscriptionPrice = value; }
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
            this.CustomerSatisfaction *= this.CustomerSatisfactionVariation;
            this.MarketShare *= this.MarketShareVariation;
            this.SubscribersNumber = (int)(GameManager.Instance.TownExpensionManager.GetPeoplesNumber() * this.MarketShare);
            this.MonthlyIncome = this.SubscribersNumber * this.SubscriptionPrice;
        }
    }
}