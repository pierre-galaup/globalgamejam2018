﻿using GameTime;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral.Upgrades
{
    public class LevelOne : IInfrastructureLevel
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

        public LevelOne()
        {
            this.Name = "Level 1";
            this.Description = "This is low capacity, like reaaaaaaaally low capacity.";
            this.InfrastructureLevelType = InfrastructureLevelType.Capacity;
            this.Level = 0;
            this.BuildCost = 1500;
            this.MaintenanceCost = 100;
            this.SatisfactionProvided = 1.40;
            this.Range = 50;
            this.CreationDate = new TimeManager.GameTime { Months = 0, Years = 1980 };
            this.ExpirationDate = new TimeManager.GameTime { Months = 0, Years = 1990 };
        }
    }
}