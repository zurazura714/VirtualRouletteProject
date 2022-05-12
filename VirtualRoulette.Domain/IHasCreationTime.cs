using System;

namespace VirtualRoulette.Domain
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTimeOffset CreationTime { get; set; }
    }
}
