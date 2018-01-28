using GameTime;

namespace BusinessCore.InfrastructureModels.CellularCentral.Upgrades
{
    public class FiveG : IInfrastructureLevel
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

        public FiveG()
        {
            this.Name = "5G";
            this.Description = "Ok, this is still slow, but faster than before, right? RIGHT?";
            this.InfrastructureLevelType = InfrastructureLevelType.Technology;
            this.Level = 4;
            this.BuildCost = 24000;
            this.MaintenanceCost = 1600;
            this.SatisfactionProvided = 1.2;
            this.Range = 55;
            this.CreationDate = new TimeManager.GameTime() { Months = 0, Years = 2020 };
            this.ExpirationDate = new TimeManager.GameTime() { Months = 0, Years = 2050 };
        }
    }
}
