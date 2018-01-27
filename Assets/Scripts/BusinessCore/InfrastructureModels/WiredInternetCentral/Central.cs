﻿using System.Collections.Generic;
using UnityEngine;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral
{
    public class Central : BaseInfrastructure
    {
        [SerializeField]
        private string _name = "Central";

        [SerializeField]
        private string _description = "Central node for wired internet network";

        [SerializeField]
        private int _limit = 1;

        [SerializeField]
        private InfrastructureType _infrastructureType = InfrastructureType.WiredInternet;

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
                new Upgrades.Speed56K()
            };
            this.Upgrades[InfrastructureLevelType.Technology] = technologies;
        }
    }
}