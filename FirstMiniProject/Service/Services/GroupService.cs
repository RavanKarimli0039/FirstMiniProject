using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class GroupService :  IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        public GroupService(IGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }

        public List<Group> GetAllByRoom(string roomName)
        {
            return _groupRepo.GetAll(x => x.RoomName.Contains(roomName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Group> GetAllByTeacher(string teacherName)
        {
            return _groupRepo.GetAll(x => x.TeacherFullName.Contains(teacherName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Group> SearchByName(string name)
        {
            return _groupRepo.GetAll(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        
        public void Create(Group data)
        {
            _groupRepo.Create(data);
        }

        public void Update(int id, Group data)
        {
           

        }

        public Group GeyById(int id)
        {
            throw new NotImplementedException();
        }
    }
}