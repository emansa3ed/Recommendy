﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class ScholarshipNotFoundException: NotFoundException
    {
        public ScholarshipNotFoundException(int scholarshipId)
            :base($"Scholarship with id: {scholarshipId} doesn't exist in the database.")
        {

        }
    }
}
