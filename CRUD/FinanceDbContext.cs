using Microsoft.EntityFrameworkCore;

namespace CRUD
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
} 