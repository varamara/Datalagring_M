using Datalagring_M.Models;
using Datalagring_M.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_M.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mikah\OneDrive\Documents\database_local.mdf;Integrated Security=True;Connect Timeout=30";

        #region constructors

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #endregion

        #region overrides

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerEntity>()
                .HasMany(c => c.Incidents)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    

        #endregion


        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<IncidentEntity> Incidents { get; set; }
    }
}
