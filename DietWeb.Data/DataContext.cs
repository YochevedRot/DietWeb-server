using DietWeb.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DietWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=_db");
        }

       /* internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
       */
    }
}
