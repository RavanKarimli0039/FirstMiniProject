using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class GroupService :  IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        private readonly IStudentRepository _studentRepo;

        public GroupService(IGroupRepository groupRepo, IStudentRepository studentRepo)
        {
            _groupRepo = groupRepo;
            _studentRepo = studentRepo;
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

        public void Update(int id, Group group)
        {
            
            var existGroup = GetById(id);

            
            if (!string.IsNullOrWhiteSpace(group.Name))
            {

                existGroup.Name = group.Name;
            }

            if (!string.IsNullOrWhiteSpace(group.TeacherFullName))
            {
                existGroup.TeacherFullName = group.TeacherFullName;
            }

            if (!string.IsNullOrWhiteSpace(group.RoomName))
            {
                existGroup.RoomName = group.RoomName;
            }

            
            _groupRepo.Update(existGroup);

           
            Console.WriteLine("Məlumatlar uğurla yeniləndi.");
        }




        public Group GetById(int id)
        {
            var group = _groupRepo.Get(x => x.Id == id);
            if (group == null)
            {
                
                throw new NotFoundException($"{id} ID-li məlumat tapılmadı.");
            }
            return group;
        }

        public void Delete(int id)
        {
            var group = GetById(id);
            if (group == null) return;

           
            var studentsInGroup = _studentRepo.GetAll(x => x.Group.Id == id);
            foreach (var student in studentsInGroup)
            {
                _studentRepo.Delete(student);
            }

            
            _groupRepo.Delete(group);
        }

        public List<Group> GetAll()
        {
            return _groupRepo.GetAll();
        }
    }
}