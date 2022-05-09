using System;

namespace VirtualRoulette.Common.Abstractions.Entity
{
    /// <summary>
    /// An entity can implement this interface if <see cref="CreationTime"/> of this entity must be stored.
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTimeOffset CreationTime { get; set; }
    }
}
