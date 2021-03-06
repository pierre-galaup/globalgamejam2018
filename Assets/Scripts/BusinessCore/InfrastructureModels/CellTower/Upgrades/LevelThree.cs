﻿using GameTime;

namespace BusinessCore.InfrastructureModels.CellTower.Upgrades
{
    public class LevelThree : IInfrastructureLevel
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

        public LevelThree()
        {
            this.Name = "Level 3";
            this.Description = "Ok, this is still slow, but faster than before, right? RIGHT?";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 2;
            this.BuildCost = 6000;
            this.MaintenanceCost = 160;
            this.SatisfactionProvided = 1.1;
            this.Range = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 2000 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2016 };
        }
    }
}