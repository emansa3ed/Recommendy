using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStudentRepository
    {
        void CreateStudent(Student student);
        Student GetStudent(string studentId, bool trackChanges);
        public void UpdateStudent(Student student);
      

    }
}
