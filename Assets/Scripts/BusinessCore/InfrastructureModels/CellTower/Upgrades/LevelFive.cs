using GameTime;

namespace BusinessCore.InfrastructureModels.CellTower.Upgrades
{
    public class LevelFive : IInfrastructureLevel
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

        public LevelFive()
        {
            this.Name = "Level 5";
            this.Description = "Ok, this is still slow, but faster than before, right? RIGHT?";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 4;
            this.BuildCost = 24000;
            this.MaintenanceCost = 800;
            this.SatisfactionProvided = 1.25;
            this.Range = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 2008 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2030 };
        }
    }
}