using Domain.Entities;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        List<Group> GetAll();
        List<Group> GetAllByTeacher(string teacherName);
        List<Group> GetAllByRoom(string roomName);
        List<Group> SearchByName(string name);
        void Create(Group group);
        void Update(int id, Group group);
        Group GetById(int id);
        void Delete(int id);

       
    }
}
