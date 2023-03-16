using FormulaOneApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        // Connect Model to Database with run migration
        public DbSet<Team> Teams { get; set; }
    }
}
