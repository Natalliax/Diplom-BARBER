using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Models2
{
    internal class EntityContext : DbContext
    {
        
        public EntityContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DataBaseInitializer());

        }
        public DbSet<NameService> NameServices{ get; set; }
       
    }
}
