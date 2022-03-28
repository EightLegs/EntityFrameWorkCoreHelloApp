using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkCoreDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
            //MS SQL Server
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=helloappdb;Trusted_Connection=True;");
            //MySQL
            //optionsBuilder.UseMySql("server=localhost;user=root;password=123456789;database=usersdb;",
            //new MySqlServerVersion(new Version(8, 0, 25)));
            //PostgreSQL
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=пароль_от_postgres");
        }
    }
}
