using Domain.Entities;
using Service.Exceptions;
using Service.Services.Interfaces;

namespace Project.Controllers
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
            Console.WriteLine("--- Yeni Tələbə Yaradılması ---");
     
            string name = GetValidatedName("Tələbənin adını daxil edin: ");

            string surname = GetValidatedName("Tələbənin soyadını daxil edin: ");


            int age;
            while (true)
            {
                Console.Write("Yaşını daxil edin (15-60): ");
                if (int.TryParse(Console.ReadLine(), out age) && age >= 15 && age <= 60) break;
                Console.WriteLine("Xəta: Yaş 15 və 60 arasında bir rəqəm olmalıdır!");
            }

            
            string email;
            while (true)
            {
                Console.Write("Email daxil edin: ");
                email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@")) break;
                Console.WriteLine("Xəta: Email boş ola bilməz və daxilində '@' olmalıdır!");
            }

            
            Group foundGroup = null;
            while (true)
            {
                Console.Write("Tələbənin əlavə olunacağı Qrup ID-sini daxil edin: ");
                if (int.TryParse(Console.ReadLine(), out int groupId))
                {
                    try
                    {
                        foundGroup = _groupService.GetById(groupId);
                        break; 
                    }
                    catch (NotFoundException)
                    {
                        Console.WriteLine("Xəta: Bu ID-li qrup tapılmadı! Yenidən yoxlayın.");
                    }
                }
                else
                {
                    Console.WriteLine("Xəta: ID rəqəm olmalıdır!");
                }
            }

            
            try
            {
                Student newStudent = new Student
                {
                    Name = name,
                    Surname = surname,
                    Age = age,
                    Email = email,
                    Group = foundGroup
                };

                _studentService.Create(newStudent);
                Console.WriteLine($"Tələbə uğurla yaradıldı və '{foundGroup.Name}' qrupuna əlavə olundu!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gözlənilməz xəta: {ex.Message}");
            }
        }

        private string GetValidatedName(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Xəta: Bu xana boş ola bilməz!");
                    continue;
                }

                bool hasDigit = false;
                foreach (char c in input)
                {
                    if (char.IsDigit(c)) { hasDigit = true; break; }
                }

                if (hasDigit) Console.WriteLine("Xəta: Daxil edilən mətndə rəqəm ola bilməz!");
                else return input;
            }
        }

        public void GetStudentsByGroupId()
        {
            Console.Write("Qrup ID-sini daxil edin: ");
            if (int.TryParse(Console.ReadLine(), out int groupId))
            {
                var students = _studentService.GetAllByGroupId(groupId);
                if (students.Count == 0)
                {
                    Console.WriteLine("Bu qrupda tələbə tapılmadı.");
                    return;
                }

                foreach (var item in students)
                {
                    Console.WriteLine($"ID: {item.Id} | {item.Name} {item.Surname} | Yaş: {item.Age} | Qrup: {item.Group.Name}");
                }
            }
            else
            {
                Console.WriteLine("Xəta: ID rəqəm olmalıdır!");
            }
        }

        public void Delete()
        {
            Console.Write("Silmək istədiyiniz tələbənin ID-si: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    _studentService.Delete(id);
                    Console.WriteLine("Tələbə silindi.");
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void GetAll()
        {
            var students = _studentService.GetAll();

            if (students.Count == 0)
            {
                Console.WriteLine("Sistemdə hələ heç bir tələbə yoxdur.");
                return;
            }

            Console.WriteLine("\n--- Bütün Tələbələr ---");
            foreach (var s in students)
            {
                string groupName = s.Group != null ? s.Group.Name : "Qrup təyin edilməyib!";

                Console.WriteLine($"ID: {s.Id} | {s.Name} {s.Surname} | Yaş: {s.Age} | Qrup: {groupName}");
            }
        }

        public void GetAllByAge()
        {
            Console.Write("Axtardığınız yaşı daxil edin: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                var students = _studentService.GetAllByAge(age);

                if (students.Count == 0)
                {
                    Console.WriteLine($"{age} yaşında tələbə tapılmadı.");
                    return;
                }

                foreach (var item in students)
                {
                    Console.WriteLine($"ID: {item.Id} | {item.Name} {item.Surname} | Yaş: {item.Age} | Qrup: {item.Group.Name}");
                }
            }
            else
            {
                Console.WriteLine("Xəta: Yaş rəqəm olmalıdır!");
            }
        }

        public void SearchByNameOrSurname()
        {
            Console.Write("Axtardığınız tələbənin adını və ya soyadını daxil edin: ");
            string text = Console.ReadLine();

            var students = _studentService.SearchByNameOrSurname(text);

            if (students.Count == 0)
            {
                Console.WriteLine("Axtarışa uyğun tələbə tapılmadı.");
                return;
            }

            foreach (var item in students)
            {
                Console.WriteLine($"ID: {item.Id} | {item.Name} {item.Surname} | Qrup: {item.Group.Name}");
            }
        }

        public void GetById()
        {
            Console.Write("Axtardığınız tələbənin ID-sini daxil edin: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var student = _studentService.GetById(id);
                    Console.WriteLine($"ID: {student.Id} | Ad: {student.Name} | Soyad: {student.Surname} | Yaş: {student.Age} | Qrup: {student.Group.Name}");
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Xəta: ID rəqəm olmalıdır!");
            }
        }

        public void Update()
        {
            Console.WriteLine("\n--- Tələbə Məlumatlarının Yenilənməsi ---");

           
            int id;
            while (true)
            {
                Console.Write("Yeniləmək istədiyiniz tələbənin ID-sini daxil edin: ");
                if (int.TryParse(Console.ReadLine(), out id)) break;
                Console.WriteLine("Xəta: ID yalnız rəqəm olmalıdır!");
            }

            try
            {
                
                _studentService.GetById(id);

                Console.Write("Yeni ad (dəyişmək istəmirsinizsə boş buraxın): ");
                string newName = Console.ReadLine();

               
                Console.Write("Yeni soyad (dəyişmək istəmirsinizsə boş buraxın): ");
                string newSurname = Console.ReadLine();

               
                int newAge = 0;
                while (true)
                {
                    Console.Write("Yeni yaş (15-60 arası, dəyişmək istəmirsinizsə boş buraxın): ");
                    string ageInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(ageInput)) break; 

                    if (int.TryParse(ageInput, out newAge))
                    {
                        if (newAge >= 15 && newAge <= 60) break;
                        else Console.WriteLine("Xəta: Yaş 15 və 60 aralığında olmalıdır!");
                    }
                    else
                    {
                        Console.WriteLine("Xəta: Zəhmət olmasa düzgün rəqəm daxil edin!");
                    }
                }

                
                string newEmail = "";
                while (true)
                {
                    Console.Write("Yeni email (dəyişmək istəmirsinizsə boş buraxın): ");
                    newEmail = Console.ReadLine();

                   
                    if (!string.IsNullOrWhiteSpace(newEmail) &&
                        newEmail.Contains("@") &&
                        newEmail.Substring(newEmail.IndexOf("@")).Contains(".") &&
                        !newEmail.EndsWith("."))
                    {
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Xəta: Email formatı yanlışdır! (Məsələn: rvan@gmail.com)");
                    }

                   
                    Group newGroup = null;
                    while (true)
                    {
                        Console.Write("Yeni Qrup ID (dəyişmək istəmirsinizsə boş buraxın): ");
                        string groupIdInput = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(groupIdInput)) break; 

                        if (int.TryParse(groupIdInput, out int newGroupId))
                        {
                            try
                            {
                                newGroup = _groupService.GetById(newGroupId);
                                break;
                            }
                            catch (NotFoundException)
                            {
                                Console.WriteLine($"Xəta: {newGroupId} ID-li qrup tapılmadı! Yenidən daxil edin.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Xəta: Qrup ID-si rəqəm olmalıdır!");
                        }
                    }

                   
                    Student updatedStudentData = new Student
                    {
                        Name = newName,
                        Surname = newSurname,
                        Age = newAge,
                        Email = newEmail,
                        Group = newGroup
                    };

                    _studentService.Update(id, updatedStudentData);
                    Console.WriteLine("Tələbə məlumatları uğurla yeniləndi!");


                }
            }


            catch (NotFoundException ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gözlənilməz bir xəta baş verdi: {ex.Message}");
            }
        }
    }
    
}