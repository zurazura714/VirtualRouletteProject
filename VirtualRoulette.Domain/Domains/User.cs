using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRoulette.Common.Abstractions.Entity;

namespace VirtualRoulette.Domain.Domains
{
    [Table("Users")]
    public class User : IHasCreationTime
    {
        public int ID { get; set; }
        [Required]
        [Range(1, long.MaxValue)]
        public long ApplicationUserId { get; set; }
        public DateTimeOffset CreationTime { get; set; }
    }
}
