﻿using GameTime;

namespace BusinessCore.InfrastructureModels.MainDistributionFrame.Upgrades
{
    public class LevelFour : IInfrastructureLevel
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

        public LevelFour()
        {
            this.Name = "Level 4";
            this.Description = "More Speed ! I can watch a video and play videogames!";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 3;
            this.BuildCost = 19000;
            this.MaintenanceCost = 450;
            this.SatisfactionProvided = 1.2;
            this.Range = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 1998 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2016 };
        }
    }
}