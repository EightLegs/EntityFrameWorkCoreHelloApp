using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace FluentAPI
{
    public partial class User
    {
        [Column("user_id")]
        public long Id { get; set; }
        public string? Name { get; set; }
        public long Age { get; set; }
        //Навигационное свойство
        public Company? Company { get; set; } 
    }
    //Если не нужно создавать
    //[NotMapped] 
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    //Не связано с DBSet, поэтому не будет создано. Для создания требуется указать, например в OnModelCreating
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
