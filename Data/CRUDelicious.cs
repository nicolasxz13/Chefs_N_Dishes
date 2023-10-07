using Chefs_N_Dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace Chefs_N_Dishes.Data
{
    public class DeliciousContext : DbContext
    {
        public DeliciousContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Dishe> Dishes { get; set; }
        public DbSet<Chef> Chefs { get; set; }
    }
}
