using GameTime;

namespace BusinessCore.InfrastructureModels.WiredInternetCentral.Upgrades
{
    public class VDSL : IInfrastructureLevel
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

        public VDSL()
        {
            this.Name = "VDSL";
            this.Description = "I can play to an mmorpg during my mother and my father are watching a movie on their computer !";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 4;
            this.BuildCost = 80000;
            this.MaintenanceCost = 12000;
            this.SatisfactionProvided = 1.25;
            this.SatisfactionRange = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 2012 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2030 };
        }
    }
}
