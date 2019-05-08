using Microsoft.EntityFrameworkCore;
using WAP.DataAccess.Models;

namespace WAP.DataAccess.DAL
{
    public class WapContext : DbContext
    {
        public DbSet<QueryLog> QueryLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=wap.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
