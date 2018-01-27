using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BusinessCore
{
    public class CustomersManager
    {
        private double _customerSatisfaction;
        private double _marketShare;

        /// <summary>
        /// Market share owned by the company
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
        /// Recalculate market share value according to variation. Should be called each month in game.
        /// </summary>
        public void Update()
        {
            this.MarketShare *= this.MarketShareVariation;
            this.CustomerSatisfaction *= this.CustomerSatisfactionVariation;
        }
    }
}