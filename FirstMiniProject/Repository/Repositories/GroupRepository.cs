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
            return GetAll(x => x.RoomName == roomName);
        }

        public List<Group> GetAllGroupsByTeacher(string teacherFullName)
        {
            return GetAll(x => x.TeacherFullName == teacherFullName);
        }

        public List<Group> SearchGroupByName(string groupName)
        {
            return GetAll(x => x.Name.Contains(groupName));
        }

        public void Update(Group group)
        {
            var existGroup = AppDbContext<Group>.datas.Find(x => x.Id == group.Id);

            if(existGroup != null)
            {
                existGroup.Name = group.Name;
                existGroup.TeacherFullName = group.TeacherFullName;
                existGroup.RoomName = group.RoomName;
            }
        }
    }
}
