using System;
using Microsoft.AspNetCore.Identity;
using VirtualRoulette.Common.Abstractions.Entity;

namespace VirtualRoulette.Data.Users.Entities
{
    public class ApplicationUser : IdentityUser<long>, IHasCreationTime
    {
        public DateTimeOffset CreationTime { get; set; }

        public ApplicationUser()
        {
            CreationTime = DateTimeOffset.UtcNow;
        }
    }
}
