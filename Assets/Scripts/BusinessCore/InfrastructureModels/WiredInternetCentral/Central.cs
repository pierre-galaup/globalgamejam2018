using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCore.InfrastructureLevelTypes;
using BusinessCore.InfrastructureTypes;
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

        public double BuildCost
        {
            get { return this.CurrentLevels.Sum(level => level.BuildCost); }
        }

        public double MaintenanceCost
        {
            get { return this.CurrentLevels.Sum(level => level.MaintenanceCost); }
        }

        public InfrastructureType InfrastructureType { get; }

        public Central(BusinessManager businessManager)
        {
            this._businessManager = businessManager;
            this._currentLevels = new Dictionary<InfrastructureLevelType, IInfrastructureLevel>();

            this.Name = "Central";
            this.Id = this._businessManager.GetId;
            this.Description = "Central node for wired internet network";
            this.Limit = 1;
            this.InfrastructureType = new WiredInternet();
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

        public bool CanUpgrade(IInfrastructureLevel newLevel)
        {
            return _businessManager.Money - newLevel?.BuildCost < 0;
        }

        public void Upgrade(IInfrastructureLevel newLevel)
        {
            if (newLevel == null)
                return;
            this._currentLevels[newLevel.InfrastructureLevelType] = newLevel;
        }

        public IEnumerable<IInfrastructureLevel> CurrentLevels => _currentLevels.Values;

        public Dictionary<InfrastructureLevelType, IEnumerable<IInfrastructureLevel>> Upgrades { get; }

        private void InitializeUpgrades()
        {
            var technologies = new List<IInfrastructureLevel>
            {
                new Upgrades.Speed1200Bits(),
                new Upgrades.Speed56K()
            };
            this.Upgrades[new Technology()] = technologies;
        }
    }
}