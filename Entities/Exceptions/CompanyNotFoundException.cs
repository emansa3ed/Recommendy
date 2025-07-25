﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(string companyId)
        : base($"The Company with id: {companyId} doesn't exist in the database.") 
        {
        }
    }
}
