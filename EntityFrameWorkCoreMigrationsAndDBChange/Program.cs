using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreMigrationsAndDBChange
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public bool IsMarried { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext()
        {
            //    Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
    }
    public class Program
    {

        static void Main(String[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
                // асинхронная версия
                //await db.Database.EnsureCreatedAsync();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                bool isCreated = db.Database.EnsureCreated();
                // bool isCreated2 = await db.Database.EnsureCreatedAsync();
                if (isCreated) Console.WriteLine("База данных была создана");
                else Console.WriteLine("База данных уже существует");
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                bool isAvalaible = db.Database.CanConnect();
                // bool isAvalaible2 = await db.Database.CanConnectAsync();
                if (isAvalaible) Console.WriteLine("База данных доступна");
                else Console.WriteLine("База данных не доступна");
            }
        }
    }
}