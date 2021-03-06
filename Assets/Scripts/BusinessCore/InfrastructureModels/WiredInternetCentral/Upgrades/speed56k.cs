﻿using GameTime;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral.Upgrades
{
    public class Speed56K : IInfrastructureLevel
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

        public Speed56K()
        {
            this.Name = "56 kbit/s";
            this.Description = "Ok, this is still slow, but faster than before, right? RIGHT?";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 1;
            this.BuildCost = 10000;
            this.MaintenanceCost = 1500;
            this.SatisfactionProvided = 1.10;
            this.Range = 55;
            this.CreationDate = new TimeManager.GameTime() {Months = 0, Years = 1994};
            this.ExpirationDate = new TimeManager.GameTime(){Months = 0, Years = 1998};
        }
    }
}
