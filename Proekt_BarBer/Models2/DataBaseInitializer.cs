using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Models2
{
    internal class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
        
        protected override void Seed(EntityContext context1)
        {
            context1.NameServices.AddRange(new NameService[]
            {
                new NameService { Id = 1,  Usl = "Бритье", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут"},
                
                new NameService { Id = 2,  Usl = "Стрижка", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут" },
                
                new NameService { Id = 3,  Usl = "Королевское бритье", Price = 35m, Description = "Бритва стандартная", Timecomletion = "35 минут" }
            });
        }
    }
}
