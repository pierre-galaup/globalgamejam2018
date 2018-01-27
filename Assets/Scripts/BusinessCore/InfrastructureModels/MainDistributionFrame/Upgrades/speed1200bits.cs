﻿using GameTime;

namespace BusinessCore.InfrastructureModels.MainDistributionFrame.Upgrades
{
    public class Speed1200Bits : IInfrastructureLevel
    {
        public string Name { get; }
        public string Description { get; }
        public InfrastructureLevelType InfrastructureLevelType { get; }
        public int Level { get; }
        public double BuildCost { get; }
        public double MaintenanceCost { get; }
        public double SatisfactionProvided { get; }
        public double SatisfactionRange { get; }
        public TimeManager.GameTime CreationDate { get; }
        public TimeManager.GameTime ExpirationDate { get; }

        public Speed1200Bits()
        {
            this.Name = "1200 bit/s";
            this.Description = "This is slow, like reaaaaaaaally slow.";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 0;
            this.BuildCost = 1500;
            this.MaintenanceCost = 40;
            this.SatisfactionProvided = 1.40;
            this.SatisfactionRange = 50;
            this.CreationDate = new TimeManager.GameTime { Months = 0, Years = 1980 };
            this.ExpirationDate = new TimeManager.GameTime { Months = 0, Years = 1990 };
        }
    }
}