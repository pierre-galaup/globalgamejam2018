namespace BusinessCore
{
    public interface IInfrastructureLevel
    {
        /// <summary>
        /// Name of the upgrade
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of the upgrade
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Level of the upgrade, from 1 to x
        /// </summary>
        int Level { get; }

        /// <summary>
        /// Cost of the upgrade
        /// </summary>
        double BuildCost { get; }

        /// <summary>
        /// Maintenance cost of 
        /// </summary>
        double MaintenanceCost { get; }

        /// <summary>
        /// Satisfaction provided by the upgrade
        /// </summary>
        double SatisfactionProvided { get; }

        /// <summary>
        /// Range of satisfaction
        /// </summary>
        double SatisfactionRange { get; }
    }
}
