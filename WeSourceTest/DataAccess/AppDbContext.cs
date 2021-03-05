using Microsoft.EntityFrameworkCore;
using WeSourceTest.DataAccess.Entities;

namespace WeSourceTest.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<CsvData> CsvData { get; set; }
        public DbSet<FixerData> FixerData { get; set; }
        public DbSet<CurrentRate> CurrentRate { get; set; }
    }
}
