using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Core
{
    public class Report
    {
        [Key] public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
        public virtual NameService NameService { get; set; }
        
        public virtual Material Material { get; set; }
        public string Master { get; set; }
        public virtual Order Order { get; set; }
        [NotMapped]
        public decimal Price {  get; set; }
        
    }
}
