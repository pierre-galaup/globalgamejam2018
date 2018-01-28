using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace BusinessCore.InfrastructureModels
{
    public abstract class BaseInfrastructure : MonoBehaviour, IInfrastructure
    {
        protected Dictionary<InfrastructureLevelType, IInfrastructureLevel> CurrentLevelsDict =
            new Dictionary<InfrastructureLevelType, IInfrastructureLevel>();

        protected BusinessManager BusinessManager;

        public int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual int Limit { get; protected set; }

        public virtual bool IsCentral { get; protected set; }

        public virtual int Range { get; protected set; }

        public virtual InfrastructureType InfrastructureType { get; protected set; }

        public IInfrastructureLevel GetCurrentLevel(InfrastructureLevelType infrastructureLevelType)
        {
            return !this.CurrentLevelsDict.ContainsKey(infrastructureLevelType)
                ? null
                : this.CurrentLevelsDict[infrastructureLevelType];
        }

        public Dictionary<InfrastructureLevelType, IEnumerable<IInfrastructureLevel>> Upgrades { get; protected set; }

        public IEnumerable<IInfrastructureLevel> CurrentLevels => CurrentLevelsDict.Values;

        public double BuildCost
        {
            get { return this.CurrentLevels.Sum(level => level.BuildCost); }
        }

        public double MaintenanceCost
        {
            get { return this.CurrentLevels.Sum(level => level.MaintenanceCost); }
        }

        protected virtual void Awake()
        {
            this.Id = GameManager.Instance.BusinessManager.GetId;
            this.BusinessManager = GameManager.Instance.BusinessManager;
            this.Upgrades = new Dictionary<InfrastructureLevelType, IEnumerable<IInfrastructureLevel>>();
            this.InitializeUpgrades();

            foreach (var infrastructureLevels in this.Upgrades)
            {
                var firstLevel = infrastructureLevels.Value.FirstOrDefault(level => level.Level == 0);
                if (firstLevel == null)
                    continue;
                this.CurrentLevelsDict[firstLevel.InfrastructureLevelType] = firstLevel;
            }
        }

        public virtual bool CanUpgrade(InfrastructureLevelType typeToUpgrade)
        {
            if (!this.Upgrades.ContainsKey(typeToUpgrade))
                return false;
            var upgradeLevel = this.GetNextUpgrade(typeToUpgrade);
            if (upgradeLevel == null)
                return false;
            //var currentTime = BusinessManager.GameManager.TimeManager.GetGameTime();
            //if (currentTime < upgradeLevel.CreationDate)
            //    return false;
            return BusinessManager.Money - upgradeLevel?.BuildCost > 0;
        }

        public virtual IInfrastructureLevel Upgrade(InfrastructureLevelType typeToUpgrade)
        {
            if (!this.CanUpgrade(typeToUpgrade))
                return null;
            this.CurrentLevelsDict[typeToUpgrade] = this.GetNextUpgrade(typeToUpgrade);
            return this.CurrentLevelsDict[typeToUpgrade];
        }

        public virtual IInfrastructureLevel GetNextUpgrade(InfrastructureLevelType upgradeType)
        {
            IInfrastructureLevel upgrade = null;
            if (!this.CurrentLevelsDict.ContainsKey(upgradeType))
                upgrade = this.Upgrades[upgradeType].FirstOrDefault();
            else
            {
                var currentLevel = this.CurrentLevelsDict[upgradeType];
                upgrade = this.Upgrades[upgradeType].FirstOrDefault(e => e.Level == currentLevel.Level + 1);
            }
            return upgrade;
        }

        protected abstract void InitializeUpgrades();
    }
}