using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Service.Services.Interfaces;
using Service.Exceptions;

namespace FirstMiniProject.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public void Create()
        {
            try
            {
                Console.Write("Qrup adını daxil edin: ");
                string name = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Qrup adı boş ola bilməz!");
                    return;
                }

                string teacherFullName;
                while (true)
                {
                    Console.Write("Müəllimin tam adını (Ad və Soyad) daxil edin: ");
                    teacherFullName = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(teacherFullName))
                    {
                        Console.WriteLine("Müəllim adı boş ola bilməz!");
                        continue;
                    }
                    if (teacherFullName.Any(char.IsDigit))
                    {
                        Console.WriteLine("Müəllim adında rəqəm ola bilməz!");
                        continue;
                    }
                    if (teacherFullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 2)
                    {
                        Console.WriteLine("Tam adı daxil edin (Ad və Soyad mütləqdir)!");
                        continue;
                    }
                    break;
                }

                Console.Write("Otaq adını daxil edin: ");
                string roomName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(roomName))
                {
                    Console.WriteLine("Otaq adı boş ola bilməz!");
                    return;
                }

                _groupService.Create(new Group { Name = name, TeacherFullName = teacherFullName, RoomName = roomName });
                Console.WriteLine("Qrup uğurla yaradıldı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAll()
        {
            var groups = _groupService.GetAll();
            if (groups.Count == 0)
            {
                Console.WriteLine("Siyahı boşdur.");
                return;
            }
            foreach (var item in groups)
                Console.WriteLine($"ID: {item.Id} | Ad: {item.Name} | Müəllim: {item.TeacherFullName} | Otaq: {item.RoomName}");
        }

        public void Update()
        {
            while (true)
            {
                try
                {
                    Console.Write("Yeniləmək üçün ID daxil edin (Çıxmaq üçün 0): ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Yanlış format! Rəqəm daxil edin.");
                        continue;
                    }
                    if (id == 0) break;

                    var exist = _groupService.GetById(id);

                    Console.Write($"Yeni qrup adı (Köhnə: {exist.Name}): ");
                    string name = Console.ReadLine();

                    Console.Write($"Yeni müəllim tam adı (Köhnə: {exist.TeacherFullName}): ");
                    string teacher = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(teacher) && teacher.Any(char.IsDigit))
                    {
                        Console.WriteLine("Müəllim adında rəqəm ola bilməz! Yenidən cəhd edin.");
                        continue;
                    }

                    Console.Write($"Yeni otaq adı (Köhnə: {exist.RoomName}): ");
                    string room = Console.ReadLine();

                    _groupService.Update(id, new Group { Name = name, TeacherFullName = teacher, RoomName = room });
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is NotFoundException) continue;
                    break;
                }
            }
        }

        public void Delete()
        {
            while (true)
            {
                try
                {
                    Console.Write("Silmək üçün ID daxil edin (Çıxmaq üçün 0): ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Yanlış format!");
                        continue;
                    }
                    if (id == 0) break;

                    _groupService.Delete(id);
                    Console.WriteLine("Uğurla silindi.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is NotFoundException) continue;
                    break;
                }
            }
        }

        public void GetById()
        {
            while (true)
            {
                try
                {
                    Console.Write("ID daxil edin (Çıxmaq üçün 0): ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Yanlış format!");
                        continue;
                    }
                    if (id == 0) break;

                    var g = _groupService.GetById(id);
                    Console.WriteLine($"ID: {g.Id} | Ad: {g.Name} | Müəllim: {g.TeacherFullName} | Otaq: {g.RoomName}");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is NotFoundException) continue;
                    break;
                }
            }
        }

        public void GetAllByTeacher()
        {
            while (true)
            {
                Console.Write("Axtarmaq üçün müəllim tam adını daxil edin: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Boşluq olmaz!");
                    continue;
                }
                if (input.Any(char.IsDigit))
                {
                    Console.WriteLine("Format yanlışdır, rəqəm olmaz!");
                    continue;
                }
                if (input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 2)
                {
                    Console.WriteLine("Tam adı daxil edin (Ad və Soyad mütləqdir)!");
                    continue;
                }

                var list = _groupService.GetAllByTeacher(input);
                if (list.Count == 0) Console.WriteLine("Məlumat tapılmadı.");
                else foreach (var g in list) Console.WriteLine($"ID: {g.Id} | Qrup: {g.Name} | Müəllim: {g.TeacherFullName}");
                break;
            }
        }

        public void GetAllByRoom()
        {
            Console.Write("Otaq adını daxil edin: ");
            string input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input lazımdır!");
                return;
            }

            var list = _groupService.GetAllByRoom(input);
            if (list.Count == 0)
            {
                Console.WriteLine("Belə otaq yoxdur.");
                return;
            }
            foreach (var g in list) Console.WriteLine($"ID: {g.Id} | Qrup: {g.Name} | Otaq: {g.RoomName}");
        }

        public void SearchByName()
        {
            Console.Write("Axtarış üçün qrup adı daxil edin: ");
            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Axtarış üçün mətn daxil edin!");
                return;
            }
            var list = _groupService.SearchByName(input);
            foreach (var g in list) Console.WriteLine($"ID: {g.Id} | Qrup: {g.Name}");
        }
    }
}