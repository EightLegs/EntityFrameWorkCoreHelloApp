using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Microsoft.Extensions.Configuration;

namespace EntityFrameworkCoreEnsureCreated
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }


    public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // получаем строку подключения из файла appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public string ConnectionString { get; set; }

        public ApplicationContext(string connectionString)
        {
            this.ConnectionString = connectionString;
            Database.EnsureDeleted(); // гарантируем, что бд удалена
            Database.EnsureCreated();   // гарантируем, что БД создана
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=helloappOnConfiguring.db");
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
            //optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });

        }
    }

    //Логирование в файл
    /*
     public class ApplicationContext : DbContext
{
    readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
        optionsBuilder.LogTo(logStream.WriteLine);
    }
    public override void Dispose()
    {
        base.Dispose();
        logStream.Dispose();
    }
 
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await logStream.DisposeAsync();
    }
}
     */


    public class Program
    {

        static void Main(String[] args)
        {

            /*
            using (ApplicationContext db = new ApplicationContext("Data Source=helloapp.db"))
            {
                var users = db.Users.ToList();
                Console.WriteLine("Пользователи:");
                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
                }
            }*/


            /*var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var options = optionsBuilder.UseSqlite("Data Source=helloapp.db").Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                var users = db.Users.ToList();

                foreach(User user in users)
                    Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
            }*/

            /*            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlite(connectionString).Options;*/

            using (ApplicationContext db = new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>().Options))
            {
                var users = db.Users.ToList();
                foreach (User user in users)
                    Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
            }
        }
    }
}