using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetAllByAge(int age);
        List<Student> GetAllByGroupId(int groupId);
        List<Student> SearchByNameOrSurname(string text);
        void Uptade(int id, Student student);
        void Create(Student student);
        Student GetById(int id);
    }
}
