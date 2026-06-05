using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        List<Student> GetByAge(int age);
        List<Student> GetAllByGroupId(int groupId);
        List<Student> SearchByNameOrSurname(string nameOrSurname);
        void Update(Student student);
    }
}
