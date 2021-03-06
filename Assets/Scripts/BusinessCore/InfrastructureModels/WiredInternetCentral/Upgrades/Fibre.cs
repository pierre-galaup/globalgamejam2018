﻿using GameTime;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral.Upgrades
{
    public class Fibre : IInfrastructureLevel
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

        public Fibre()
        {
            this.Name = "Fiber";
            this.Description = "Too much speed ! My computer can't follow my network ! Love it";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 5;
            this.BuildCost = 160000;
            this.MaintenanceCost = 24000;
            this.SatisfactionProvided = 1.3;
            this.Range = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 2020 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2050 };
        }
    }
}