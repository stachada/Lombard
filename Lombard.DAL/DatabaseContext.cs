using Microsoft.EntityFrameworkCore;
using Lombard.BL.Models;

namespace Lombard.DAL
{
    public class DatabaseContext: DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Lombard.db");
        }
    }
}
