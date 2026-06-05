using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGroupRepository _groupRepo;

        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;

        }

        public void Create(Student student)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAllByAge(int age)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public Student GeyById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> SearchByNameOrSurname(string text)
        {
            throw new NotImplementedException();
        }
    }
}
