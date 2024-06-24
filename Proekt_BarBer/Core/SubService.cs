using System.ComponentModel.DataAnnotations;

namespace Proekt_BarBer.Core
{
    public class SubService
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString() => Name;
    }
}
