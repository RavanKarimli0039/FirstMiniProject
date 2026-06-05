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
            throw new NotImplementedException();
        }

        public List<Student> GetByAge(int age)
        {
            throw new NotImplementedException();
        }

        public List<Student> SearchByNameOrSurnamew(string nameOrSurname)
        {
            throw new NotImplementedException();
        }
    }
}
