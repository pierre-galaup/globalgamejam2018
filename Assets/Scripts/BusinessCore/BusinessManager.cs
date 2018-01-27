using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BusinessCore
{
    public class BusinessManager : MonoBehaviour
    {
        internal int GetId => _lastId++;

        /// <summary>
        /// List of insfrastructures built in the city
        /// </summary>
        private readonly List<IInfratructure> _infrastructuresList = new List<IInfratructure>();

        private CustomersManager _customersManager;
        private static int _lastId = 0;

        [SerializeField]
        public double Money { get; private set; }

        [SerializeField]
        public double MaintenanceCosts { get; private set; }

        public bool CanBuild(IInfratructure infrastructureToBuild)
        {
            var infrastructureCount =
                this._infrastructuresList.Count(e => e.InfrastructureType == infrastructureToBuild.InfrastructureType);
            if (infrastructureCount >= infrastructureToBuild.Limit)
                return false;
            return !(infrastructureToBuild.BuildCost > this.Money);
        }

        public bool Build(IInfratructure infrastructureToBuild)
        {
            if (!this.CanBuild(infrastructureToBuild))
                return false;
            this._infrastructuresList.Add(infrastructureToBuild);
            this.Money -= infrastructureToBuild.BuildCost;
            this.MaintenanceCosts += infrastructureToBuild.MaintenanceCost;
            return true;
        }

        public bool CanUpgradeTechnology(IInfratructure infrastructureToUpgrade)
        {
            
            return false;
        }

        public bool UpgradeTechnology(IInfratructure infrastructureToUpgrade)
        {
            return false;
        }

        public bool CanUpgradeCapacity(IInfratructure infrastructureToUpgrade)
        {
            return false;
        }

        public bool UpgradeCapacity(IInfratructure infrastructureToUpgrade)
        {
            return false;
        }

        private void Awake()
        {
            this._customersManager = new CustomersManager();
            this.Money = 10000;
        }

        private void OnNewMonth()
        {
            this._customersManager.Update();
            this.Money -= this.MaintenanceCosts;
            //if (this.Money < 0)
            //    GameManager.Instance.GameOver();
        }
    }
}