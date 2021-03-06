﻿using System.Collections.Generic;
using UnityEngine;

namespace BusinessCore.InfrastructureModels.MainDistributionFrame
{
    public class MDF : BaseInfrastructure
    {
        [SerializeField]
        private string _name = "MDF";

        [SerializeField]
        private string _description = "Main Distribution Frame";

        [SerializeField]
        private int _limit = 50;

        [SerializeField]
        private InfrastructureType _infrastructureType = InfrastructureType.WiredInternet;

        [SerializeField]
        private int _range = 5;

        [SerializeField]
        private bool _isCentral = false;

        public override string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public override string Description
        {
            get { return _description; }
            protected set { _description = value; }
        }

        public override int Limit
        {
            get { return _limit; }
            protected set { _limit = value; }
        }

        public override int Range
        {
            get { return _range; }
            protected set { _range = value; }
        }

        public override bool IsCentral
        {
            get { return _isCentral; }
            protected set { _isCentral = value; }
        }

        public override InfrastructureType InfrastructureType
        {
            get { return _infrastructureType; }
            protected set { _infrastructureType = value; }
        }

        protected override void InitializeUpgrades()
        {
            var technologies = new List<IInfrastructureLevel>
            {
                new Upgrades.Speed1200Bits(),
                new Upgrades.Speed56K(),
                new Upgrades.AOL(),
                new Upgrades.ADSL(),
                new Upgrades.VDSL(),
                new Upgrades.Fibre()
            };
            this.Upgrades[InfrastructureLevelType.Technology] = technologies;

            var capacity = new List<IInfrastructureLevel>
            {
                new Upgrades.LevelOne(),
                new Upgrades.LevelTwo(),
                new Upgrades.LevelThree(),
                new Upgrades.LevelFour(),
                new Upgrades.LevelFive(),
                new Upgrades.LevelSix()
            };
            this.Upgrades[InfrastructureLevelType.Capacity] = capacity;

        }
    }
}