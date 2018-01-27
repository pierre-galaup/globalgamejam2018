using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace BusinessCore
{
    public class BusinessManager : MonoBehaviour
    {
        /// <summary>
        /// Get an unique id
        /// </summary>
        internal int GetId => _lastId++;

        /// <summary>
        /// List of insfrastructures built in the city
        /// </summary>
        private readonly List<IInfratructure> _infrastructuresList = new List<IInfratructure>();

        /// <summary>
        /// 
        /// </summary>
        private CustomersManager _customersManager;

        private static int _lastId = 0;

        public GameManager GameManager = GameManager.Instance;

        public double Money { get; private set; }

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
            return infrastructureToUpgrade != null && infrastructureToUpgrade.CanUpgrade(InfrastructureLevelType.Technology);
        }

        public bool UpgradeTechnology(IInfratructure infrastructureToUpgrade)
        {
            var upgrade = infrastructureToUpgrade?.Upgrade(InfrastructureLevelType.Technology);
            if (upgrade == null)
                return false;
            this.Money -= upgrade.BuildCost;
            this.MaintenanceCosts = this.MaintenanceCosts - infrastructureToUpgrade.MaintenanceCost +
                                    upgrade.MaintenanceCost;
            return true;
        }

        public bool CanUpgradeCapacity(IInfratructure infrastructureToUpgrade)
        {
            return infrastructureToUpgrade != null && infrastructureToUpgrade.CanUpgrade(InfrastructureLevelType.Capacity);
        }

        public bool UpgradeCapacity(IInfratructure infrastructureToUpgrade)
        {
            var upgrade = infrastructureToUpgrade?.Upgrade(InfrastructureLevelType.Capacity);
            if (upgrade == null)
                return false;
            this.Money -= upgrade.BuildCost;
            this.MaintenanceCosts = this.MaintenanceCosts - infrastructureToUpgrade.MaintenanceCost +
                                    upgrade.MaintenanceCost;
            return true;
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
            if (this.Money < 0)
                this.GameManager.GameOver();
        }
    }
}