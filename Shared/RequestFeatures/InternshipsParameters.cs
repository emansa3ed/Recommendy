using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
	public class InternshipsParameters : RequestParameters
	{
		public string? searchTerm { get; set; }
		public Funded? fund { get; set; }
		public Degree? degree { get; set; }

		public override string ToString()
		{
			return base.ToString() + fund.ToString() + degree.ToString();
		}
	}
}
