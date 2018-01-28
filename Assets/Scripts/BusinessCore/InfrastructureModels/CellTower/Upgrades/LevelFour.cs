using GameTime;

namespace BusinessCore.InfrastructureModels.CellTower.Upgrades
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
            this.Description = "Ok, this is still slow, but faster than before, right? RIGHT?";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 3;
            this.BuildCost = 12000;
            this.MaintenanceCost = 400;
            this.SatisfactionProvided = 1.1;
            this.Range = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 2008 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2030 };
        }
    }
}