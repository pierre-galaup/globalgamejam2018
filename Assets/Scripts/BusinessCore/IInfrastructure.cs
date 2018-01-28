using System;
using System.Collections.Generic;

namespace BusinessCore
{
    /// <summary>
    /// Represent an infrastructure object, like NRO, Central, whatever
    /// </summary>
    public interface IInfrastructure
    {
        /// <summary>
        /// Object id
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Name of the infrastructure
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of the infrastructure
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Limit of infrastructure of this type that you can use
        /// </summary>
        int Limit { get; }

        double BuildCost { get; }
        double MaintenanceCost { get; }

        bool IsCentral { get; }
        int Range { get; }

        /// <summary>
        /// Type of the infrastructure
        /// </summary>
        InfrastructureType InfrastructureType { get; }

        /// <summary>
        /// Check if infrastructure can be upgraded
        /// </summary>
        /// <returns>True if upgrade can be performed, else False</returns>
        bool CanUpgrade(InfrastructureLevelType typeToUpgrade);

        /// <summary>
        /// Upgrade the infrastructure to the next level
        /// </summary>
        /// <returns>The upgraded object</returns>
        IInfrastructureLevel Upgrade(InfrastructureLevelType typeToUpgrade);

        /// <summary>
        /// Get the next upgrade available for the given type
        /// </summary>
        /// <param name="upgradeType">Type of upgrade</param>
        /// <returns>The upgrade level object.  Can be null if no upgrade.</returns>
        IInfrastructureLevel GetNextUpgrade(InfrastructureLevelType upgradeType);

        /// <summary>
        /// Current levels of the infrastructure upgrades
        /// </summary>
        IEnumerable<IInfrastructureLevel> CurrentLevels { get; }

        /// <summary>
        /// Return the current level of the given infrastructure type
        /// </summary>
        /// <param name="infrastructureLevelType"></param>
        /// <returns></returns>
        IInfrastructureLevel GetCurrentLevel(InfrastructureLevelType infrastructureLevelType);

        /// <summary>
        /// List of available levels.
        /// We need a List here, because it MUST be ordered and IEnumerable do not garantie that it is ordered.
        /// </summary>
        Dictionary<InfrastructureLevelType, IEnumerable<IInfrastructureLevel>> Upgrades { get; }
    }
}