﻿using GameTime;

namespace BusinessCore.InfrastructureModels.CellTower.Upgrades
{
    public class LevelTwo : IInfrastructureLevel
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

        public LevelTwo()
        {
            this.Name = "Level Two";
            this.Description = "Ok, this is still low capacity, but bigger than before, right? RIGHT?";
            this.InfrastructureLevelType = InfrastructureLevelType.Capacity;
            this.Level = 1;
            this.BuildCost = 1000;
            this.MaintenanceCost = 40;
            this.SatisfactionProvided = 1.25;
            this.SatisfactionRange = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 1994 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 1998 };
        }
    }
}