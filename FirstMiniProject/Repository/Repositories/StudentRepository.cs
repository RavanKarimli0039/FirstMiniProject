using Domain.Entities;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public List<Student> GetAllByGroupId(int groupId)
        {
            return GetAll(x => x.Group.Id == groupId);
        }

        public List<Student> GetByAge(int age)
        {
            return GetAll(x => x.Age == age);
        }

        public List<Student> SearchByNameOrSurname(string nameOrSurname)
        {
            return GetAll(x => x.Name.Contains(nameOrSurname)|| x.Surname.Contains(nameOrSurname));
        }

       
    }
}
