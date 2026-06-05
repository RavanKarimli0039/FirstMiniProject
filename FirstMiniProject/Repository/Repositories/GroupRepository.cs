using Domain.Entities;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public List<Group> GetAllGroupsByRoom(string room)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllGroupsByTeacher(string teacher)
        {
            throw new NotImplementedException();
        }

        public List<Group> SearchGroupByName()
        {
            throw new NotImplementedException();
        }

        public void Update(T data)
        {
            throw new NotImplementedException();
        }
    }
}
