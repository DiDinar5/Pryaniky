using Microsoft.EntityFrameworkCore;

namespace Pryaniky.Models
{
    public class PryanikyContext: DbContext
    {
        public PryanikyContext(DbContextOptions<PryanikyContext> options) :base(options)
        {
        }
        public DbSet<Pryanik> Pryaniky { get; set; } = null!;
    }
}
