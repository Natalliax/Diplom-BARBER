using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt_BarBer.Core
{
	public class Users
	{
		[Key]
		public int Id { get; set; }
		public string login { get; set; }
		public string pass { get; set; }

		public string email { get; set; }

		public override string ToString() => login;

		public Users() {}

		public Users(string login, string pass, string email)
		{
			
			this.login = login;
			this.pass = pass;
			this.email = email;
		}
	}
}
