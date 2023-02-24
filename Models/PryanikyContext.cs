using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Pryaniky.Models
{
    public class PryanikyContext: DbContext
    {
        public PryanikyContext(DbContextOptions<PryanikyContext> options) :base(options)
        {
        }
        public DbSet<Pryanik> Pryaniky { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
             .HasMany<Pryanik>(o => o.Pryaniks)
             .WithMany("Orders");
        }
    }
}
