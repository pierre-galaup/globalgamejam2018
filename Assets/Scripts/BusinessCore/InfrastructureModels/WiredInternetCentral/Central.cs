using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral
{
    public class Central : MonoBehaviour, IInfratructure
    {
        private Dictionary<InfrastructureLevelType, IInfrastructureLevel> _currentLevels;
        private BusinessManager _businessManager;

        [SerializeField]
        private string _name = "Central";

        [SerializeField]
        private string _description = "Central node for wired internet network";

        [SerializeField]
        private int _limit = 1;

        [SerializeField]
        private InfrastructureType _infrastructureType = InfrastructureType.WiredInternet;

        public int Id { get; private set; }

        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            private set { _description = value; }
        }

        public int Limit
        {
            get { return _limit; }
            private set { _limit = value; }
        }

        public InfrastructureType InfrastructureType
        {
            get { return _infrastructureType; }
            private set { _infrastructureType = value; }
        }

        public Dictionary<InfrastructureLevelType, IEnumerable<IInfrastructureLevel>> Upgrades { get; private set; }

        public IEnumerable<IInfrastructureLevel> CurrentLevels => _currentLevels.Values;

        public double BuildCost
        {
            get { return this.CurrentLevels.Sum(level => level.BuildCost); }
        }

        public double MaintenanceCost
        {
            get { return this.CurrentLevels.Sum(level => level.MaintenanceCost); }
        }

        private void Awake()
        {
            this.Id = GameManager.Instance.BusinessManager.GetId;
            this._businessManager = GameManager.Instance.BusinessManager;
            this._currentLevels = new Dictionary<InfrastructureLevelType, IInfrastructureLevel>();
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