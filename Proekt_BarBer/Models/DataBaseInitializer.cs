using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Models
{
    internal class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
        
        protected override void Seed(EntityContext context)
        {
            context.Persons.AddRange(new Person[]
            {
                new Person { Date = "27.12.2023", Time = "10:00", Client = "John Smith", Service = "Бритье",
                    Master = "Богдан", Price = 35m},
                new Person { Date = "28.12.2023", Time = "15:00", Client = "Miranda", Service = "Стрижка",
                    Master = "Руслан", Price = 65m}

            });
        }
    }
}
