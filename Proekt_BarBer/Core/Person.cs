using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proekt_BarBer.Core
{

    public class Person
    {

        [Key] public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }

        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price { get; set; }
		
		public virtual DiscountInfo DiscountInfo { get; set; }
       
        public virtual NameService NameService { get; set; }
       
        public virtual Material Material { get; set; }

        //public string Master { get; set; }
        public virtual Order Order { get; set; }

        public virtual MastSchedule MastSchedule{ get; set; }
       


    }
}









