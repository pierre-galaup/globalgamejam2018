using GameTime;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral.Upgrades
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
        public double SatisfactionRange { get; }
        public TimeManager.GameTime CreationDate { get; }
        public TimeManager.GameTime ExpirationDate { get; }

        public LevelThree()
        {
            this.Name = "Level Three";
            this.Description = "Amazing, more speed ! Well.. It's not perfect... But works";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 2;
            this.BuildCost = 20000;
            this.MaintenanceCost = 1000;
            this.SatisfactionProvided = 1.15;
            this.SatisfactionRange = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 1996 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2000 };
        }
    }
}
