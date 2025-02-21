using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
	public class ScholarshipsParameters : RequestParameters
	{
        public Funded? fund { get; set; }
        public Degree? degree { get; set; }
    }
}
