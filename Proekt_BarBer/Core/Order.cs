using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Proekt_BarBer.Core
{
    /// <summary> Заказ </summary>
    public class Order
    {
        [Key] public int Id { get; set; }

        ///<summary> Услуга </summary>
        public virtual NameService NameService { get; set; }

        ///<summary> Доп.услуга </summary>
        

        public virtual List<Material> Materials { get; set; }

        public virtual MastSchedule MastSchedules { get; set; }

        public virtual Users Users { get; set; }

        public virtual DiscountInfo DisInfo { get; set; }


		


        
    }
}
