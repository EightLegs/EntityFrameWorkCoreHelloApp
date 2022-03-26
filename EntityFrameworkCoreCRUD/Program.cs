using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreCRUD
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        //public ApplicationContext() => Database.EnsureCreated();
        public ApplicationContext()
        {
            Database.EnsureDeleted(); // гарантируем, что бд удалена
            Database.EnsureCreated();   // гарантируем, что БД создана
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloappCRUD.db");
        }
    }

    public class Program
    {
        static void Main(String[] args)
        {
            // Добавление
            using (ApplicationContext db = new ApplicationContext())
            {
                User tom = new User { Name = "Tom", Age = 33 };
                User alice = new User { Name = "Alice", Age = 26 };

                // Добавление
                db.Users.Add(tom);
                db.Users.Add(alice);
                db.SaveChanges();

                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Данные после добавления:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }

                User cRAD = new User { Name = "CRAD", Age = 33 };
                User sata = new User { Name = "sata", Age = 26 };
                List<User> userList = new List<User>() { cRAD, sata };
                db.Users.AddRange(userList);
                db.SaveChanges();
                
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }

                // получаем первый объект
                User? user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    user.Name = "Bob";
                    user.Age = 44;
                    //обновляем объект
                    //db.Users.Update(user);
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после редактирования:");
                users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }

                // получаем первый объект
                user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    //удаляем объект
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                // выводим данные после обновления
                Console.WriteLine("\nДанные после удаления:");
                users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
        }
    }
}