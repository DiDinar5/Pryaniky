using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Pryaniky.Models
{
    public class PryanikyContext : DbContext
    {
        public PryanikyContext(DbContextOptions<PryanikyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Pryanik> Pryaniky { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  base.OnModelCreating(modelBuilder);
            modelBuilder
            .Entity<Order>()
            .HasMany(o => o.Pryaniks)
            .WithMany()
            .UsingEntity(j => j.ToTable("OrderPryanik"));
        }
    }
}
