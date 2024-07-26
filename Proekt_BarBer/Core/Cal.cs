using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Core
{
    public class Cal
    {
        [Key]
        public int Id { get; set; }
        public string Master {  get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }
        public string Procent1 { get; set; }
        public string Procent2 { get; set; }
        public string Summa1 { get; set; }
        public string Summa2 { get; set; }
        public string Total { get; set; }
    }
}
