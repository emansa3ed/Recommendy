using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepository : RepositoryBase<Student> , IStudentRepository
    {
        public StudentRepository( RepositoryContext repositoryContext) 
        : base(repositoryContext)
        {
        }

       public void CreateStudent (Student student) => Create(student);

        public Student GetStudent(string studentId, bool trackChanges) =>
         FindByCondition(s => s.StudentId.Equals(studentId), trackChanges)
        .Include(s => s.User)
        .SingleOrDefault();

        public void UpdateStudent(Student student) => Update(student);

        public void DeleteStudent(Student student) => Delete(student);



    }
}
