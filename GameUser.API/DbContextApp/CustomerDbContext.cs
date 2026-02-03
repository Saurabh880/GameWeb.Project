using GameUser.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace GameUser.API.DbContextApp
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
        {
            
        }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (databaseCreator.CanConnect()) databaseCreator.Create();
                    if (databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public DbSet<GameUsers> CustomerModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
