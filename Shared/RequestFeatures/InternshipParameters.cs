using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class InternshipParameters :RequestParameters
    {
		public bool ? Paid{ get; set; }

		public override string ToString()
		{
			return base.ToString() + Paid.ToString();
		}
	}
}
