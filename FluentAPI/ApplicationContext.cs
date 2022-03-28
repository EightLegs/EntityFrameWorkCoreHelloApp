using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI
{
    public class ApplicationContext : DbContext
    {
        //public DbSet<User> Users { get; set; } = null!;
        public DbSet<User> Users => Set<User>();
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            //Не создаем столбец
            modelBuilder.Entity<User>().Ignore(u => u.Address);
            //Создание сущности Country
            //modelBuilder.Entity<Country>();
            //Исключение сущности Company
            //modelBuilder.Ignore<Company>();
            // использование Fluent API
            //base.OnModelCreating(modelBuilder);
        }
    }
}
