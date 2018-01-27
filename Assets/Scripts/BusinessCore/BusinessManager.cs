using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BusinessCore
{
    public class BusinessManager : MonoBehaviour
    {
        /// <summary>
        /// List of insfrastructures built in the city
        /// </summary>
        private List<IInfratructure> infrastructuresList = new List<IInfratructure>();

        private CustomersManager customersManager;

        [SerializeField]
        public double Money { get; private set; }

        [SerializeField]
        public double MaintenanceCosts { get; private set; }

        private void Awake()
        {
            this.customersManager = new CustomersManager();
            this.Money = 10000;
        }

        private void OnNewMonth()
        {
            this.customersManager.Update();
            this.Money -= this.MaintenanceCosts;
            //if (this.Money < 0)
            //    GameManager.Instance.GameOver();
        }

        /// <summary>
        /// Called on infrastructure construction or upgrade
        /// </summary>
        private void OnInfrastructureBuilt(IInfratructure previousLevel, IInfratructure newLevel)
        {
            this.customersManager.CustomerSatisfactionVariation += newLevel.InfrastructureLevel.SatisfactionProvided;
            this.Money -= newLevel.InfrastructureLevel.BuildCost;
            if (previousLevel != null)
                this.MaintenanceCosts -= previousLevel.InfrastructureLevel.MaintenanceCost;
            this.MaintenanceCosts += newLevel.InfrastructureLevel.MaintenanceCost;

        }
    }
}