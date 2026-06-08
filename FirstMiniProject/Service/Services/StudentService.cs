using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            _studentRepo.Create(student);

        }

        public void Delete(int id)
        {
            var student = GetById(id);
            _studentRepo.Delete(student);
        }

        public List<Student> GetAll()
        {
            return _studentRepo.GetAll();
        }

        public List<Student> GetAllByAge(int age)
        {
            return _studentRepo.GetByAge(age);
        }

        public List<Student> GetAllByGroupId(int groupId)
        {
            return _studentRepo.GetAllByGroupId(groupId);
        }

        public Student GetById(int id)

        {
            var student = _studentRepo.Get(x => x.Id == id);
            if (student == null)
            {

                throw new NotFoundException($"{id} ID-li məlumat tapılmadı.");
            }
            return student;
        }


        public List<Student> SearchByNameOrSurname(string text)
        {
            return _studentRepo.SearchByNameOrSurname(text);
        }

        public void Update(int id, Student student)
        {

            var existStudent = GetById(id);


            if (!string.IsNullOrWhiteSpace(student.Name))
            {
                existStudent.Name = student.Name;
            }

            if (!string.IsNullOrWhiteSpace(student.Surname))
            {
                existStudent.Surname = student.Surname;
            }



        }

    }
}
