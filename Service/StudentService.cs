using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DTO;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service
{
    internal sealed class StudentService : IStudentService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public StudentService(IRepositoryManager repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public StudentDto GetStudent(string id, bool trackChanges)
        {
            var student = _repository.Student.GetStudent(id, trackChanges);
            if (student is null)
                throw new StudentNotFoundException(id);
            var studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }
        public async Task UpdateStudentProfileAsync(string studentId, StudentForUpdateDto studentForUpdate)
        {
            var student = _repository.Student.GetStudent(studentId, trackChanges: true);
            if (student == null)
                throw new StudentNotFoundException(studentId);

            if (studentForUpdate.UserName != null)
                student.User.UserName = studentForUpdate.UserName;
            if (studentForUpdate.Bio != null)
                student.User.Bio = studentForUpdate.Bio;
            if (studentForUpdate.PhoneNumber != null)
                student.User.PhoneNumber = studentForUpdate.PhoneNumber;
            if (studentForUpdate.UrlResume != null)
                student.UrlResume = studentForUpdate.UrlResume;
            _repository.Student.UpdateStudent(student);
            _repository.Save();
        }

        
        
       
    }


}

