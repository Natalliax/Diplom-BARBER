using System;
using System.Data.Entity;
using System.Linq;
using Proekt_BarBer.Core;
using ZstdSharp.Unsafe;

namespace Proekt_BarBer.Core
{
    public class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<NameService> NameServices { get; set; }
        
        public DbSet<Material> Materials { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<MastSchedule> MastSchedules { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<DiscountInfo> DisInfo { get; set; }

        public DbSet<Cal> Cals { get; set; }

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
                    new Material{Name="Шампунь",Price= "40" },
                    new Material{Name="Бальзам",Price=  "25" },
                    new Material{Name="Лосьон", Price= "15" },
                    new Material{Name="Комплекс \"Уход\"",Price="65"},
                });
                SaveChanges();
            }

            if (!MastSchedules.Any())
            {
                MastSchedules.AddRange(sc = new MastSchedule[]
                {
                    new MastSchedule { Master = "Богдан",  Date = "29.07.2024" , Time = "12:00", IsRegistered = true },
                    new MastSchedule { Master = "Евгений", Date = "10.08.2024", Time = "17:00", IsRegistered = true},  
                    new MastSchedule { Master = "Олег", Date = "06.08.2024", Time = "10:00", IsRegistered = true},   
                    new MastSchedule { Master = "Стас", Date = "01.08.2024",  Time= "19:00", IsRegistered = true}, 
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
                    new DiscountInfo { NameDis = "Процент мастеру за доп услуги", Persent = 0.1m },

                 });

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
