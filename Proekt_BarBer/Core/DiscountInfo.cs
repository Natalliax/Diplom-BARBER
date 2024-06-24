using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Core
{
	public class DiscountInfo
	{
		[Key]
		public int Id { get; set; }
		public string NameDis { get; set; }

		public decimal Persent { get; set; }


		public override string ToString() => NameDis;
	}
}


	



