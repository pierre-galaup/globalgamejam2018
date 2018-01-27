using System;
using System.Collections.Generic;

namespace BusinessCore
{
    /// <summary>
    /// Represent an infrastructure object, like NRO, Central, whatever
    /// </summary>
    public interface IInfratructure
    {
        /// <summary>
        /// Name of the infrastructure
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of the infrastructure
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Check if infrastructure can be upgraded
        /// </summary>
        /// <returns>True if upgrade can be performed, else False</returns>
        bool CanUpgrade();

        /// <summary>
        /// Upgrade the infrastructure to the next level
        /// </summary>
        void Upgrade();

        /// <summary>
        /// Current level of the infrastructure
        /// </summary>
        IInfrastructureLevel InfrastructureLevel { get; }

        /// <summary>
        /// List of available levels.
        /// We need a List here, because it MUST be ordered and IEnumerable do not garantie that it is ordered.
        /// </summary>
        List<IInfrastructureLevel> UpgradesLevels { get; }

        /// <summary>
        /// </summary>
        event EventHandler InfrastructureBuilt;
    }
}