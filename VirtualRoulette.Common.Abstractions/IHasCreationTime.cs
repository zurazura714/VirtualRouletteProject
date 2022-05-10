using System;

namespace VirtualRoulette.Common.Abstractions.Entity
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTimeOffset CreationTime { get; set; }
    }
}
