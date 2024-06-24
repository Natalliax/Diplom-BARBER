using System;
using System.Data.Entity;
using System.Linq;
using Proekt_BarBer.Core;
using ZstdSharp.Unsafe;

namespace Proekt_BarBer.Core
{
    internal class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<NameService> NameServices { get; set; }
        
        public DbSet<Material> Materials { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<MastSchedule> MastSchedules { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<DiscountInfo> DisInfo { get; set; }

		public MyContext() : base("DefaultConnection")
        {
            DataBaseInitializer();
        }

        private void DataBaseInitializer()
        {
            NameService[] ns=null;
           
            Material[] m=null;
            MastSchedule[] sc=null;
            Users[] us=null;
            DiscountInfo[] dis=null;

            if (!NameServices.Any())
            {
                NameServices.AddRange(ns=new NameService[]
                {
                    new NameService { Usl = "Бритье", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут"},
                    new NameService { Usl = "Стрижка", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут" },
                    new NameService { Usl = "Королевское бритье", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут" }
                });
                SaveChanges();
            }

            

            if (!Materials.Any())
            {
                Materials.AddRange(m=new Material[]
                {
                    new Material{Name="Шампунь",Price= 40m },
                    new Material{Name="Бальзам",Price=  25m },
                    new Material{Name="Лосьон", Price= 15m },
                    new Material{Name="Комплекс \"Уход\"",Price=65m},
                });
                SaveChanges();
            }

            if (!MastSchedules.Any())
            {
                MastSchedules.AddRange(sc = new MastSchedule[]
                {
                    new MastSchedule { Master = "Богдан",  Date = "09.06.2024" , Time = "12:00"},
                    new MastSchedule { Master = "Евгений", Date = "10.06.2024", Time = " " },  
                    new MastSchedule { Master = "Олег", Date = "06.06.2024", Time = "12:00"},   
                    new MastSchedule { Master = "Стас", Date = "09.06.2024",  Time= " "}, 
                });
                SaveChanges();
            }

            if (!DisInfo.Any())
            {
                DisInfo.AddRange(dis = new DiscountInfo[]
                 {
                    new DiscountInfo { NameDis = "Скидка на день рождение", Persent = 0.2m },
                    new DiscountInfo { NameDis = "Скидка персональная", Persent = 0.1m },
					new DiscountInfo { NameDis = "Процент мастеру", Persent = 0.4m },

				 });

            }
            
			if (!Persons.Any())
            {
                Persons.AddRange(new Person[]
                {

                    new Person { 
                        Date="27.12.2023", 
                        Time ="10:00", 
                        Name = "Джон ", 
                        NameService = ns[0],
                        Material = m[0],
                        MastSchedule = sc[0],
                        Price = 30m},
                  
                    new Person {
                        Date="28.12.2023", 
                        Time="15:00", 
                        Name = "Богдан", 
						NameService = ns[1],
                        Material = m[1],
                        MastSchedule = sc[1],
                        Price = 35m}
                }) ;
                SaveChanges();
            }

			if (!Users.Any())
			{
				Users.AddRange(us = new Users[]
				{
					new Users {login="Natal",pass = "7534392", email = "6372437@gmail.com"  },
                    new Users {login="Mars",pass = "Co787878", email = "7534392@gmail.com"  }

				});
				SaveChanges();
			}

		}
    }
}
