using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proekt_BarBer.Core
{
    public class Material
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        public override string ToString() => Name;
    }
}
