using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Game;
using GameTime;
using JetBrains.Annotations;
using UnityEngine;

namespace BusinessCore
{
    public class BusinessManager : MonoBehaviour, INotifyPropertyChanged
    {
        /// <summary>
        /// Get an unique id
        /// </summary>
        internal int GetId => _lastId++;

        /// <summary>
        /// List of insfrastructures built in the city
        /// </summary>
        private readonly List<IInfrastructure> _infrastructuresList = new List<IInfrastructure>();

        [SerializeField]
        private List<CustomersManager> _networks = new List<CustomersManager>();

        private static int _lastId = 0;

        [SerializeField] private GameManager _gameManager;

        [SerializeField] private double _money = 20000;
        private double _income;
        private double _marketShare;
        private double _maintenanceCosts;

        public double Money
        {
            get { return _money; }
            private set
            {
                if (value.Equals(_money)) return;
                _money = value;
                OnPropertyChanged();
            }
        }

        public double Income
        {
            get { return _income; }
            private set
            {
                if (value.Equals(_income)) return;
                _income = value;
                OnPropertyChanged();
            }
        }

        public double MarketShare
        {
            get { return _marketShare; }
            private set
            {
                if (value.Equals(_marketShare)) return;
                _marketShare = value;
                OnPropertyChanged();
            }
        }

        public double MaintenanceCosts
        {
            get { return _maintenanceCosts; }
            private set
            {
                if (value.Equals(_maintenanceCosts)) return;
                _maintenanceCosts = value;
                OnPropertyChanged();
            }
        }

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

            if (this._networks.All(e => e.NetworkType != infrastructureToBuild.InfrastructureType))
            {
                this._networks.Add(customersManager); // We have a new network here
            }
        

            var network = this._networks.FirstOrDefault(e => e.NetworkType == infrastructureToBuild.InfrastructureType);
            if (!network)
                return false;
            var levels = infrastructureToBuild.CurrentLevels;
            foreach (var level in levels)
            {
                network.CustomerSatisfactionVariation *= level.SatisfactionProvided;
            }

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

        public IEnumerable<CustomersManager> Networks => this._networks;

        private void OnNewMonth(TimeManager.GameTime time)
        {
            var income = 0.0;
            var marketShares = new List<double>();
            var totalSubscribers = 0;
            foreach (var customersManager in this._networks)
            {
                customersManager.OnNewMonth();
                income += customersManager.MonthlyIncome;
                marketShares.Add(customersManager.MarketShare);
                totalSubscribers += customersManager.SubscribersNumber;
                Debug.Log($"Network: {customersManager.NetworkType} - Customer Satisfaction Variation {customersManager.CustomerSatisfactionVariation} - Market variation: {customersManager.MarketShareVariation} - Satisfaction: {customersManager.CustomerSatisfaction} (+/- {customersManager.CustomerSatisfactionVariation})");
            }
            if (marketShares.Any())
                this.MarketShare = marketShares.Average();
            this.Income = income;
            this.Money += this.Income;
            this.Money =  this.Money - (this.MaintenanceCosts + this.MaintenanceCosts * 0.07 * totalSubscribers);
            Debug.Log($"Account: {this.Money}€ - Maintenance costs: {(this.MaintenanceCosts + this.MaintenanceCosts * 0.07 * totalSubscribers)}€ - Income: {this.Income}€ - Market Share: {this.MarketShare * 100}% of {GameManager.Instance.TownExpensionManager.GetPeoplesNumber()} habs - Subscribers: {totalSubscribers}");
            if (this.Money < 0)
                this._gameManager.GameOver();
        }

        private void OnDestroy()
        {
            TimeManager.OnNewMonth -= this.OnNewMonth;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}