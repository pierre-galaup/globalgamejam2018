﻿using GameTime;

namespace BusinessCore.InfrastructureModels.CellTower.Upgrades
{
    public class OneG : IInfrastructureLevel
    {
        public string Name { get; }
        public string Description { get; }
        public InfrastructureLevelType InfrastructureLevelType { get; }
        public int Level { get; }
        public double BuildCost { get; }
        public double MaintenanceCost { get; }
        public double SatisfactionProvided { get; }
        public int Range { get; }
        public TimeManager.GameTime CreationDate { get; }
        public TimeManager.GameTime ExpirationDate { get; }

        public OneG()
        {
            this.Name = "1G";
            this.Description = "This is slow, like reaaaaaaaally slow.";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 0;
            this.BuildCost = 500;
            this.MaintenanceCost = 20;
            this.SatisfactionProvided = 1.40;
            this.Range = 50;
            this.CreationDate = new TimeManager.GameTime { Months = 0, Years = 1980 };
            this.ExpirationDate = new TimeManager.GameTime { Months = 0, Years = 1990 };
        }
    }
}