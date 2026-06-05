using Domain.Entities;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository: IBaseRepository<Group>
    {
        List<Group> GetAllGroupsByTeacher(string teacher);
        List<Group> GetAllGroupsByRoom(string room);
        void Update(Group group);
        List<Group> SearchGroupByName(string name);



    }
}
