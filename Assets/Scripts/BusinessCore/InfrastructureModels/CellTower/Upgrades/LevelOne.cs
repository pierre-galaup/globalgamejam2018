using GameTime;

namespace BusinessCore.InfrastructureModels.CellTower.Upgrades
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
        public double SatisfactionRange { get; }
        public TimeManager.GameTime CreationDate { get; }
        public TimeManager.GameTime ExpirationDate { get; }

        public LevelOne()
        {
            this.Name = "Level One";
            this.Description = "This is low capacity, like reaaaaaaaally low capacity.";
            this.InfrastructureLevelType = InfrastructureLevelType.Capacity;
            this.Level = 0;
            this.BuildCost = 500;
            this.MaintenanceCost = 20;
            this.SatisfactionProvided = 1.40;
            this.SatisfactionRange = 50;
            this.CreationDate = new TimeManager.GameTime { Months = 0, Years = 1980 };
            this.ExpirationDate = new TimeManager.GameTime { Months = 0, Years = 1990 };
        }
    }
}