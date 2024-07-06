using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proekt_BarBer.Core
{
	public class MastSchedule
	{
        
        [Key]
		 public int Id { get; set; }
		
		public string Master { get; set; }
		public string Date { get; set; }
		public string Time{ get; set; }



		public override string ToString() => Master;
        //internal static string SelectedMasterTime;



    }
}
