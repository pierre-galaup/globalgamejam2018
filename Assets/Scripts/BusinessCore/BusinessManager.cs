using System.Collections.Generic;
using System.Linq;
using Game;
using GameTime;
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
        private readonly List<IInfrastructure> _infrastructuresList = new List<IInfrastructure>();

        private List<CustomersManager> _networks = new List<CustomersManager>();

        private static int _lastId = 0;

        [SerializeField] private GameManager _gameManager;

        [SerializeField] private double _money = 20000;

        public double Money
        {
            get { return _money; }
            private set { _money = value; }
        }

        public double Income { get; private set; }

        public double MarketShare { get; private set; }

        public double MaintenanceCosts { get; private set; }

        private void Awake()
        {
            TimeManager.OnNewMonth += this.OnNewMonth;
        }

        private void Start()
        {
            this._gameManager = GameManager.Instance;
        }

        public bool CanBuild(IInfrastructure infrastructureToBuild)
        {
            if (infrastructureToBuild == null)
                return false;
            var infrastructureCount =
                this._infrastructuresList.Count(e => e.Name == infrastructureToBuild.Name &&
                                                     e.InfrastructureType == infrastructureToBuild.InfrastructureType);
            if (infrastructureCount >= infrastructureToBuild.Limit)
                return false;
            return !(infrastructureToBuild.BuildCost > this.Money);
        }

        public bool Build(IInfrastructure infrastructureToBuild)
        {
            if (!this.CanBuild(infrastructureToBuild))
                return false;
            this._infrastructuresList.Add(infrastructureToBuild);
            this.Money -= infrastructureToBuild.BuildCost;
            this.MaintenanceCosts += infrastructureToBuild.MaintenanceCost;
            var customersManager = this.gameObject.AddComponent<CustomersManager>();
            customersManager.NetworkType = infrastructureToBuild.InfrastructureType;
            this._networks.Add(customersManager);
            return true;
        }

        public bool CanUpgradeTechnology(IInfrastructure infrastructureToUpgrade)
        {
            return infrastructureToUpgrade != null &&
                   infrastructureToUpgrade.CanUpgrade(InfrastructureLevelType.Technology);
        }

        public bool UpgradeTechnology(IInfrastructure infrastructureToUpgrade)
        {
            var previousTech = infrastructureToUpgrade.GetCurrentLevel(InfrastructureLevelType.Technology);
            if (previousTech == null)
                return false;
            var upgrade = infrastructureToUpgrade?.Upgrade(InfrastructureLevelType.Technology);
            if (upgrade == null)
                return false;
            this.Money -= upgrade.BuildCost;
            this.MaintenanceCosts = this.MaintenanceCosts - previousTech.MaintenanceCost + upgrade.MaintenanceCost;
            return true;
        }

        public bool CanUpgradeCapacity(IInfrastructure infrastructureToUpgrade)
        {
            return infrastructureToUpgrade != null &&
                   infrastructureToUpgrade.CanUpgrade(InfrastructureLevelType.Capacity);
        }

        public bool UpgradeCapacity(IInfrastructure infrastructureToUpgrade)
        {
            var previousTech = infrastructureToUpgrade.GetCurrentLevel(InfrastructureLevelType.Capacity);
            var upgrade = infrastructureToUpgrade?.Upgrade(InfrastructureLevelType.Capacity);
            if (upgrade == null)
                return false;
            this.Money -= upgrade.BuildCost;
            this.MaintenanceCosts = this.MaintenanceCosts - previousTech.MaintenanceCost + upgrade.MaintenanceCost;
            return true;
        }

        private void OnNewMonth(TimeManager.GameTime time)
        {
            this.Income = 0;
            this.MarketShare = 0;
            var marketShares = new List<double>();
            var totalSubscribers = 0;
            foreach (var customersManager in this._networks)
            {
                customersManager.OnNewMonth();
                this.Income += customersManager.MonthlyIncome;
                marketShares.Add(customersManager.MarketShare);
                totalSubscribers += customersManager.SubscribersNumber;
            }
            if (marketShares.Any())
                this.MarketShare = marketShares.Average();
            this.Money += this.Income;
            this.Money -= this.MaintenanceCosts;
            Debug.Log($"Account: ${this.Money} - Maintenance costs: ${this.MaintenanceCosts} - Income: ${this.Income} - Market Share : {this.MarketShare * 100}% of {GameManager.Instance.TownExpensionManager.GetPeoplesNumber()} habs - Subscribers: {totalSubscribers}");
            if (this.Money < 0)
                this._gameManager.GameOver();
        }

        private void OnDestroy()
        {
            TimeManager.OnNewMonth -= this.OnNewMonth;
        }
    }
}