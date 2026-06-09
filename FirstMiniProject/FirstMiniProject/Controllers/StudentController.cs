using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Service.Services.Interfaces;
using Service.Exceptions;

namespace FirstMiniProject.Controllers
{
    public class StudentController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public StudentController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public void Create()
        {
            try
            {
                Console.Write("Tələbə adı: ");
                string name = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
                {
                    Console.WriteLine("Ad boş ola bilməz və rəqəm daxil edilə bilməz!");
                    return;
                }

                Console.Write("Tələbə soyadı: ");
                string surname = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(surname) || surname.Any(char.IsDigit))
                {
                    Console.WriteLine("Soyad boş ola bilməz və rəqəm daxil edilə bilməz!");
                    return;
                }

                int age;
                while (true)
                {
                    Console.Write("Yaş (15-60): ");
                    if (!int.TryParse(Console.ReadLine(), out age))
                    {
                        Console.WriteLine("Format yanlışdır! Rəqəm daxil edin.");
                        continue;
                    }
         
                    if (age < 15 || age > 60)
                    {
                        Console.WriteLine("Yaş 15-60 arası olmalıdır!");
                        continue;
                    }
                    break;
                }

                string email;
                while (true)
                {
                    Console.Write("Email: ");
                    email = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                    {
                        Console.WriteLine("Email formatı yanlışdır (mütləq @ olmalıdır)!");
                        continue;
                    }
                    break;
                }

                int groupId;
                while (true)
                {
                    Console.Write("Qrup ID: ");
                    if (!int.TryParse(Console.ReadLine(), out groupId))
                    {
                        Console.WriteLine("Wrong format! Rəqəm daxil edin.");
                        continue;
                    }
                    try
                    {
                        var group = _groupService.GetById(groupId);
                        _studentService.Create(new Student { Name = name, Surname = surname, Age = age, Email = email, Group = group });
                        Console.WriteLine("Tələbə uğurla yaradıldı.");
                        break;
                    }
                    catch (NotFoundException)
                    {
                        Console.WriteLine("Belə bir qrup mövcud deyil! Yenidən cəhd edin.");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAll()
        {
            var list = _studentService.GetAll();
            if (list.Count == 0)
            {
                Console.WriteLine("Siyahı boşdur.");
                return;
            }
            foreach (var s in list)
                Console.WriteLine($"ID: {s.Id} | {s.Name} {s.Surname} | Yaş: {s.Age} | Email: {s.Email} | Qrup: {s.Group.Name}");
        }

        public void Update()
        {
            while (true)
            {
                try
                {
                    Console.Write("Update üçün ID (Çıxmaq üçün 0): ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Format yanlışdır!");
                        continue;
                    }
                    if (id == 0) break;

                    var exist = _studentService.GetById(id);

                    Console.Write($"Yeni ad (Köhnə: {exist.Name}): ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name) && name.Any(char.IsDigit))
                    {
                        Console.WriteLine("Ad daxilində rəqəm ola bilməz!");
                        continue;
                    }

                    Console.Write($"Yeni soyad (Köhnə: {exist.Surname}): ");
                    string surname = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(surname) && surname.Any(char.IsDigit))
                    {
                        Console.WriteLine("Soyad daxilində rəqəm ola bilməz!");
                        continue;
                    }

                    _studentService.Update(id, new Student { Name = name, Surname = surname });
                    Console.WriteLine("Məlumatlar yeniləndi.");
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
                    Console.Write("Silmək üçün ID (Çıxmaq üçün 0): ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Yanlış format!");
                        continue;
                    }
                    if (id == 0) break;

                    _studentService.Delete(id);
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

                    var s = _studentService.GetById(id);
                    Console.WriteLine($"ID: {s.Id} | {s.Name} {s.Surname} | Qrup: {s.Group.Name}");
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

        public void GetAllByAge()
        {
            while (true)
            {
                Console.Write("Yaş daxil edin: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int age))
                {
                    Console.WriteLine("Yanlış format! Rəqəm daxil edin.");
                    continue;
                }
                if (age < 15 || age > 60)
                {
                    Console.WriteLine("Olmaz! Yaş intervalı (15-60) düzgün deyil. Menyuya qayıdılır.");
                    break;
                }

                var list = _studentService.GetAllByAge(age);
                if (list.Count == 0) Console.WriteLine("Bu yaşda tələbə tapılmadı.");
                else foreach (var s in list) Console.WriteLine($"ID: {s.Id} | {s.Name} {s.Surname}");
                break;
            }
        }

        public void GetStudentsByGroupId()
        {
            while (true)
            {
                Console.Write("Qrup ID daxil edin (Çıxmaq üçün 0): ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Wrong format! Rəqəm daxil edin.");
                    continue;
                }
                if (id == 0) break;

                try
                {
                    _groupService.GetById(id);
                    var list = _studentService.GetAllByGroupId(id);
                    if (list.Count == 0) Console.WriteLine("Bu qrupda tələbə yoxdur.");
                    else foreach (var s in list) Console.WriteLine($"ID: {s.Id} | {s.Name} {s.Surname}");
                    break;
                }
                catch (NotFoundException)
                {
                    Console.WriteLine("Belə bir qrup mövcud deyil!");
                    break;
                }
            }
        }

        public void SearchByNameOrSurname()
        {
            Console.Write("Axtarış üçün ad və ya soyad daxil edin: ");
            string text = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Axtarış üçün mətn daxil edin!");
                return;
            }
            var list = _studentService.SearchByNameOrSurname(text);
            if (list.Count == 0) Console.WriteLine("Uyğun tələbə tapılmadı.");
            else foreach (var s in list) Console.WriteLine($"ID: {s.Id} | {s.Name} {s.Surname}");
        }
    }
}