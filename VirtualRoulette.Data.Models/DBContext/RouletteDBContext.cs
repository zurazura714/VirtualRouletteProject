using Microsoft.EntityFrameworkCore;
using VirtualRoulette.Data.Models.Models;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace VirtualRoulette.Data.Models.DBContext
{
    /// <summary>
    /// Creating Model DBContext.
    /// </summary>
    public class RouletteDBContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public RouletteDBContext(DbContextOptions<RouletteDBContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var a = ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString.ToString();

            //var text = @"data source=(localdb)mssqllocaldb;initial catalog=usTest;integrated security=True;MultipleActiveResultSets=True";
            //SqlConnection sq = null;
            //optionsBuilder.UseSqlServer(sq);


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
