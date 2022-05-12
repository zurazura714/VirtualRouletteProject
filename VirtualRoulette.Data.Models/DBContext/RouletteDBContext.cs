using Microsoft.EntityFrameworkCore;
using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Data.Models.DBContext
{
    /// <summary>
    /// Creating Model DBContext.
    /// </summary>
    public class RouletteDBContext : DbContext, IUnitOfWork
    {
        public RouletteDBContext(DbContextOptions<RouletteDBContext> options) : base(options)
        {
        }

        public DbSet<Spin> Spins { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<SessionToken> SessionTokens { get; set; }
        public DbSet<JackPot> JackPots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Spin>().HasOne(e => e.AppUser).WithMany(e => e.spins);
            modelBuilder.Entity<Spin>().HasOne(x => x.SessionToken).WithMany(e => e.Spins);
            modelBuilder.Entity<SessionToken>().HasOne(e => e.AppUser).WithMany(e => e.Tokens);
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}
