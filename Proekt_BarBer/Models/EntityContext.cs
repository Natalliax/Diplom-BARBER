using Proekt_BarBer.Core;
using Proekt_BarBer.Models2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Models
{
    internal class MyContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<NameService> NameServices { get; set; }
        public DbSet<SubService> SubServices { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public MyContext() : base("DefaultConnection")
        {
            DataBaseInitializer();
        }
        private void DataBaseInitializer()
        {
            if (!NameServices.Any())
            {
                NameServices.AddRange(new[]
                {
                    new NameService { Id = 1,  Usl = "Бритье", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут"},
                    new NameService { Id = 2,  Usl = "Стрижка", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут" },
                    new NameService { Id = 3,  Usl = "Королевское бритье", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут" }
                });
            }

            if (!SubServices.Any())
            {
                SubServices.AddRange(new[] 
                {
                    new SubService{Name="Королевское бритье", Price=20m },
                    new SubService{Name="Стрижка бороды",  Price=40m },
                    new SubService{Name="Депиляция волос носа",  Price=25m },
                    new SubService{Name="Бритье",  Price=45m },
                    new SubService{Name="Стрижка",  Price=65m },
                    new SubService{Name="Стрижка волос",  Price=15m }
               });
            }

            if (!Persons.Any())
            {
                Persons.AddRange(new[] {
                new Person
                {
                    Date = "27.12.2023",
                    Time = "10:00",
                    Client = "John Smith",
                    Service = "Бритье",
                    Master = "Богдан",
                    Price = 35m
                },
                new Person
                {
                    Date = "28.12.2023",
                    Time = "15:00",
                    Client = "Miranda",
                    Service = "Стрижка",
                    Master = "Руслан",
                    Price = 65m
                } });
            }
            SaveChanges();
        }
    }
}
