using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral
{
    public class Central : MonoBehaviour, IInfratructure
    {
        private readonly Dictionary<InfrastructureLevelType, IInfrastructureLevel> _currentLevels;
        private readonly BusinessManager _businessManager;

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Limit { get; }
        public Dictionary<InfrastructureLevelType, IEnumerable<IInfrastructureLevel>> Upgrades { get; }
        public InfrastructureType InfrastructureType { get; }

        public IEnumerable<IInfrastructureLevel> CurrentLevels => _currentLevels.Values;

        public double BuildCost
        {
            get { return this.CurrentLevels.Sum(level => level.BuildCost); }
        }

        public double MaintenanceCost
        {
            get { return this.CurrentLevels.Sum(level => level.MaintenanceCost); }
        }

        public Central(BusinessManager businessManager)
        {
            this._businessManager = businessManager;
            this._currentLevels = new Dictionary<InfrastructureLevelType, IInfrastructureLevel>();

            this.Name = "Central";
            this.Id = this._businessManager.GetId;
            this.Description = "Central node for wired internet network";
            this.Limit = 1;
            this.InfrastructureType = InfrastructureType.WiredInternet;
            this.Upgrades = new Dictionary<InfrastructureLevelType, IEnumerable<IInfrastructureLevel>>();
            this.InitializeUpgrades();

            foreach (var infrastructureLevels in this.Upgrades)
            {
                var firstLevel = infrastructureLevels.Value.FirstOrDefault(level => level.Level == 0);
                if (firstLevel == null)
                    continue;
                this._currentLevels[firstLevel.InfrastructureLevelType] = firstLevel;
            }
        }

        public bool CanUpgrade(InfrastructureLevelType typeToUpgrade)
        {
            if (!this.Upgrades.ContainsKey(typeToUpgrade))
                return false;
            var upgradeLevel = this.GetNextUpgrade(typeToUpgrade);
            if (upgradeLevel == null)
                return false;
            var currentTime = _businessManager.GameManager.TimeManager.GetGameTime();
            if (currentTime < upgradeLevel.CreationDate)
                return false;
            return _businessManager.Money - upgradeLevel?.BuildCost < 0;
        }

        public IInfrastructureLevel Upgrade(InfrastructureLevelType typeToUpgrade)
        {
            if (!this.CanUpgrade(typeToUpgrade))
                return null;
            this._currentLevels[typeToUpgrade] = this.GetNextUpgrade(typeToUpgrade);
            return this._currentLevels[typeToUpgrade];
        }

        public IInfrastructureLevel GetNextUpgrade(InfrastructureLevelType upgradeType)
        {
            IInfrastructureLevel upgrade = null;
            if (!this._currentLevels.ContainsKey(upgradeType))
                upgrade = this.Upgrades[upgradeType].FirstOrDefault();
            else
            {
                var currentLevel = this._currentLevels[upgradeType];
                upgrade = this.Upgrades[upgradeType].FirstOrDefault(e => e.Level == currentLevel.Level + 1);
            }
            return upgrade;
        }

        private void InitializeUpgrades()
        {
            var technologies = new List<IInfrastructureLevel>
            {
                new Upgrades.Speed1200Bits(),
                new Upgrades.Speed56K()
            };
            this.Upgrades[InfrastructureLevelType.Technology] = technologies;
        }
    }
}