using GameTime;

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

        InfrastructureLevelType InfrastructureLevelType { get; }

        /// <summary>
        /// Level of the upgrade, from 0 to x. 0 is the default value
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

        /// <summary>
        /// In which year this item is available
        /// </summary>
        TimeManager.GameTime CreationDate { get; }

        /// <summary>
        /// In which year this infrastructure become deprecated
        /// </summary>
        TimeManager.GameTime ExpirationDate { get; }
    }
}
