using Domain.Entities;
using Repository.Data;
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
        public List<Group> GetAllGroupsByRoom(string roomName)
        {
            return AppDbContext<Group>.datas.Where(x => x.RoomName == roomName).ToList();
        }

        public List<Group> GetAllGroupsByTeacher(string teacherFullName)
        {
            return AppDbContext<Group>.datas.Where(x => x.TeacherFullName == teacherFullName).ToList();
        }

        public List<Group> SearchGroupByName(string groupName)
        {
            return AppDbContext<Group>.datas.Where(x => x.Name == groupName).ToList();
        }
        
    }


}
