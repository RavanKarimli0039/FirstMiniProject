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
                        break; // Qrup tapıldısa dövrdən çıxırıq
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
            Console.WriteLine("--- Tələbə Məlumatlarının Yenilənməsi ---");
            Console.Write("Yeniləmək istədiyiniz tələbənin ID-sini daxil edin: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    
                    _studentService.GetById(id);

                    Console.Write("Yeni ad (dəyişmək istəmirsinizsə boş buraxın): ");
                    string newName = Console.ReadLine();

                    Console.Write("Yeni soyad (dəyişmək istəmirsinizsə boş buraxın): ");
                    string newSurname = Console.ReadLine();

                    Console.Write("Yeni yaş (dəyişmək istəmirsinizsə boş buraxın): ");
                    string ageInput = Console.ReadLine();
                    int newAge = string.IsNullOrWhiteSpace(ageInput) ? 0 : int.Parse(ageInput);

                    Console.Write("Yeni email (dəyişmək istəmirsinizsə boş buraxın): ");
                    string newEmail = Console.ReadLine();

                    Console.Write("Yeni Qrup ID (dəyişmək istəmirsinizsə boş buraxın): ");
                    string groupIdInput = Console.ReadLine();

                    Group newGroup = null;
                    if (!string.IsNullOrWhiteSpace(groupIdInput))
                    {
                        int newGroupId = int.Parse(groupIdInput);
                        newGroup = _groupService.GetById(newGroupId);
                    }

                   
                    Student updatedStudent = new Student
                    {
                        Name = newName,
                        Surname = newSurname,
                        Age = newAge,
                        Email = newEmail,
                        Group = newGroup
                    };

                    _studentService.Update(id, updatedStudent);
                    Console.WriteLine("Tələbə məlumatları uğurla yeniləndi.");
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Xəta baş verdi: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Xəta: ID rəqəm olmalıdır!");
            }
        }
    }
    
}