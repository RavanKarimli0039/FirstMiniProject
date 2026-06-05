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

        public StudentService(IStudentRepository studentRepo, IGroupRepository groupRepo)
        {
            _studentRepo = studentRepo;
            _groupRepo = groupRepo;

        }

        public void Create(Student student)
        {
            
        }

        public List<Student> GetAllByAge(int age)
        {
            
        }

        public List<Student> GetAllByGroupId(int groupId)
        {
            
        }

        public Student GetById(int id)
        {
            
        }

        public List<Student> SearchByNameOrSurname(string text)
        {
            
        }

        public void Uptade(int id, Student student)
        {
            
        }
    }
}
