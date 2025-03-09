using Microsoft.AspNetCore.Http;
using Shared.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IStudentService
    {
        StudentDto GetStudent(string studentId, bool trackChanges);
      

        Task UpdateStudentProfileAsync(string studentId, StudentForUpdateDto studentForUpdate);

    }
}
